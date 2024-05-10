using Lab4_s26486.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace Lab4_s26486.Repository
{
    public class ProductWarehouseRepository : IProductWarehouseRepository
    {
        private readonly string _connectionString;

        public ProductWarehouseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Add(ProductWarehouse productWarehouse)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) 
                              VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
                    command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
                    command.Parameters.AddWithValue("@IdOrder", productWarehouse.IdOrder);
                    command.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
                    command.Parameters.AddWithValue("@Price", productWarehouse.Price);
                    command.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}