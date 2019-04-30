using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Store.RepositoryLayer
{
    class OrderDbRepository
    {
        private string _connectionString;
        public OrderDbRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection GetSqlConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }
        public DbActionResult AddOrder(Order order)
        {
            DbActionResult dbActionResult = new DbActionResult() { Success = true, Message = "Order added successfully!" };
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"INSERT INTO [dbo].[Order]
                                               ([OrderId]
                                               ,[UserId]
                                               ,[OrderDateTime])
                                         VALUES
                                               (@orderId, @userId, @orderDate)";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText,sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("orderId", order.OrderId);
                        sqlCommand.Parameters.AddWithValue("userId", order.UserId);
                        sqlCommand.Parameters.AddWithValue("orderDate", order.OrderDate);
                        sqlCommand.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "Order Not Added!, Unable to connect";
            }
            catch (Exception ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "An error occurred while processing the request";
            }
            return dbActionResult;
        }
        public DbActionResult UpdateOrder(Order order)
        {
            DbActionResult dbActionResult = new DbActionResult() { Success = true, Message = "Order added successfully!" };
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"UPDATE [dbo].[Order]
                               SET [OrderId] = @orderId
                                  ,[UserId] = @userId
                                  ,[OrderDateTime] =@orderDateTime
                             WHERE [orderId] = @orderId";
                    using(SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("orderId", order.OrderId);
                        sqlCommand.Parameters.AddWithValue("userId", order.UserId);
                        sqlCommand.Parameters.AddWithValue("orderDateTime", order.OrderDate);
                        sqlCommand.ExecuteNonQuery();

                    }

                }
            }
            catch (SqlException ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "Order Not Added!, Unable to connect";
            }
            catch (Exception ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "An error occurred while processing the request";
            }
            return dbActionResult;
        }
        public DbActionResult DeleteOrder(Guid orderId)
        {
            DbActionResult dbActionResult = new DbActionResult() { Success = true, Message = "Order added successfully!" };
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"DELETE FROM [dbo].[Order]
                                           WHERE [OrderId] = orderId";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("orderId", orderId);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "Order Not Added!, Unable to connect";
            }
            catch (Exception ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "An error occurred while processing the request";
            }
            return dbActionResult;

        }
       /* public Order GetOrder()
        {
            List<Order> orderList = new List<Order>();

        }
        */
    }
}
