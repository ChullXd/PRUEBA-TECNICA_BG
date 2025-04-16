namespace AdministracionApi.Inventory.Endpoints.UpdateProduct;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(ProductRoutes.Base, async (ISender sender, [AsParameters] UpdateProductCommand command) =>
            Results.Ok((await sender.Send(command)).ProductDto)
        )
            
        .WithName("UpdateProduct")
        .WithTags(ProductRoutes.Tag)
        .Produces<ProductDto>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");;
    }
}