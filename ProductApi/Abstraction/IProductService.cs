public interface IProductService
{
    Task<IEnumerable<ProductEntity>> GetAllProductsAsync();
    Task<ProductEntity?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductHistory>> GetProductHistoryAsync(int id);
    Task<ProductEntity> CreateProductAsync(ProductEntity product);
    Task UpdateProductAsync(int id, ProductEntity product);
    Task DeleteProductAsync(int id);
}