using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Data
{
    public class MessageStorage
    {

        public MessageStorage(string id, string name, string login, string time, string text)
        {
            ID = id;
            Name = name;
            Login = login;
            Time = time;
            Text = text;
        }

        public string ID { get; set; } = "";

        public string Name { get; set; } = "";

        public string Login { get; set; } = "";

        public string Time { get; set; } = "";

        public string Text { get; set; } = "";


    }
}
