using Microsoft.Extensions.Options;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IForbiddenPhraseRepository _forbiddenPhraseRepository;
    private readonly List<ISpecification<Product>> _specifications;

    public ProductService(IProductRepository productRepository, IForbiddenPhraseRepository forbiddenPhraseRepository, IOptions<ProductValidationOptions> productValidationOptions)
    {
        _productRepository = productRepository;
        _forbiddenPhraseRepository = forbiddenPhraseRepository;
        _specifications = new List<ISpecification<Product>>()
        {
            new ProductNameSpecification(productValidationOptions),
            new ProductPriceSpecification(),
            new ProductQuantitySpecification(productValidationOptions)
        };
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync(p => new
        {
            p.Id,
            p.Name,
            p.Price
        });
        
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price
        });
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ProductHistory>> GetProductHistoryAsync(int id)
    {
        return await _productRepository.GetProductHistoryAsync(id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        var forbiddenPhrases = await _forbiddenPhraseRepository.GetAllForbiddenPhrasesAsync();
        var matchedForbiddenPhrases = forbiddenPhrases
            .Where(phrase => product.Name.Contains(phrase, StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        if (matchedForbiddenPhrases.Any())
        {
            var errorMessage = $"Product name contains the following forbidden phrases: {string.Join(", ", matchedForbiddenPhrases)}";
            throw new ArgumentException(errorMessage);
        }

        if (!await _productRepository.IsNameUniqueAsync(product.Name))
        {
            throw new ArgumentException("Name is not unique.");
        }

        if (!Validate(product, out var errors))
        {
            throw new ArgumentException(string.Join(", ", errors));
        }

        return await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(int id, Product product)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            throw new ArgumentException("Product does not exist.");
        }

        var forbiddenPhrases = await _forbiddenPhraseRepository.GetAllForbiddenPhrasesAsync();
        var matchedForbiddenPhrases = forbiddenPhrases
            .Where(phrase => product.Name.Contains(phrase, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (matchedForbiddenPhrases.Any())
        {
            var errorMessage = $"Product name contains the following forbidden phrases: {string.Join(", ", matchedForbiddenPhrases)}";
            throw new ArgumentException(errorMessage);
        }

        if (!Validate(product, out var errors))
        {
            throw new ArgumentException(string.Join(", ", errors));
        }

        await _productRepository.TrackProductHistory(existingProduct, product);

        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public bool Validate(Product product, out List<string> errors)
    {
        errors = _specifications
            .Where(spec => !spec.IsSatisfiedBy(product))
            .Select(spec => spec.ErrorMessage)
            .ToList();

        return !errors.Any();
    }
}