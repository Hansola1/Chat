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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using Npgsql;
namespace Chat
{
    public partial class MainWindow : Window
    {
        DataBaseControll DataBase = new DataBaseControll();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonRegistClick(object sender, RoutedEventArgs e) 
        {
            string login = textBoxLogin.Text.Trim(); 
            string password = passwordBox.Password.Trim(); 
            string username = textBoxUserName.Text.Trim();
            //Trim - убирает пробелы

            if (login.Length > 20 || login.Length < 3)
            {
                textBoxLogin.ToolTip = "Длина логина не более 20 знаков и не менее 3 знаков";
                textBoxLogin.Background = Brushes.Salmon;
            }
            if (password.Length > 20 || password.Length < 4) 
            {
                passwordBox.ToolTip = "Длина пароля не более 20 знаков и не менее 4 знаков";
                passwordBox.Background = Brushes.Salmon;
            }
            if(username.Length > 15 || username.Length < 3)
            {
                textBoxUserName.ToolTip = "Длина имени не более 15 знаков и не менее 3 знаков";
                textBoxUserName.Background = Brushes.Salmon;
            }
            else
            {
                textBoxLogin.Background = Brushes.Transparent;
                passwordBox.Background = Brushes.Transparent;
                textBoxUserName.Background = Brushes.Transparent;

                MessageBox.Show("Ввод корректный :)");

                if(DataBase.CreateUser(login,password,username))
                {
                    Auth authWin = new Auth();
                    authWin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь уже существует ;(", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
        private void ButtonAuthClickWin(object sender, RoutedEventArgs e)
        {
            Auth authWin = new Auth();
            authWin.Show();
            this.Close();
        }

        private bool isFirstInput = true;
        private bool isTwoInput = true;
        private bool isThreeInput = true;
        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "имя";              
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
                isFirstInput = true;
            }
        }
        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (isFirstInput && textBox.Text == "имя")
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
                isFirstInput = false;
            }
        }
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
