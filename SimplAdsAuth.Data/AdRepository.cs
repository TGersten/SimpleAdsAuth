using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace SimplAdsAuth.Data
{
    public class AdRepository
    {
        private readonly string _connectionString;

        public AdRepository( string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Ad> GetAllAds()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Ads a JOIN Users u ON a.UserId = u.Id";
            connection.Open();

            List<Ad> ads = new();
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ads.Add(new()
                {
                    Id = (int)reader["Id"],
                    Date = (DateTime)reader["Date"],
                    Details = (string)reader["Details"],
                    UserId = (int)reader["UserId"],
                    Name= (string)reader["Name"],
                    Email = (string)reader["Email"],
                    PasswordHash = (string)reader["PasswordHash"],
                });
            }
            return ads;
        }

        public void AddUser(User user, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Users (Name, Email, PasswordHash) VALUES (@name,@email @password)";
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("password", password);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
