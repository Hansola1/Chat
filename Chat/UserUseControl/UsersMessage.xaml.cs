using System;
using System.Collections.Generic;
using System.Data;
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

namespace Chat.UserUseControl
{
    public partial class UsersMessage : UserControl
    {
        public UsersMessage(string senderName, string messageText, DateTime timeSent)
        {
            InitializeComponent();
            SenderName.Text = senderName;
            MessageText.Text = messageText;
            TimeSent.Text = timeSent.ToString("HH:mm:ss");
        }
    }
}
