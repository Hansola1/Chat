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
namespace Chat
{
    public partial class ChatWin : Window
    {
        DataBaseControll DataBase = new DataBaseControll();
        public ChatWin()
        {
            InitializeComponent();
            messageTextBox.KeyDown += MessageTextBox_KeyDown;
        }

        public class Message
        {
            public string Text { get; set; }
            public string Sender { get; set; }
            public DateTime Time { get; set; }

            public Message(string text, string sender)
            {
                Text = text;
                Sender = sender;
                Time = DateTime.Now;
            }
        }

        private void SendToListBxButton_Click(object sender, RoutedEventArgs e)
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
        }
    } //git попа
}
    

