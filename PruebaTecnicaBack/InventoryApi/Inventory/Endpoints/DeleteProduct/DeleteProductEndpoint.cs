namespace AdministracionApi.Inventory.Endpoints.DeleteProduct;

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(ProductRoutes.Base, async (ISender sender, [AsParameters] DeleteProductCommand command) =>
            {
                await sender.Send(command);
                return Results.NoContent();
            }
        )
        .WithName("DeleteProduct")
        .WithTags(ProductRoutes.Tag)
        .Produces<Unit>(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete product by id")
        .WithDescription("Delete Product");
    }
}