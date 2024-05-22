using WebApplication2.Models;

namespace WebApplication2.Repositories;

public interface IProductRepository
{
    public Task<Product> GetById(int IdProduct);
}