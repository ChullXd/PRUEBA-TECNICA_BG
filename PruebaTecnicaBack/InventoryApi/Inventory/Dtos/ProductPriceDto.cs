namespace AdministracionApi.Inventory.Dtos;

public class ProductPriceDto
{
    public Guid? Id { get; set; }
    public required decimal Price { get; set; }
    public required string Store { get; set; }
    
    public static ProductPriceDto Create(decimal price, string store) =>  new() 
        {
            Price = price,
            Store = store
        };
    
    
}
