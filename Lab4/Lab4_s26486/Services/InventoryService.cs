using Lab4_s26486.Models;
using Lab4_s26486.Repository;

namespace Lab4_s26486.Services;

public class InventoryService : IInventoryService
{
    // private readonly ProductService _productService;
    // private readonly OrderService _orderService;
    // private readonly WarehouseService _warehouseService;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductWarehouseRepository _productWarehouseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;

    public InventoryService(
        // ProductService productService,
        // OrderService orderService,
        // WarehouseService warehouseService,
        IOrderRepository orderRepository,
        IProductWarehouseRepository productWarehouseRepository,
        IProductRepository productRepository,
        IWarehouseRepository warehouseRepository)
    {
        // _productService = productService;
        // _orderService = orderService;
        // _warehouseService = warehouseService;
        _orderRepository = orderRepository;
        _productWarehouseRepository = productWarehouseRepository;
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
    }

    public void AddProductToWarehouse(InventoryRequest inventoryRequest)
    {
        // if (!_productService.Exists(inventoryRequest.IdProduct))
        //     throw new Exception("Nie znaleziono produtku o podanym id");
        //
        // if (!_warehouseService.Exists(inventoryRequest.IdWarehouse))
        //     throw new Exception("Nie znaleziono magayznu o podanym id");

        if (inventoryRequest.Amount <= 0 || inventoryRequest == null)
            throw new Exception("Niepoprawna ilosc");
        
        //
        // if (!_orderService.Exists(inventoryRequest.IdOrder))
        //     throw new Exception("Nie znaleziono zamowienia o podanym id");

        _orderRepository.UpdateFullFilledAt(inventoryRequest.IdOrder, DateTime.Now);
        var productWarehouse = new ProductWarehouse
        {
            IdWarehouse = inventoryRequest.IdWarehouse,
            IdProduct = inventoryRequest.IdProduct,
            IdOrder = inventoryRequest.IdOrder,
            Amount = inventoryRequest.Amount,
            Price = CalculatePrice(inventoryRequest.IdProduct),
            CreatedAt = DateTime.Now
        };
        _productWarehouseRepository.Add(productWarehouse);
    }

    private decimal CalculatePrice(int productId)
    {
        var product = _productRepository.GetById(productId);

        if (product != null)
        {
            return product.Price;
        }
        else
        {
            throw new Exception($"Nie znaleziono produktu o id :  {productId}");
        }
    }
}