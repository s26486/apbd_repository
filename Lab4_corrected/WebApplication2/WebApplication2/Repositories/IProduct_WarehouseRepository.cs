using System.Runtime.InteropServices.JavaScript;
using WebApplication2.Models;

namespace WebApplication2.Repositories;

public interface IProduct_WarehouseRepository
{
    Task<ProductWarehouse> GetProduct_WarehouseByIdOrdered(int OrderId);
    Task<int?> RegisterProductInProductWarehouseAsync(int? IdWarehouse, int? IdProduct, int? IdOrder, int amount, decimal Price, DateTime CreatedAt);
    Task<int?> RegisterProductInWarehouseByStoredProc(int? idWarehouse, int? Idproduct, int amount, DateTime CreatedAt);
}