using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SavorySweets.Project.Models;

namespace SavorySweets.Project.Data
{
    public class UserDataService
    {
        private readonly string dbPath;

        public UserDataService()
        {
            dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db");
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS Users (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT,
                    LastName TEXT,
                    Email TEXT UNIQUE,
                    Password TEXT,
                    ProfileImagePath TEXT,
                    IsDarkMode INTEGER
                );
            ";
            command.ExecuteNonQuery();
        }

        public bool Register(User user)
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO Users (FirstName, LastName, Email, Password, ProfileImagePath, IsDarkMode)
                VALUES ($firstName, $lastName, $email, $password, $profileImagePath, $isDarkMode);
            ";
            command.Parameters.AddWithValue("$firstName", user.FirstName);
            command.Parameters.AddWithValue("$lastName", user.LastName);
            command.Parameters.AddWithValue("$email", user.Email);
            command.Parameters.AddWithValue("$password", user.Password);
            command.Parameters.AddWithValue("$profileImagePath", user.ProfileImagePath);
            command.Parameters.AddWithValue("$isDarkMode", user.IsDarkMode ? 1 : 0);

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false; // likely duplicate email
            }
        }

        public User? Login(string email, string password)
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT * FROM Users WHERE Email = $email AND Password = $password;
            ";
            command.Parameters.AddWithValue("$email", email);
            command.Parameters.AddWithValue("$password", password);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    ID = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Password = reader.GetString(4),
                    ProfileImagePath = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    IsDarkMode = reader.GetInt32(6) == 1
                };
            }

            return null;
        }

        // ✅ NEW: Get all users (for persistent login support)
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    ID = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Password = reader.GetString(4),
                    ProfileImagePath = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    IsDarkMode = reader.GetInt32(6) == 1
                });
            }

            return users;
        }

        public void UpdateUser(User user)
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
        UPDATE Users
        SET FirstName = $firstName,
            LastName = $lastName,
            Email = $email,
            Password = $password,
            ProfileImagePath = $profileImagePath,
            IsDarkMode = $isDarkMode
        WHERE ID = $id;
    ";
            command.Parameters.AddWithValue("$id", user.ID);
            command.Parameters.AddWithValue("$firstName", user.FirstName);
            command.Parameters.AddWithValue("$lastName", user.LastName);
            command.Parameters.AddWithValue("$email", user.Email);
            command.Parameters.AddWithValue("$password", user.Password);
            command.Parameters.AddWithValue("$profileImagePath", user.ProfileImagePath);
            command.Parameters.AddWithValue("$isDarkMode", user.IsDarkMode ? 1 : 0);

            command.ExecuteNonQuery();
        }
    }
}
