
namespace AdministracionApi.Inventory.Endpoints.DeleteProduct;
public record DeleteProductCommand(Guid Id) : IRequest<DeleteProductResult>;
public record DeleteProductResult(Unit Unit);
public class DeleteProductHandler(IInventoryRepository repository) : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    public async  Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken) =>
        new (await repository.DeleteProduct(command.Id, cancellationToken));
}