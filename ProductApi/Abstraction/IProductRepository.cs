public interface IProductRepository : IGenericRepository<Product>
{
    Task<bool> IsNameUniqueAsync(string name);
    Task TrackProductHistory(Product oldProduct, Product newProduct);
    Task<IEnumerable<ProductHistory>> GetProductHistoryAsync(int productId);
}