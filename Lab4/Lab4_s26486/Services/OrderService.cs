using Lab4_s26486.Repository;

namespace Lab4_s26486.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public bool Exists(int id)
    {
        return true;
    }

    public bool IsFullFilled(int id)
    {
        return true;
    } 
}