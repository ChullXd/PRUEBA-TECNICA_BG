    namespace AdministracionApi.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await context.Database.MigrateAsync();
        await SeedAsync(userManager, context);
    }
    private static async Task SeedAsync(UserManager<User> userManager, ApplicationDbContext context)
    {
        await SeedUsersAsync(userManager);
        await SeedProductsAsync(context);
    }
    private static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (!await userManager.Users.AnyAsync())
        {
            var user = new User
            {
                UserName = "admin",
                Email = "admin@example.com",
                FirstName = "Test",
                LastName = "Test"
            };
            await userManager.CreateAsync(user, "Admin1234!");
        }
    }
    private static async Task SeedProductsAsync(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(GenerateRandomProducts(50));
            await context.SaveChangesAsync();
        }
    }
    
    private static List<Product> GenerateRandomProducts(int quantity)
    {
        var products = new List<Product>();
        var random = new Random();
        for (var i = 0; i < quantity; i++)
        {
            var productName = $"Product_{i + 1}";
            var productDescription = $"Description for {productName}";
            var productQuantity = random.Next(1, 10000); // Cantidad entre 1 y 10,000
            // Generar una cantidad aleatoria de precios entre 1 y 10
            var priceCount = random.Next(1, 11);
            var productPrices = new List<ProductPriceDto>();

            for (var j = 0; j < priceCount; j++)
            {
                var price = RandomDecimal(1, 100, random);
                var store = $"Store_{random.Next(1, 5)}";
                productPrices.Add(ProductPriceDto.Create(price, store));
            }
            var product = Product.Create(productName, productDescription, productQuantity, productPrices);
            products.Add(product);
        }
        return products;
    }

    private static decimal RandomDecimal(int minValue, int maxValue, Random random)
    {
        var value = (decimal)random.NextDouble() * (maxValue - minValue) + minValue;
        return Math.Round(value, 2); // Redondear a 2 decimales
    }


}