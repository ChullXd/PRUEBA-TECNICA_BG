namespace AdministracionApi.Inventory.Endpoints.GetProductById;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet($"{ProductRoutes.Base}/{{id:guid}}", async (Guid id, ISender sender) =>
                Results.Ok((await sender.Send(new GetProductByIdQuery(id))).ProductDto)
        )
        .WithName("GetProductById")
        .WithTags(ProductRoutes.Tag)
        .Produces<ProductDto>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Product by id")
        .WithDescription("Get Product By Id");
    }
}