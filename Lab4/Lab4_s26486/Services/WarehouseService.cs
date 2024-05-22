using Lab4_s26486.Repository;

namespace Lab4_s26486.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public bool Exists(int id)
    {
        return _warehouseRepository.GetById(id) != null;
    }
}