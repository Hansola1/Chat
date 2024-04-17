﻿using System;
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

namespace Chat
{
    public partial class ChatWin : Window
    {
        DataBaseControll DataBase = new DataBaseControll();
        private string _login;
        private string _userName;


        public ChatWin(string login, string userName)
        {
            _login = login;
            _userName = userName;
            InitializeComponent();
           // messageTextBox.KeyDown += MessageTextBox_KeyDown;
        }

        private void SendToListBxButton_Click(object sender, RoutedEventArgs e)
        {
            string message = messageTextBox.Text;
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
            {
                //chatListBox.Items.Add(message);
                DataBase.SendMessage(message, _login, _userName);
                messageTextBox.Text = "";
            }
        }


        /*private void CreateMessageBox(string name, string text, string date, string id, ItemCollection collection)
        {
            TextBox messageTextBox = new TextBox();

            messageTextBox.Width = 400;
            messageTextBox.TextWrapping = TextWrapping.Wrap;
            messageTextBox.AcceptsReturn = true;
            messageTextBox.IsReadOnly = true;
            messageTextBox.FontFamily = new FontFamily("Comic Sans MS");
            messageTextBox.Name = id;
            messageTextBox.Text = $"Пользователь {name}:\n{text}\n\n{date}";

            collection.Add(messageTextBox);
        }

        private async void SendToListBxButton_Click(object sender, RoutedEventArgs e)
        {
            string message = messageTextBox.Text;
            if (message.Length > 0)
            {
                List<List<string>> messages = new List<List<string>>();
                messages = await DataBase.OutputMessagesCommand();

                List<List<string>> messagesID = new List<List<string>>();
               // messagesID = await DataBase.OutputMessagesID();

                DataBase.InputCommand($"INSERT INTO messages (\"ID\", \"username\", \"text\", \"time\", \"login\") VALUES ('{(messages[0].Count()).ToString()}', '{Username.Text}', '{messageTextBox.Text}', '[{DateTime.Now}], {}');");
               // DataBase.InputCommand($"INSERT INTO messageID (id) VALUES ('{messagesID[0].Count()}');");

                CreateMessageBox(Username.Text, messageTextBox.Text, $"[{DateTime.Now}]", $"idx{messageTextBox.Text.Length.ToString()}", chatListBox.Items);
                //chatListBox.Items.Add($"Пользователь {name}:\n{text}\n\n{date}"message);
                messageTextBox.Text = string.Empty;
            }
        }*/




        /* private void SendToListBxButton_Click(object sender, RoutedEventArgs e)
         {
             string message = messageTextBox.Text;
             if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
             {
                 chatListBox.Items.Add(message);
                 messageTextBox.Text = "";
             }
         }
         private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
         {
             string message = messageTextBox.Text;
             if (e.Key == Key.Enter)
             {
                 if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
                 {
                     chatListBox.Items.Add(message);
                     messageTextBox.Text = "";
                 }
             }
         } */
    }
}


/*private void TimeForMess()
{
    System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

    timer.Tick += new EventHandler(timerTick);
    timer.Interval = new TimeSpan(0, 0, 5);
    timer.Start();
}*/
