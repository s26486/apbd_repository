using WebApplication2.Models;

namespace WebApplication2.Repositories;

public interface IOrderRepository
{
    public Task<Order> GetOrderByProductId(int IdProduct, int amount);
    public void UpdateFullFilledAtById(int IdOrder);
}