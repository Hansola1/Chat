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
using Chat.Data;
//using Npgsql.EntityFrameworkCore;

namespace Chat
{
    public class DataBaseControll : DbContext
    {
        static string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=root;"; //"Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=root;";
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
            npgSqlConnection.Close();
            return false;
        }

        public void SendMessage(string messageText, string login, string userName)
        {
            Connection();
            string command = $"INSERT INTO messages (username, text, time, login) VALUES ( '{userName}', '{messageText}', '{DateTime.Now}', '{login}');";
            NpgsqlCommand commandSQL = new NpgsqlCommand(command, npgSqlConnection);

            commandSQL.ExecuteNonQuery();
            npgSqlConnection.Close();
        }

        public async Task<List<MessageStorage>> RequireMessage()
        {
            Connection();
            List<MessageStorage> message = new List<MessageStorage>();
            string command = $"SELECT * FROM messages";
            NpgsqlCommand commandSQL = new NpgsqlCommand(command, npgSqlConnection);
            NpgsqlDataReader dataReader = commandSQL.ExecuteReader();
            while (dataReader.HasRows)
            {
                dataReader.Read();
                if (dataReader.IsOnRow)
                {
                    message.Add(new MessageStorage(dataReader.GetInt64(0).ToString(), dataReader.GetString(1),
                        dataReader.GetString(4), dataReader.GetString(3), dataReader.GetString(2)));
                }
                else
                {
                    break;
                }
            }
            npgSqlConnection.Close();
            return message;
        }

        public void InputCommand(string command)
        {
            Connection();
            NpgsqlCommand commandSQL = new NpgsqlCommand(command, npgSqlConnection);

            commandSQL.ExecuteNonQuery();
            npgSqlConnection.Close();
        }

        public void Close() { npgSqlConnection.Close(); }
    }
}

