using Lab4_s26486.Repository;

namespace Lab4_s26486.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool Exists(int id)
    {
        return true;
    }
}