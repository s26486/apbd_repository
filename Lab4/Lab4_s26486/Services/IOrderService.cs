using Lab4_s26486.Repository;

namespace Lab4_s26486.Services
{
    public interface IOrderService
    {
        bool Exists(int id);
        bool IsFullFilled(int id);
    }
}