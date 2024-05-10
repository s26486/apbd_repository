using Lab4_s26486.Models;

namespace Lab4_s26486.Repository;

public interface IOrderRepository
{
    Order GetById(int id);
    void UpdateFullFilledAt(int orderId, DateTime fulfilledAt);
    
}