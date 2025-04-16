namespace AdministracionApi.Inventory.Models;
[Table(nameof(Product), Schema ="DBO")]
public class Product : Entity<Guid>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required long Quantity { get; set; }

    public static Product Create( string name, string description, long quantity, List<ProductPriceDto> productPriceDtos)
    {
        var guid = Guid.NewGuid();
        return new Product
        {
        Id = guid,
        Name = name,
        Description = description,
        Quantity = quantity,
        ProductPrices = productPriceDtos.Select(x => ProductPrice.Create(x.Price, x.Store, guid)).ToList(),
        };
    }
    
    public virtual ICollection<ProductPrice> ProductPrices { get; set; } = [];

}
