using Lab4_s26486.Models;

namespace Lab4_s26486.Services
{
    public interface IInventoryService 
    {
        void AddProductToWarehouse(InventoryRequest inventoryRequest);
    }
}