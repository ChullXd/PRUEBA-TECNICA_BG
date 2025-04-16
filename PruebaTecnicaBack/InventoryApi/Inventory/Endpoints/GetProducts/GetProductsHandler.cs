
namespace AdministracionApi.Inventory.Endpoints.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IRequest<GetProductsResult>;

public record GetProductsResult(IEnumerable<ProductDto> ProductsDto);

public class GetProductsHandler(IInventoryRepository repository) : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken) => 
        new(await repository.GetProducts(request.PageNumber, request.PageSize, cancellationToken));
}