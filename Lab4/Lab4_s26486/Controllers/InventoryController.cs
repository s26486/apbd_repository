using Lab4_s26486.Models;
using Lab4_s26486.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab4_s26486.Inventory;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly InventoryService _inventoryService;

    public InventoryController(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }
        
    [HttpPost]
    [Route("AddProductToWarehouse")]
    public IActionResult AddProductToWarehouse([FromBody] InventoryRequest inventoryRequest)
    {
        try
        {
            _inventoryService.AddProductToWarehouse(inventoryRequest);
            return Ok("Dodano product do magazynu");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error; {ex.Message}");
        }
    }
}