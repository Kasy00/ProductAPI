using Microsoft.Extensions.Options;

public class ProductNameSpecification : ISpecification<Product>
{
    private readonly int _minLength;
    private readonly int _maxLength;

    public ProductNameSpecification(IOptions<ProductValidationOptions> options)
    {
        _minLength = options.Value.MinNameLength;
        _maxLength = options.Value.MaxNameLength;
    }

    public string ErrorMessage => "Name of the product has to be between 3 and 20 characters and contain only letters and digits.";

    public bool IsSatisfiedBy(Product product)
    {
        if (product.Name.Length < _minLength || product.Name.Length > _maxLength || !product.Name.All(char.IsLetterOrDigit))
        {
            return false;
        }
        return true;
    }
}

public class ProductPriceSpecification : ISpecification<Product>
{
    private static readonly Dictionary<string, (decimal minPrice, decimal maxPrice)> _categoryPriceRanges = new ()
    {
        { "Elektronika", (50, 50000) },
        { "Książki", (5, 500) },
        { "Odzież", (10, 5000) }
    };

    public string ErrorMessage => "The price for this category is wrong.";

    public bool IsSatisfiedBy(Product product)
    {
        if (_categoryPriceRanges.TryGetValue(product.Category, out var range))
        {
            return product.Price >= range.minPrice && product.Price <= range.maxPrice;
        }

        return false;
    }
}

public class ProductQuantitySpecification : ISpecification<Product>
{
    private readonly int _minQuantity;

    public ProductQuantitySpecification(IOptions<ProductValidationOptions> options)
    {
        _minQuantity = options.Value.MinQuantity;
    }

    public string ErrorMessage => "Quantity of products has to be positive number not less than zero.";

    public bool IsSatisfiedBy(Product product)
    {
        return product.Quantity >= _minQuantity;
    }
}