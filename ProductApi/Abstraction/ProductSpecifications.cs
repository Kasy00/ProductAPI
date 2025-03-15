public class ProductNameSpecification : ISpecification<ProductEntity>
{
    private readonly IProductRepository _productRepository;

    public ProductNameSpecification(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public string ErrorMessage => "Name of the product has to be between 3 and 20 characters and contain only letters and digits.";

    public bool IsSatisfiedBy(ProductEntity productEntity)
    {
        if (productEntity.Name.Length < 3 || productEntity.Name.Length > 20 || !productEntity.Name.All(char.IsLetterOrDigit))
        {
            return false;
        }
        return true;
    }
}

public class ProductPriceSpecification : ISpecification<ProductEntity>
{
    public string ErrorMessage => "The price for this category is wrong.";

    public bool IsSatisfiedBy(ProductEntity productEntity)
    {
        return productEntity.Category switch
        {
            "Elektronika" => productEntity.Price >= 50 && productEntity.Price <= 50000,
            "Książki" => productEntity.Price >= 5 && productEntity.Price <= 500,
            "Odzież" => productEntity.Price >= 10 && productEntity.Price <= 5000,
            _ => false
        };
    }
}

public class ProductQuantitySpecification : ISpecification<ProductEntity>
{
    public string ErrorMessage => "Quantity of products has to be positive number not less than zero.";

    public bool IsSatisfiedBy(ProductEntity productEntity)
    {
        return productEntity.Quantity >= 0;
    }
}