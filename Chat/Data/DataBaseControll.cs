using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection.Emit;
using System.Data.SQLite;

using Npgsql;
using System.Security.Cryptography;
using System.Net.Configuration;
//using Npgsql.EntityFrameworkCore;

namespace Chat
{
    public class DataBaseControll : DbContext
    {
        static string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=root;";
        NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);

      /*  public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        } */

        public void Connection()
        {
            try
            {
                npgSqlConnection.Open();
                Console.WriteLine("Succesful connection!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool CreateUser(string login, string password)
        {
            Connection();
            string _command = $"SELECT * FROM users WHERE (\"login\" = '{login}');";
            NpgsqlCommand commandSQL = new NpgsqlCommand(_command, npgSqlConnection);
            NpgsqlDataReader dataReader = commandSQL.ExecuteReader();
            if (dataReader.HasRows)
            {
                npgSqlConnection.Close();
                return false;
            }
            dataReader.Close();
            _command = $"INSERT INTO users (\"login\", \"password\") VALUES ('{login}', '{password}');";
            commandSQL.CommandText = _command;
            commandSQL.ExecuteNonQuery();
            npgSqlConnection.Close();
            return true;
        }

        // Безвозвратная команда
        public bool LogginCheck(string command)
        {
            Connection();
            NpgsqlCommand commandSQL = new NpgsqlCommand(command, npgSqlConnection);
            NpgsqlDataReader dataReader =  commandSQL.ExecuteReader();
            if (dataReader.HasRows)
            {
                npgSqlConnection.Close();
                return true;
            }
            //commandSQL.ExecuteNonQuery();
            npgSqlConnection.Close();
            return false;
        }

        public void Close() { npgSqlConnection.Close(); }
    }
}

