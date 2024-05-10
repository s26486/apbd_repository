using Lab4_s26486.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Lab4_s26486.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Order GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM \"Order\" WHERE IdOrder = @IdOrder";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdOrder", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Order
                            {
                                IdOrder = Convert.ToInt32(reader["IdOrder"]),
                                IdProduct = Convert.ToInt32(reader["IdProduct"]),
                                Amount = Convert.ToInt32(reader["Amount"]),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                FullFilledAt = reader["FulfilledAt"] != DBNull.Value ? Convert.ToDateTime(reader["FulfilledAt"]) : default(DateTime?)
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public void UpdateFullFilledAt(int orderId, DateTime fulfilledAt)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE \"Order\" SET FulfilledAt = @FulfilledAt WHERE IdOrder = @IdOrder";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdOrder", orderId);
                    command.Parameters.AddWithValue("@FulfilledAt", fulfilledAt);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
