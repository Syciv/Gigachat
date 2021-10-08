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
    public class ClientObject
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

        public ClientObject(string Username)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            localIP = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 5555);
            
            Random rand = new Random();
            this.UserName = UserName;// "Ivan" + rand.Next().ToString();

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
        }

        // Отправка заявки на аутентификацию пользователя
        public int Authentification(string userName, byte[] passwordHash)
        {
            UserAuthentification userAuth = new UserAuthentification { UserName = userName, PasswordHash = passwordHash };
            Data data = new Data { UserAuthentification = userAuth };

            string jsonObj = JsonSerializer.Serialize<Data>(data);
            byte[] bytes = Encoding.Default.GetBytes(jsonObj);
            sock.Send(bytes);

            // Получение результата аутентификации
            byte[] respbytes = new byte[1024];
            sock.Receive(respbytes);

            string respnObj = Encoding.Default.GetString(respbytes);

            int endOfData = respnObj.IndexOf((char)0x00);
            respnObj = respnObj.Substring(0, endOfData);

            Response response = JsonSerializer.Deserialize<Response>(respnObj);

            return response.Result;
        }

        // Отправка заявки на регистрацию пользователя
        public int Registration(string name, string surname, string userName, byte[] passwordHash, byte[] salt)
        {
            UserRegistration userReg = new UserRegistration { Name = name, Surname = surname, UserName = userName, PasswordHash = passwordHash, Salt = salt };
            Data data = new Data { UserRegistration = userReg };

            string jsonObj = JsonSerializer.Serialize<Data>(data);
            byte[] bytes = Encoding.Default.GetBytes(jsonObj);
            sock.Send(bytes);

            // Получение результата регистрации
            byte[] respbytes = new byte[1024];
            sock.Receive(respbytes);

            string respnObj = Encoding.Default.GetString(respbytes);

            int endOfData = respnObj.IndexOf((char)0x00);
            respnObj = respnObj.Substring(0, endOfData);

            Response response = JsonSerializer.Deserialize<Response>(respnObj);

            return response.Result;
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
    }
}
