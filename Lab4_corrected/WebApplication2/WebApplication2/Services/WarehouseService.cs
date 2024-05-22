using WebApplication2.Dto;
using WebApplication2.Exceptions;
using WebApplication2.Repositories;

namespace WebApplication2.Services;

public interface IWarehouseService
{
    public Task<int> RegisterProductInWarehouseAsync(RegisterProductInWarehouseRequestDTO dto);
    public Task<int> RegisterProductInWarehouseByStoredProcedureAsync(RegisterProductInWarehouseRequestDTO dto);
}

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProduct_WarehouseRepository _productWarehouseRepository;

    public WarehouseService(
        IWarehouseRepository warehouseRepository, 
        IProductRepository productRepository, 
        IOrderRepository orderRepository, 
        IProduct_WarehouseRepository productWarehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _productWarehouseRepository = productWarehouseRepository;
    }

    public async Task<int> RegisterProductInWarehouseAsync(RegisterProductInWarehouseRequestDTO dto)
    {
        // Check if product exists else throw NotFoundException
        var product = await _productRepository.GetById(dto.IdProduct.Value);
        if (product == null)
            throw new NotFoundException("Product not found");

        // Check if warehouse exists else throw NotFoundException
        var warehouse = await _warehouseRepository.GetById(dto.IdWarehouse.Value);
        if (warehouse == null)
            throw new NotFoundException("Warehouse not found");

        // Get order if exists else throw NotFoundException
        var order = await _orderRepository.GetOrderByProductId(dto.IdProduct.Value, dto.Amount.Value);
        if (order == null)
            throw new NotFoundException("Order not found");

        // Check if product is already in warehouse else throw ConflictException
        var existingProductWarehouse = await _productWarehouseRepository.GetProduct_WarehouseByIdOrdered(order.IdOrder);
        if (existingProductWarehouse != null)
            throw new ConflictException("Product already in warehouse");

        var idProductWarehouse = await _warehouseRepository.RegisterProductInWarehouseAsync(
            idWarehouse: dto.IdWarehouse!.Value,
            idProduct: dto.IdProduct!.Value,
            idOrder: order.IdOrder,
            createdAt: DateTime.UtcNow);

        if (!idProductWarehouse.HasValue)
            throw new Exception("Failed to register product in warehouse");

        return idProductWarehouse.Value;
    }

    public async Task<int> RegisterProductInWarehouseByStoredProcedureAsync(RegisterProductInWarehouseRequestDTO dto)
    {
        // Check if product exists else throw NotFoundException
        var product = await _productRepository.GetById(dto.IdProduct.Value);
        if (product == null)
            throw new NotFoundException("Product not found");

        // Check if warehouse exists else throw NotFoundException
        var warehouse = await _warehouseRepository.GetById(dto.IdWarehouse.Value);
        if (warehouse == null)
            throw new NotFoundException("Warehouse not found");

        // Get order if exists else throw NotFoundException
        var order = await _orderRepository.GetOrderByProductId(dto.IdProduct.Value, dto.Amount.Value);
        if (order == null)
            throw new NotFoundException("Order not found");

        var idProductWarehouse = await _warehouseRepository.RegisterProductInWarehouseByProcedureAsync(
            idWarehouse: dto.IdWarehouse!.Value,
            idProduct: dto.IdProduct!.Value,
            amount: dto.Amount.Value,
            createdAt: DateTime.UtcNow);

        if (!idProductWarehouse.HasValue)
            throw new Exception("Failed to register product in warehouse");

        return idProductWarehouse.Value;
    }
}
