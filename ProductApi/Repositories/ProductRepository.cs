using Microsoft.EntityFrameworkCore;

public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(ProductDbContext context) : base(context) { }

    public async Task<bool> IsNameUniqueAsync(string name)
    {
        return !await _context.Products.AnyAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<ProductHistory>> GetProductHistoryAsync(int productId)
    {
        return await _context.ProductsHistories
            .Where(ph => ph.ProductId == productId)
            .Include(ph => ph.Product)
            .OrderByDescending(ph => ph.ChangeDate)
            .ToListAsync();
    }

    public async Task TrackProductHistory(ProductEntity oldProduct, ProductEntity newProduct)
    {
        var changes = new List<ProductHistory>();

        if (oldProduct.Name != newProduct.Name)
        {
            changes.Add(new ProductHistory
            {
                ProductId = oldProduct.Id,
                PropertyName = "Name",
                OldValue = oldProduct.Name,
                NewValue = newProduct.Name,
                ChangeDate = DateTime.UtcNow
            });
        }

        if (oldProduct.Price != newProduct.Price)
        {
            changes.Add(new ProductHistory
            {
                ProductId = oldProduct.Id,
                PropertyName = "Price",
                OldValue = oldProduct.Price.ToString(),
                NewValue = newProduct.Price.ToString(),
                ChangeDate = DateTime.UtcNow
            });
        }

        if (oldProduct.Quantity != newProduct.Quantity)
        {
            changes.Add(new ProductHistory
            {
                ProductId = oldProduct.Id,
                PropertyName = "Quantity",
                OldValue = oldProduct.Quantity.ToString(),
                NewValue = newProduct.Quantity.ToString(),
                ChangeDate = DateTime.UtcNow
            });
        }

        if (oldProduct.Category != newProduct.Category)
        {
            changes.Add(new ProductHistory
            {
                ProductId = oldProduct.Id,
                PropertyName = "Category",
                OldValue = oldProduct.Category,
                NewValue = newProduct.Category,
                ChangeDate = DateTime.UtcNow
            });
        }

        if (changes.Any())
        {
            _context.ProductsHistories.AddRange(changes);
            await _context.SaveChangesAsync();
        }
    }
}