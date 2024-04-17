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




        public void InputCommand(string command)
        {
            Connection();
            NpgsqlCommand commandSQL = new NpgsqlCommand(command, npgSqlConnection);

            commandSQL.ExecuteNonQuery();
            npgSqlConnection.Close();
        }

        // Получение данных о сообщениях
        public async Task<List<List<string>>> OutputMessagesCommand()
        {
            Connection();
            List<List<string>> messages = new List<List<string>>();
            List<string> name = new List<string> { "AdminName" };
            List<string> message = new List<string> { "AdminMessage" };
            List<string> date = new List<string> { "AdminDate" };
            List<string> id = new List<string> { "AdminId" };

            messages.Add(name);
            messages.Add(message);
            messages.Add(date);
            messages.Add(id);

            NpgsqlCommand commandSQL = new NpgsqlCommand("SELECT * FROM messages;", npgSqlConnection);
            await commandSQL.ExecuteNonQueryAsync();
            try
            {
                var reader = await commandSQL.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    messages[0].Add(reader.GetString(0));
                    messages[1].Add(reader.GetString(1));
                    messages[2].Add(reader.GetString(2));
                    messages[3].Add(reader.GetInt32(3).ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            npgSqlConnection.Close();
            return messages;
        }
        public async Task<List<List<string>>> OutputMessagesID()
        {
            Connection();
            List<List<string>> messages = new List<List<string>>();
            List<string> id = new List<string> { "AdminId" };
            messages.Add(id);

            NpgsqlCommand commandSQL = new NpgsqlCommand("SELECT * FROM messageID;", npgSqlConnection);
            await commandSQL.ExecuteNonQueryAsync();
            try
            {
                var reader = await commandSQL.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    messages[0].Add(reader.GetInt32(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            npgSqlConnection.Close();
            return messages;
        }


        public void Close() { npgSqlConnection.Close(); }
    }
}

