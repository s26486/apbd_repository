using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repositories;

public class OrderRepository : IOrderRepository
{ 
    private readonly IConfiguration _configuration;

    public OrderRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public async Task<Order> GetOrderByProductId(int IdProduct, int amount)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        using var command = new SqlCommand("SELECT IdOrder, IdProduct, Amount, CreatedAt FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount", connection);
        command.Parameters.AddWithValue("@IdProduct", IdProduct);
        command.Parameters.AddWithValue("@Amount", amount);

        using var reader = await command.ExecuteReaderAsync();
        if (reader.Read())
        {
            return new Order
            {
                IdOrder = Convert.ToInt32(reader["IdOrder"]),
                IdProduct = Convert.ToInt32(reader["IdProduct"]),
                Amount = Convert.ToInt32(reader["Amount"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
            };
        }
        return null;
    }

    public void UpdateFullFilledAtById(int IdOrder)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
    
        using var command = new SqlCommand("UPDATE [Order] SET FulfilledAt = @FulfilledAt WHERE IdOrder = @IdOrder", connection);
        command.Parameters.AddWithValue("@FulfilledAt", DateTime.UtcNow);
        command.Parameters.AddWithValue("@IdOrder", IdOrder);
    
        command.ExecuteNonQuery();
    }

}