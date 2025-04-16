namespace AdministracionApi.Inventory.Data;

public class InventoryRepository(ApplicationDbContext context) : IInventoryRepository
{
    public async Task<List<ProductDto>> GetProducts(int? pageNumber, int? pageSize, CancellationToken cancellationToken = default) => 
        (await context.Products
            .Include(p =>
                p.ProductPrices).AsNoTracking()
            .Skip(((pageNumber ?? 1) - 1) * (pageSize ?? 10))
            .Take(pageSize ?? 10)
            .ToListAsync(cancellationToken)).ToProductDtoList().ToList();

    public async Task<ProductDto> GetProductById(Guid id, CancellationToken cancellationToken = default) =>
         (await context.Products.FindAsync([id], cancellationToken) ??
                throw new NotFoundException("Producto no encontrado")
            ).ToProductDto();

    public async Task<ProductDto> CreateProduct(ProductDto productDto, CancellationToken cancellationToken = default)
    {
        var newProduct = Product.Create(productDto.Name,
            productDto.Description,
            productDto.Quantity,
            productDto.ProductPrices);
        await context.Products.AddAsync(
            newProduct
            , cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return newProduct.ToProductDto();
    }

    public async Task<ProductDto> UpdateProduct(ProductDto productDto, CancellationToken cancellationToken = default)
    {
        if (await context.Products
                .Include(p => p.ProductPrices)
                .FirstOrDefaultAsync(p => p.Id == productDto.Id, cancellationToken) is not {} product)
            throw new NotFoundException("Producto no encontrado");
        
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Quantity = productDto.Quantity;
        
        var dtoPriceIds = productDto.ProductPrices.Select(pp => pp.Id).ToList();
        var pricesToRemove = product.ProductPrices
            .Where(pp => !dtoPriceIds.Contains(pp.Id))
            .ToList();
        foreach (var price in pricesToRemove)
        {
            price.Active = false;
            context.ProductPrices.Entry(price).State = EntityState.Modified;
        }
        foreach (var priceDto in productDto.ProductPrices)
        {
            if (product.ProductPrices.FirstOrDefault(pp => pp.Id == priceDto.Id) is {} existingPrice)
            {
                existingPrice.Price = priceDto.Price;
                existingPrice.Store = priceDto.Store;
                context.ProductPrices.Entry(existingPrice).State = EntityState.Modified;
            }
            else
            {
                var newPrice = ProductPrice.Create(priceDto.Price, priceDto.Store, product.Id);
                product.ProductPrices.Add(newPrice);
                context.ProductPrices.Entry(newPrice).State = EntityState.Added;
            }
        }
        await context.SaveChangesAsync(cancellationToken);
        return product.ToProductDto();
    }

    public async Task<Unit> DeleteProduct(Guid id, CancellationToken cancellationToken = default)
    {
        if (await context.Products.FindAsync([id], cancellationToken) is not { } product)
            throw new NotFoundException("Producto no encontrado");
        product.Active = false;
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}