using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public UsersMessage(string senderName, string messageText, string timeSent)
        {
            InitializeComponent();
            Name = senderName;
            Text = messageText;
            Time = timeSent; //.ToString("HH:mm:ss");
        }
        
        public string Name
        {
            get { return SenderName.Text; }
            set { SenderName.Text = value; }
        }

        public string Text
        {
            get { return MessageText.Text; }
            set { MessageText.Text = value; }
        }

        public string Time
        {
            get { return TimeSent.Text; }
            set { TimeSent.Text = value; }
        }
    }
}
