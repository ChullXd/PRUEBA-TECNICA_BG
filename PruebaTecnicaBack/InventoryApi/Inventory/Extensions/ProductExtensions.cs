
namespace AdministracionApi.Inventory.Extensions;

public static class ProductExtensions
{
    public static IEnumerable<ProductDto> ToProductDtoList(this IEnumerable<Product> products) =>
        products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Quantity = p.Quantity,
            ProductPrices = p.ProductPrices.Select(price => new ProductPriceDto
            {
                Id = price.Id,
                Price = price.Price,
                Store = price.Store
            }).ToList()
        });

    public static ProductDto ToProductDto(this Product? product) =>
        new()
        {
            Id = product!.Id,
            Name = product.Name,
            Description = product.Description,
            Quantity = product.Quantity,
            ProductPrices = product.ProductPrices.Select(price => new ProductPriceDto
            {
                Id = price.Id,
                Price = price.Price,
                Store = price.Store
            }).ToList()
        };

}
