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
using System.Windows.Media;
using System.Windows;
//using Npgsql.EntityFrameworkCore;

namespace Chat
{
    public class DataBaseControll : DbContext
    {
        static string connectionString = "Host=localhost;Port=5432;Database=Chatnastya;Username=postgres;Password=Moper220;"; //"Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=root;";
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
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CreateUser(string login, string password, string username)
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
            _command = $"INSERT INTO users (\"login\", \"password\", \"username\") VALUES ('{login}', '{password}', '{username}');";
            commandSQL.CommandText = _command;
            commandSQL.ExecuteNonQuery();
            npgSqlConnection.Close();
            return true;
        }

        public string GetUserName(string login)
        {
            string username = "";
            Connection();
            string command = $"SELECT \"username\" FROM users WHERE (\"login\" = '{login}');";
            NpgsqlCommand commandSQL = new NpgsqlCommand(command, npgSqlConnection);
            NpgsqlDataReader dataReader = commandSQL.ExecuteReader();
            if (dataReader.HasRows)
            {
                dataReader.Read();
                username = dataReader.GetString(0);
            }
            npgSqlConnection.Close();
            return username;
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

        public void SendMessage(string messageText, string login, string userName)
        {
            Connection();
            string command = $"INSERT INTO messages (username, text, time, login) VALUES ( '{userName}', '{messageText}', '[{DateTime.Now}]', '{login}');";
            NpgsqlCommand commandSQL = new NpgsqlCommand(command, npgSqlConnection);

            commandSQL.ExecuteNonQuery();
            npgSqlConnection.Close();
        }

        /*public void RequireMessage()
        {
            Connection();
            NpgsqlCommand commandSQL = new NpgsqlCommand(command, npgSqlConnection);
            NpgsqlDataReader dataReader = commandSQL.ExecuteReader();
            while (dataReader.HasRows)
            {

                dataReader.NextResult();
            }
            npgSqlConnection.Close();
        }*/

        public void InputCommand(string command)
        {
            Connection();
            NpgsqlCommand commandSQL = new NpgsqlCommand(command, npgSqlConnection);

            commandSQL.ExecuteNonQuery();
            npgSqlConnection.Close();
        }





        // Получение данных о сообщениях
        /*public async Task<List<List<string>>> OutputMessagesCommand()
        {
            Connection();
            List<List<string>> messages = new List<List<string>>();

            List<string> username = new List<string> { "AdminName" };
            List<string> text = new List<string> { "AdminMessage" };
            List<string> time = new List<string> { "AdminDate" };
            List<string> id = new List<string> { "AdminId" };
            List<string> login = new List<string> { "AdminLG" };

            messages.Add(id);
            messages.Add(username);
            messages.Add(text);
            messages.Add(time);
            messages.Add(login);

            NpgsqlCommand commandSQL = new NpgsqlCommand("SELECT * FROM messages;", npgSqlConnection);
            await commandSQL.ExecuteNonQueryAsync();
            try
            {
                var reader = await commandSQL.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    messages[0].Add(reader.GetInt64(0).ToString());
                    messages[1].Add(reader.GetString(1));
                    messages[2].Add(reader.GetString(2));
                    messages[3].Add(reader.GetString(3));
                    messages[4].Add(reader.GetString(4));
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
        }*/


        public void Close() { npgSqlConnection.Close(); }
    }
}

