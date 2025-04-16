namespace AdministracionApi.Inventory.Endpoints.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ProductRoutes.Base, async (ISender sender, [AsParameters] GetProductsQuery query) =>
             Results.Ok((await sender.Send(query)).ProductsDto)
        )
        .WithName("GetProducts")
        .WithTags(ProductRoutes.Tag)
        .Produces<List<ProductDto>>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}