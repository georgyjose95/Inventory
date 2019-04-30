using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace Store.RepositoryLayer
{
    public class InventoryDBRepository
    {
        private string _connectionString;
        public InventoryDBRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection GetSQLConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }

        public DbActionResult AddInventory(Inventory inventory)
        {
            DbActionResult actionResult = new DbActionResult() { Success = true, Message =  "Inventory Added!"};

            try
            {

                using (SqlConnection sqlConnection = GetSQLConnection())
                {
                    sqlConnection.Open();
                    String cmdText = $@"INSERT INTO [dbo].[Inventory] 
                                   ([InventoryId]
                                   ,[Name]
                                   ,[Quantity])
                                         VALUES
                                   (@inventoryId,
                                    @name, 
                                    @quantity)";

                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("inventoryId", inventory.InventoryId);
                        sqlCommand.Parameters.AddWithValue("name", inventory.Name);
                        sqlCommand.Parameters.AddWithValue("quantity", inventory.Quantity);

                        sqlCommand.ExecuteNonQuery();

                    }

                }


            }
            catch (SqlException ex)
            {           
                actionResult.Success = false;
                actionResult.Message = "Inventory Not Added!, Unable to connect";
            }
            catch (Exception ex)
            {
                actionResult.Success = false;
                actionResult.Message = "An error occurred while processing the request";
            }
            return actionResult;
        }
        public DbActionResult UpdateInventory(Inventory inventory)
        {
            DbActionResult actionResult = new DbActionResult() { Success = true, Message = "Inventory Updated!" };

            try
            {
                using (SqlConnection sqlConnection = GetSQLConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"UPDATE [dbo].[Inventory]
                                        SET [InventoryId] = @inventoryId,
                                            [Name] = @name,
                                            [Quantity] = @quantity
                                            WHERE [InventoryId] = @inventoryId";

                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("inventoryId", inventory.InventoryId);
                        sqlCommand.Parameters.AddWithValue("name", inventory.Name);
                        sqlCommand.Parameters.AddWithValue("quantity", inventory.Quantity);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch(SqlException sqlExecption)
            {
                actionResult.Success = false;
                actionResult.Message = "Update not successful, Internal server error!";
            }
            catch (Exception ex)
            {
                actionResult.Success = false;
                actionResult.Message = "Update unsuccessful!, Query not processed";
            }
            return actionResult;
            
        }
        public DbActionResult DeleteInventory(Guid inventoryId)
        {
            DbActionResult actionResult = new DbActionResult { Success = true, Message = "Record Sucessfully Deleted!" };
            try
            {
                using(SqlConnection sqlConnection = GetSQLConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"DELETE FROM [dbo].[Inventory]
                                            WHERE [InventoryId] = @inventoryId";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("inventoryId", inventoryId);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch(SqlException sqlExecption)
            {
                actionResult.Success = false;
                actionResult.Message = "Internal server Error!, Deletion unsuccessful!";

            }
            catch(Exception execption)
            {
                actionResult.Success = false;
                actionResult.Message = "Query processing Error!, Deletion unsuccessful";
            }
            return actionResult;

        }
        public Inventory GetInventoriesById(Guid inventoryId)
        {
            Inventory retrievedInventory = new Inventory();
            DbActionResult actionResult = new DbActionResult { Success = true, Message = "Inventoried retireved based on Id" };
            try
            {
                using (SqlConnection sqlConnection = GetSQLConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"SELECT [InventoryId],[Name],[Quantity]
                                                    FROM[dbo].[Inventory]
                                                    WHERE [InventoryId] = @inventoryId";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("inventoryId", inventoryId);
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            int colInventoryId = dataReader.GetOrdinal("InvenotryId");
                            int colName        = dataReader.GetOrdinal("Name");
                            int colQuanity     = dataReader.GetOrdinal("Quanitity"); 
                            dataReader.Read();
                            retrievedInventory.InventoryId = dataReader.GetGuid(colInventoryId);
                            retrievedInventory.Name        = dataReader.GetString(colName);
                            retrievedInventory.Quantity = dataReader.GetInt32(colQuanity);
                        }
                    }
                }
            }
            catch (SqlException sqlExecption)
            {
                actionResult.Success = false;
                actionResult.Message = "Internal server Error!, Search unsuccessful! ";

            }
            catch (Exception execption)
            {
                actionResult.Success = false;
                actionResult.Message = "Query processing Error!, Search unsuccessful";
            }
            DbActionResult setResult = actionResult;
            return retrievedInventory;

        }
        public List<Inventory> GetByName(String searchTerm)
        {

            List<Inventory> retrievedInventories = new List<Inventory>();
            DbActionResult actionResult = new DbActionResult { Success = true, Message = "Inventoried retireved based on Id" };
            try
            {
                using (SqlConnection sqlConnection = GetSQLConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"SELECT [InventoryId],[Name],[Quantity]
                                                    FROM[dbo].[Inventory]
                                                    WHERE [Name]= @searchTerm";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("searchTerm", searchTerm);
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            int colInventoryId = dataReader.GetOrdinal("InventoryId");
                            int colName = dataReader.GetOrdinal("Name");
                            int colQuantity = dataReader.GetOrdinal("Quantity");

                            while (dataReader.Read())
                            {
                                Inventory dbInventory = new Inventory();
                                dbInventory.InventoryId = dataReader.GetGuid(colInventoryId);
                                dbInventory.Name = dataReader.GetName(colName);
                                dbInventory.Quantity = dataReader.GetInt32(colQuantity);

                                retrievedInventories.Add(dbInventory);

                            }

                        }

                    }
                }
            }
            catch (SqlException sqlExecption)
            {
                actionResult.Success = false;
                actionResult.Message = "Internal server Error!, Search unsuccessful! ";

            }
            catch (Exception execption)
            {
                actionResult.Success = false;
                actionResult.Message = "Query processing Error!, Search unsuccessful";
            }
            DbActionResult setResult = actionResult;
            return retrievedInventories;
        }
        public List<Inventory> GetAllInventories()
        {
            List<Inventory> retrievedInventories = new List<Inventory>();
            DbActionResult actionResult = new DbActionResult { Success = true, Message = "Inventoried retireved based on Id" };
            try
            {
                using (SqlConnection sqlConnection = GetSQLConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"SELECT * FROM[dbo].[Inventory]";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            int colInventoryId = dataReader.GetOrdinal("InventoryId");
                            int colName = dataReader.GetOrdinal("Name");
                            int colQuantity = dataReader.GetOrdinal("Quantity");
                            while (dataReader.Read())
                            {
                                Inventory dbInventory = new Inventory();
                                dbInventory.InventoryId = dataReader.GetGuid(colInventoryId);
                                dbInventory.Name = dataReader.GetName(colName);
                                dbInventory.Quantity = dataReader.GetInt32(colQuantity);
                                retrievedInventories.Add(dbInventory);
                               
                            }
                        }                        
                    }
                }
            }
            catch (SqlException sqlExecption)
            {
                actionResult.Success = false;
                actionResult.Message = "Internal server Error!, Search unsuccessful! ";

            }
            catch (Exception execption)
            {
                actionResult.Success = false;
                actionResult.Message = "Query processing Error!, Search unsuccessful";
            }
            DbActionResult setResult = actionResult;
            return retrievedInventories;
        }
    }
}

