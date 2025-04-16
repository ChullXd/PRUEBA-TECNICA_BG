namespace AdministracionApi.Inventory.Data;

public class CachedInventoryRepository(IInventoryRepository repository, IDistributedCache cache ): IInventoryRepository
{
    public async Task<List<ProductDto>> GetProducts(int? pageNumber, int? pageSize, CancellationToken cancellationToken = default)
    {
        var cachedKey = $"Products({pageNumber}-{pageSize})";
        var cachedProducts = await cache.GetStringAsync(cachedKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedProducts))
            return JsonSerializer.Deserialize<List<ProductDto>>(cachedProducts)!;
        var products = await repository.GetProducts(pageNumber, pageSize, cancellationToken);
        // Cache the products
        await cache.SetStringAsync(cachedKey, JsonSerializer.Serialize(products), cancellationToken);
        // Add the cache key to the index
        await AddCacheKeyToIndexAsync(cachedKey, cancellationToken);
        return products;
    }


    public async Task<ProductDto> GetProductById(Guid id, CancellationToken cancellationToken = default)
    {
        var cachedKey = $"Product-{id}";
        var cachedProduct = await cache.GetStringAsync(cachedKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedProduct))
            return JsonSerializer.Deserialize<ProductDto>(cachedProduct)!;
        var product = await repository.GetProductById(id, cancellationToken);
        await cache.SetStringAsync(cachedKey, JsonSerializer.Serialize(product), cancellationToken);
        return product;
    }

    public async Task<ProductDto> CreateProduct(ProductDto productDto, CancellationToken cancellationToken = default)
    {
        var product = await repository.CreateProduct(productDto, cancellationToken);
        var cachedKey = $"Product-{product.Id}";
        await cache.SetStringAsync(cachedKey, JsonSerializer.Serialize(product), cancellationToken);
        return product;
    }

    public async Task<ProductDto> UpdateProduct(ProductDto productDto, CancellationToken cancellationToken = default)
    {
        var product = await repository.UpdateProduct(productDto, cancellationToken);
        // Invalidate relevant cache entries
        await InvalidateProductListCachesAsync(cancellationToken);
        var cachedKey = $"Product-{product.Id}";
        await cache.SetStringAsync(cachedKey, JsonSerializer.Serialize(product), cancellationToken);
        return product;
    }

    public async Task<Unit> DeleteProduct(Guid id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteProduct(id, cancellationToken);
        // Invalidate relevant cache entries
        await InvalidateProductListCachesAsync(cancellationToken);
        var cachedKey = $"Product-{id}";
        await cache.RemoveAsync(cachedKey,  cancellationToken);
        return Unit.Value;
    }
    
    private async Task AddCacheKeyToIndexAsync(string cacheKey, CancellationToken cancellationToken)
    {
        const string indexKey = "ProductListCacheKeys";
        var existingKeys = await cache.GetStringAsync(indexKey, cancellationToken);
        var cacheKeys = string.IsNullOrEmpty(existingKeys)
            ? []
            : JsonSerializer.Deserialize<HashSet<string>>(existingKeys)!;
        cacheKeys.Add(cacheKey);
        await cache.SetStringAsync(indexKey, JsonSerializer.Serialize(cacheKeys), cancellationToken);
    }
    private async Task InvalidateProductListCachesAsync(CancellationToken cancellationToken)
    {
        const string indexKey = "ProductListCacheKeys";
        var existingKeys = await cache.GetStringAsync(indexKey, cancellationToken);
        if (string.IsNullOrEmpty(existingKeys))
            return;
        var cacheKeys = JsonSerializer.Deserialize<HashSet<string>>(existingKeys)!;
        foreach (var cacheKey in cacheKeys)
        {
            await cache.RemoveAsync(cacheKey, cancellationToken);
        }
        // Clear the index after invalidation
        await cache.RemoveAsync(indexKey, cancellationToken);
    }


}