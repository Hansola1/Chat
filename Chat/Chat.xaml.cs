using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Xml;
using Npgsql;
using Chat.UserUseControl;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Timers;
using Chat.Data;
using System.ComponentModel;

namespace Chat
{
    public partial class ChatWin : Window
    {
        private System.Timers.Timer timer;
        DataBaseControll DataBase = new DataBaseControll();
        private string _login;
        private string _userName;

        private void SetTimer()
        {
            timer = new System.Timers.Timer(2000); 
            timer.Elapsed += AddMessage;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public ChatWin(string login, string userName)
        {
            _login = login;
            _userName = userName;
            InitializeComponent();
            SetTimer();
           // messageTextBox.KeyDown += MessageTextBox_KeyDown;
        }

        private void SendToListBxButton_Click(object sender, RoutedEventArgs e)
        {
            string message = messageTextBox.Text;
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
            {
                DataBase.SendMessage(message, _login, _userName);
                messageTextBox.Text = "";
            }
        }
        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            string message = messageTextBox.Text;
            if (e.Key == Key.LeftAlt)
            {
                if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
                {
                    DataBase.SendMessage(message, _login, _userName);
                    messageTextBox.Text = "";
                }
            }
        } 

        private async void AddMessage(object sender, ElapsedEventArgs e)
        {
            List<MessageStorage> test = await Task.Run(() => DataBase.RequireMessage());
            Dispatcher.Invoke(() =>
            {            
                Messages.Items.Clear();
                foreach (MessageStorage message in test)
                { 
                    Messages.Items.Add(new UsersMessage(message.Name, message.Text, message.Time));
                }
            });
        }
    }
}