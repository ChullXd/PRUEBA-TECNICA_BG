namespace AdministracionApi.Inventory.Endpoints.CreateProduct;

public class CreateProductEndpoint  : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ProductRoutes.Base, async (ISender sender, [AsParameters] CreateProductCommand command) =>
            Results.Created($"{ProductRoutes.Base}/Id",(await sender.Send(command)).ProductDto)
        )
        .WithName("CreateProduct")
        .WithTags(ProductRoutes.Tag)
        .Produces<ProductDto>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Create product")
        .WithDescription("Create Product");
    }
}