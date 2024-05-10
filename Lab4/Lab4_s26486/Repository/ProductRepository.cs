using Lab4_s26486.Models;
using System.Data.SqlClient;

namespace Lab4_s26486.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Product GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Product WHERE IdProduct = @IdProduct";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdProduct", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                IdProduct = Convert.ToInt32(reader["IdProduct"]),
                                Name = reader["Name"]?.ToString(),
                                Description = reader["Description"]?.ToString(),
                                Price = Convert.ToDecimal(reader["Price"])
                            };
                        }
                        return null;
                    }
                }
            }
        }

    }
}