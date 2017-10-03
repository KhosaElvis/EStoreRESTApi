using System;
using System.Collections.Generic;
using EStoreRESTApi.Models;
using System.Configuration;
using System.Data.SqlClient;
using EStoreRESTApi.Security;
using System.Diagnostics;

namespace EStoreRESTApi.Presenter
{
    public class AuthDataMapper : IAuthDataMapper
    {
        //Post 
        public int ? insertUserNewToDatabase(string username, string password, string defaultRole = "Client")
        {
            int ? results = null;
            string connectinString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string byteArrayPassword = Password.Encrypt(password);
            string query = string.Format("insert into tblAuthouriser values('{0}','{1}','{2}')", username, byteArrayPassword, defaultRole);
            using (SqlConnection conn = new SqlConnection(connectinString))
            {
                using (SqlCommand com = new SqlCommand(query, conn))
                {
                    conn.Open();
                    results = com.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return results;
        }
        //Put
        public int ? resetUserPasswordFromDatabase(string username, string password)
        {
            int ? results = null;

            string connectinString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string byteArrayPassword = Password.Encrypt(password);
            string query = string.Format("UPDATE tblAuthouriser  SET [Password] = '{0}' WHERE [Username] = '{1}' ", byteArrayPassword, username);
            using (SqlConnection conn = new SqlConnection(connectinString))
            {
                using (SqlCommand com = new SqlCommand(query, conn))
                {
                    conn.Open();
                    results = (int)com.ExecuteScalar();
                    conn.Close();
                }
            }
            return results;
        }
        // Helpers
        // Check User
        public bool usernameExistInTheDatabase(string username)
        {
            int results = 0;
            string connectinString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string query = string.Format("SELECT COUNT(*) FROM tblAuthouriser WHERE [Username] = '{0}'", username);
            using (SqlConnection conn = new SqlConnection(connectinString))
            {
                using (SqlCommand com = new SqlCommand(query, conn))
                {
                    conn.Open();
                    results = (int)com.ExecuteScalar();
                    conn.Close();
                }
            }
            return results > 0;
        }

        // Get single user by Id
        public Authouriser getUserFromTheDatabase(string username)
        {
            Authouriser results = new Authouriser();
            string connectinString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string query = string.Format("SELECT [Id],[Username],[Password],[Role] FROM tblAuthouriser WHERE [Username] = '{0}'", username);
            using (SqlConnection conn = new SqlConnection(connectinString))
            {
                using (SqlCommand com = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    while(reader.Read())
                    {
                        results.Id = (int)reader["Id"];
                        results.Username = (string)reader["Username"];
                        results.Password = (string)reader["Password"];
                        results.Role = (string)reader["Role"];
                    }
                    conn.Close();
                }
            }
            return results;
        }
        // Get all users by Id
        public List<Authouriser> getAllUsersFromTheDatabase( )
        {
            List<Authouriser> allResults = new List<Authouriser>();

            string connectinString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string query = string.Format("SELECT [Id],[Username],[Password],[Role] FROM [tblAuthouriser]");
            using (SqlConnection conn = new SqlConnection(connectinString))
            {
                using (SqlCommand com = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        allResults.Add( new Authouriser
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Role = (string)reader["Role"]

                        });
                    }
                    conn.Close();
                }
            }
            return allResults;
        }
        // Remove User
        public string deleteUserFromDatabase(string username, string password)
        {
            string results = null;

            if (Password.userPasswordMatchesDatabasePassword(username, password))
            {
                string connectinString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                string query = string.Format("DELETE FROM tblAuthouriser WHERE [Username] = '{0}'", username);
                using (SqlConnection conn = new SqlConnection(connectinString))
                {
                    using (SqlCommand com = new SqlCommand(query, conn))
                    {
                        try
                        {
                            conn.Open();
                            int rows = com.ExecuteNonQuery();
                            conn.Close();
                            if (rows > 0)
                                results = string.Format("Client {0} deleted ", username);
                            else
                                results = string.Format("Fail to deleted Client ", username);
                        }
                        catch (Exception e)
                        {
                            Debug.Write(e.Message);
                        }
                    }
                }
                return results;
            }
            else
            {
                return results = string.Format("Failed to delete {0} Wrong Password", username);
            }


        }
    }
}