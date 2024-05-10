using Lab4_s26486.Models;

namespace Lab4_s26486.Repository;

public interface IWarehouseRepository
{
    Warehouse GetById(int id);
}