public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductHistory>> GetProductHistoryAsync(int id);
    Task<Product> CreateProductAsync(Product product);
    Task UpdateProductAsync(int id, Product product);
    Task DeleteProductAsync(int id);
}