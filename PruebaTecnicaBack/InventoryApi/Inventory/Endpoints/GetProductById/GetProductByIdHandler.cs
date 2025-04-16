namespace AdministracionApi.Inventory.Endpoints.GetProductById;
public record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResult>;

public record GetProductByIdResult(ProductDto ProductDto);
public class GetProductByIdHandler(IInventoryRepository repository) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)=>
        new (await repository.GetProductById(query.Id, cancellationToken));
}