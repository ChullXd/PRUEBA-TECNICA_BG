namespace AdministracionApi.Inventory.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = default!;
    public required string Description { get; set; } = default!;
    public required long Quantity { get; set; }
    public List<ProductPriceDto> ProductPrices { get; set; } = [];
}
