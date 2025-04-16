namespace AdministracionApi.Inventory.Data;

public interface IInventoryRepository
{
    //Get
    Task<List<ProductDto>> GetProducts(int? pageNumber, int? pageSize,CancellationToken cancellationToken = default);
    Task<ProductDto> GetProductById(Guid id,CancellationToken cancellationToken = default);
    
    //Create
    Task<ProductDto> CreateProduct(ProductDto productDto,CancellationToken cancellationToken = default);
    
    //Update
    Task<ProductDto> UpdateProduct(ProductDto productDto,CancellationToken cancellationToken = default);
    
    //Delete
    Task<Unit> DeleteProduct(Guid id,CancellationToken cancellationToken = default);

}