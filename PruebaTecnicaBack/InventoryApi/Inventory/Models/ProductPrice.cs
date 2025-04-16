namespace AdministracionApi.Inventory.Models;
[Table(nameof(ProductPrice), Schema ="DBO")]
public class ProductPrice : Entity<Guid>
{
    [Precision(18, 2)]
    public required decimal Price { get; set; }
    public required string Store { get; set; }
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;

    public static ProductPrice Create(
            decimal price,
            string store,
            Guid productId
        ) => new()
    {
        Id = Guid.NewGuid(),
        Price = price,
        Store = store,
        ProductId = productId,
    };
}
