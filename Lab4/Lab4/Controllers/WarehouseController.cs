using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly string _connectionString = "Server=localhost;Database=master;User Id=SA;Password=MyPass@word;";

        [HttpPost]
        [Route("AddProductToWarehouse")]
        public IActionResult AddProductToWarehouse([FromBody] InventoryRequest inventoryRequest)
        {
            try
            {
                // Nawiąż połączenie z bazą danych
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Utwórz obiekt SqlCommand reprezentujący procedurę przechowywaną
                    using (var command = new SqlCommand("AddProductToWarehouse", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Ustaw parametry procedury przechowywanej
                        command.Parameters.AddWithValue("@IdProduct", inventoryRequest.IdProduct);
                        command.Parameters.AddWithValue("@IdWarehouse", inventoryRequest.IdWarehouse);
                        command.Parameters.AddWithValue("@Amount", inventoryRequest.Amount);
                        command.Parameters.AddWithValue("@CreatedAt", inventoryRequest.CreatedAt);

                        // Wykonaj procedurę przechowywaną
                        command.ExecuteNonQuery();

                        // Zwróć odpowiedź HTTP 200 OK w przypadku sukcesu
                        return Ok("Produkt został pomyślnie dodany do magazynu.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Obsłuż ewentualny błąd i zwróć odpowiednią odpowiedź HTTP
                return StatusCode(500, $"Wystąpił błąd podczas dodawania produktu do magazynu: {ex.Message}");
            }
        }
    }
}

public class InventoryRequest
{
    public int IdProduct { get; set; }
    public int IdWarehouse { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
