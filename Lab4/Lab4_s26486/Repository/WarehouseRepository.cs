using Lab4_s26486.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace Lab4_s26486.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly string _connectionString;

        public WarehouseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Warehouse GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdWarehouse", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Warehouse
                            {
                                IdWarehouse = Convert.ToInt32(reader["IdWarehouse"]),
                                Name = reader["Name"]?.ToString(),
                                Address = reader["Address"]?.ToString()
                            };
                        }
                        return null;
                    }
                }
            }
        }
    }
}