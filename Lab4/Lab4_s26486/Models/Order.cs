namespace Lab4_s26486.Models;

public class Order
{
    public int IdOrder { get; set; }
    public int IdProduct { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FullFilledAt { get; set; }
}