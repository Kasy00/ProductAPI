public interface IProductRepository : IGenericRepository<ProductEntity>
{
    Task<bool> IsNameUniqueAsync(string name);
    Task TrackProductHistory(ProductEntity oldProduct, ProductEntity newProduct);
    Task<IEnumerable<ProductHistory>> GetProductHistoryAsync(int productId);
}