using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;
using MessageBox = System.Windows.MessageBox;

namespace Chatt.Models
{
    class ClientObject
    {
        private Socket sock;
        private IPEndPoint localIP;
        // public ObservableCollection<Message> Messages { get; set; }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
            }
        }

        public ClientObject()
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            localIP = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 5555);
            
            Random rand = new Random();
            UserName = "Ivan" + rand.Next().ToString();

            // Messages = new ObservableCollection<Message>();
        }

        // Начало работы
        public void Start()
        {
            sock.Connect(localIP);
        }

        // Отправка сообщения
        public void Send_Message(string messageText)
        {
            Message message = new Message { Text = messageText, Name = UserName, Time = DateTime.Now.ToString("HH:mm") };
            Data data = new Data { Message = message };

            // string jsonObj = JsonSerializer.Serialize<Message>(message);
            string jsonObj = JsonSerializer.Serialize<Data>(data);
            byte[] bytes = Encoding.Default.GetBytes(jsonObj);
            sock.Send(bytes);
            // Messages.Add(message);
            // OnPropertyChanged(nameof(Messages));
        }

        public Data Recieve_Message()
        {
            byte[] bytes = new byte[1024];
            int len = sock.Receive(bytes);
            string jsonObj = Encoding.Default.GetString(bytes);

            // jsonObj = jsonObj.Replace("0x00", String.Empty);
            // Получаем сообщение из буфера
            int endOfData = jsonObj.IndexOf((char)0x00);
            jsonObj = jsonObj.Substring(0, endOfData);
            // MessageBox.Show(jsonObj);
            Data data = JsonSerializer.Deserialize<Data>(jsonObj);
            return data;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
