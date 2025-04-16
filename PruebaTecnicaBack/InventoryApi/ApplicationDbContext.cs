using System.Reflection;

namespace AdministracionApi;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToTable("Users", "SEG");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsersToken", "SEG");
        modelBuilder.Entity<IdentityRole>().ToTable("Role", "SEG");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "SEG");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "SEG");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "SEG");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "SEG");
        
        // Aplicar el filtro de consulta global a todas las entidades que implementan IEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType.ClrType)) continue;
            var method = typeof(ApplicationDbContext)
                .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(entityType.ClrType);
            method?.Invoke(null, [modelBuilder]);
        }
    }
    
    private static void SetGlobalQueryFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : class, IEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e => e.Active ?? false);
    }
    
    public new DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
}


