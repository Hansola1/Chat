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
using Npgsql;

namespace Chat
{
    public partial class Auth : Window
    {
        DataBaseControll DataBase = new DataBaseControll();

        public Auth()
        {
            InitializeComponent();
        }

        private void ButtonAuthClick(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = passwordBox.Password.Trim();

            if (login != null && password != null)
            {
                LogIn(login, password);            
            }
        }

        private void LogIn(string login, string password)
        {
            string QueryText = $"SELECT * FROM users WHERE (\"login\" = '{login}' AND \"password\" = '{password}');";
            
            if (DataBase.LogginCheck(QueryText))
            {
                string userName = DataBase.GetUserName(login);
                ChatWin chatWindow = new ChatWin(login, userName);
                chatWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Данные неверны ;(");
            }
        }

        private void ButtonRegClickWin(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            this.Close();
        }

        private bool isThreeInput = true;
        private bool isTwoInput = true;
        private void logTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "логин";
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
                isTwoInput = true;
            }
        }
        private void logTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (isTwoInput && textBox.Text == "логин")
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
                isTwoInput = false;
            }
        }
        private void passTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Password = "пароль";
                passwordBox.Foreground = System.Windows.Media.Brushes.Gray;
                isThreeInput = true;
            }
        }
        private void passTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (isThreeInput && passwordBox.Password == "пароль")
            {
                passwordBox.Password = "";
                passwordBox.Foreground = System.Windows.Media.Brushes.Black;
                isThreeInput = false;
            }
        }
    }
}
