using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Store.RepositoryLayer
{
    public class UserDbRepository
    {
        private string _connectionString;
        public UserDbRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection GetSqlConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }
        public DbActionResult AddUser(User user)
        {
            DbActionResult dbActionResult = new DbActionResult() { Success = true, Message = "User Added Sucessfully!" };
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"INSERT INTO [dbo].[Users]
                                             ([UserId]
                                            ,[UserName]
                                            ,[Password]
                                            ,[FirstName]
                                            ,[LastName]
                                            ,[Email]
                                            ,[Membership])
                                        VALUES (
                                             @userId
                                             ,@username
                                             ,@password
                                             ,@firstName
                                             ,@lastName
                                             ,@email
                                             ,@membership)";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("userId", user.UserId);
                        sqlCommand.Parameters.AddWithValue("username", user.UserName);
                        sqlCommand.Parameters.AddWithValue("password", user.Password);
                        sqlCommand.Parameters.AddWithValue("firstname", user.FirstName);
                        sqlCommand.Parameters.AddWithValue("lastname", user.LastName);
                        sqlCommand.Parameters.AddWithValue("email", user.Email);
                        sqlCommand.Parameters.AddWithValue("membership", user.Membership);
                        sqlCommand.ExecuteNonQuery();


                    }
                }
            }
            catch (SqlException ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "User Not Added!, Unable to connect";
            }
            catch (Exception ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "An error occurred while processing the request";
            }
            return dbActionResult;
        }
        public DbActionResult UpdateUser(User user)
        {
            DbActionResult dbActionResult = new DbActionResult { Success = true, Message = "User updated sucessfully!" };
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"UPDATE [dbo].[Users]
                                            SET
                                                   [UserId] = @userId
                                                  ,[UserName] = @username
                                                  ,[Password] = @password
                                                  ,[FirstName] = @firstname
                                                  ,[LastName] = @lastname
                                                  ,[Email] = @email
                                                  ,[Membership] = @membership
                                             WHERE [UserId] = @userId";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("userId", user.UserId);
                        sqlCommand.Parameters.AddWithValue("username", user.UserName);
                        sqlCommand.Parameters.AddWithValue("password", user.Password);
                        sqlCommand.Parameters.AddWithValue("firstname", user.FirstName);
                        sqlCommand.Parameters.AddWithValue("lastname", user.LastName);
                        sqlCommand.Parameters.AddWithValue("email", user.Email);
                        sqlCommand.Parameters.AddWithValue("membership", user.Membership);
                        sqlCommand.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "User Not Updated!, Unable to connect";
            }
            catch (Exception ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "An error occurred while processing the request";
            }
            return dbActionResult;
        } 
        public DbActionResult DeleteUser(Guid userId)
        {
            DbActionResult dbActionResult = new DbActionResult { Success = true, Message = "User deleted sucessfully!" };
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"DELETE FROM [dbo].[Users]
                                            WHERE [UserId] = @userId";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("userId", userId);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "User Not Deleted!, Unable to connect";
            }
            catch (Exception ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "An error occurred while processing the request";
            }
            return dbActionResult;
        }
        public User GetUserById(Guid userId)
        {
            DbActionResult dbActionResult = new DbActionResult { Success = true, Message = "User Data Retrieved sucessfully!" };
            User userInfo = new User();
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    sqlConnection.Open();
                    String commandText = @"SELECT * FROM [dbo].[Users]
                                           WHERE [UserId] = @userId";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("userId", userId);

                        using(SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            int colUserId = dataReader.GetOrdinal("UserId");
                            int colUserName = dataReader.GetOrdinal("UserName");
                            int colPassword = dataReader.GetOrdinal("Password");
                            int colFirstName = dataReader.GetOrdinal("FirstName");
                            int colLastName = dataReader.GetOrdinal("LastName");
                            int colEmail = dataReader.GetOrdinal("Email");
                            int colMembership = dataReader.GetOrdinal("Membership");

                            dataReader.Read();

                            userInfo.UserId = dataReader.GetGuid(colUserId);
                            userInfo.UserName = dataReader.GetString(colUserName);
                            userInfo.Password = dataReader.GetString(colPassword);
                            userInfo.FirstName = dataReader.GetString(colFirstName);
                            userInfo.LastName = dataReader.GetString(colLastName);
                            userInfo.Email = dataReader.GetString(colEmail);
                            userInfo.Membership = dataReader.GetInt32(colMembership);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "User Not Deleted!, Unable to connect";
            }
            catch (Exception ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "An error occurred while processing the request";
            }
            return userInfo;
        }
        public DbActionResult<List<User>> GetAllUsers()
        {
           
            DbActionResult<List<User>> dbActionResult = new DbActionResult<List<User>> { Success = false, Data = new List<User>()};

            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    sqlConnection.Open();
                    String commandText = "SELECT * FROM [dbo].[Users]";                                           
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            int colUserId = dataReader.GetOrdinal("UserId");
                            int colUserName = dataReader.GetOrdinal("UserName");
                            int colPassword = dataReader.GetOrdinal("Password");
                            int colFirstName = dataReader.GetOrdinal("FirstName");
                            int colLastName = dataReader.GetOrdinal("LastName");
                            int colEmail = dataReader.GetOrdinal("Email");
                            int colMembership = dataReader.GetOrdinal("Membership");
                            while (dataReader.Read())
                            {
                                User userInfo = new User();
                                userInfo.UserId = dataReader.GetGuid(colUserId);
                                userInfo.UserName = dataReader.GetString(colUserName);
                                userInfo.Password = dataReader.GetString(colPassword);
                                userInfo.FirstName = dataReader.GetString(colFirstName);
                                userInfo.LastName = dataReader.GetString(colLastName);
                                userInfo.Email = dataReader.GetString(colEmail);
                                userInfo.Membership = dataReader.GetInt32(colMembership);
                                dbActionResult.Data.Add(userInfo);
                            }
                            
                        }
                    }
                }

                dbActionResult.Success = true;
                dbActionResult.Message = "User retrieved successfully!";

            }
            catch (SqlException ex)
            {
                dbActionResult.Message = "User Not Deleted!, Unable to connect";
            }
            catch (Exception ex)
            {
                dbActionResult.Message = "An error occurred while processing the request";
            }
            return dbActionResult;

        }
        public List<User> GetUsersByName(String searchTerm)
        {

            List<User> userList = new List<User>();
            DbActionResult dbActionResult = new DbActionResult { Success = true, Message = "User List Retrieved sucessfully!" };
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    sqlConnection.Open();
                    String commandText = $@"SELECT * FROM [dbo].[Users]
                                            WHERE [UserName] =@searchTerm";
                    using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("searchTerm", searchTerm);
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            int colUserId = dataReader.GetOrdinal("UserId");
                            int colUserName = dataReader.GetOrdinal("UserName");
                            int colPassword = dataReader.GetOrdinal("Password");
                            int colFirstName = dataReader.GetOrdinal("FirstName");
                            int colLastName = dataReader.GetOrdinal("LastName");
                            int colEmail = dataReader.GetOrdinal("Email");
                            int colMembership = dataReader.GetOrdinal("Membership");
                            while (dataReader.Read())
                            {
                                User userInfo = new User();
                                userInfo.UserId = dataReader.GetGuid(colUserId);
                                userInfo.UserName = dataReader.GetString(colUserName);
                                userInfo.Password = dataReader.GetString(colPassword);
                                userInfo.FirstName = dataReader.GetString(colFirstName);
                                userInfo.LastName = dataReader.GetString(colLastName);
                                userInfo.Email = dataReader.GetString(colEmail);
                                userInfo.Membership = dataReader.GetInt32(colMembership);
                                userList.Add(userInfo);
                            }

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "User Not Deleted!, Unable to connect";
            }
            catch (Exception ex)
            {
                dbActionResult.Success = false;
                dbActionResult.Message = "An error occurred while processing the request";
            }
            return userList;
        }

    }
}
