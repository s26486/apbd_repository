using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repositories;

public class ProductWarehouseRepository : IProduct_WarehouseRepository
{
    private readonly IConfiguration _configuration;

    public ProductWarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<ProductWarehouse> GetProduct_WarehouseByIdOrdered(int IdOrder)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        using var command = new SqlCommand("SELECT * FROM Product_Warehouse WHERE IdOrder = @IdOrder", connection);
        command.Parameters.AddWithValue("@IdOrder", IdOrder);
    
        using var reader = await command.ExecuteReaderAsync();
        if (reader.Read())
        {
            return new ProductWarehouse
            {
                IdProductWarehouse = Convert.ToInt32(reader["IdProductWarehouse"]),
                IdWarehouse = Convert.ToInt32(reader["IdWarehouse"]),
                IdProduct = Convert.ToInt32(reader["IdProduct"]),
                IdOrder = Convert.ToInt32(reader["IdOrder"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
            };
        }
        return null;
    }

    public async Task<int?> RegisterProductInProductWarehouseAsync(int? IdWarehouse, int? IdProduct, int? IdOrder, int amount, decimal Price, DateTime CreatedAt)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        using var command = new SqlCommand(@"
            INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
            OUTPUT Inserted.IdProductWarehouse
            VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)", connection);
        command.Parameters.AddWithValue("@IdWarehouse", IdWarehouse);
        command.Parameters.AddWithValue("@IdProduct", IdProduct);
        command.Parameters.AddWithValue("@IdOrder", IdOrder);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@Price", Price);
        command.Parameters.AddWithValue("@CreatedAt", CreatedAt);
    
        var idProductWarehouse = (int?)await command.ExecuteScalarAsync();
        return idProductWarehouse;
    }

    public async Task<int?> RegisterProductInWarehouseByStoredProc(int? idWarehouse, int? Idproduct, int amount,
        DateTime CreatedAt)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        using var command = new SqlCommand("AddProductToWarehouse", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        command.Parameters.AddWithValue("@IdProduct", Idproduct);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@CreatedAt", CreatedAt);

        var result = await command.ExecuteScalarAsync();
        return (int?)result;
    }
}