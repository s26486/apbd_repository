namespace Lab4_s26486.Models;

public class InventoryRequest
{
    public int IdProduct { get; set; }
    public int IdWarehouse { get; set; }
    public int IdOrder { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    
}