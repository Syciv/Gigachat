using System;
using System.Text;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;
using MessageBox = System.Windows.MessageBox;
using Gigachat.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Gigachat.Core
{
    public class ClientObject
    {
        private Socket sock;
        private IPEndPoint localIP;

        //
        private AutoResetEvent responseEvent;
        private AutoResetEvent messageEvent;
        private AutoResetEvent profileEvent;

        private UserProfile userProfileBuf;
        private Message messageBuf;
        private Response responseBuf;
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

            responseEvent = new AutoResetEvent(false);
            messageEvent = new AutoResetEvent(false);
            profileEvent = new AutoResetEvent(false);

            userProfileBuf = null;
            messageBuf = null;
            responseBuf = null;

            Recieve_dataAsync();
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
        public (int, string) Authentification(string userName, string password)
        {
            UserAuthentification userAuth = new UserAuthentification { UserName = userName, Password = password };
            Data data = new Data { UserAuthentification = userAuth };

            string jsonObj = JsonSerializer.Serialize<Data>(data);
            byte[] bytes = Encoding.Default.GetBytes(jsonObj);
            sock.Send(bytes);

            // Получение результата аутентификации

            try
            {
                responseEvent.WaitOne(5000);
                Response response = responseBuf;
                responseBuf = null;
                responseEvent.Reset();

                return (response.Result, response.Message);
            }
            catch
            {
                return (0, "Что-то не получилось...");
            }
        }

        // Отправка заявки на регистрацию пользователя
        public (int, string) Registration(string name, string surname, string userName, string password)
        {
            UserRegistration userReg = new UserRegistration { Name = name, Surname = surname, UserName = userName, Password = password};

            Data data = new Data { UserRegistration = userReg };

            string jsonObj = JsonSerializer.Serialize<Data>(data);
            byte[] bytes = Encoding.Default.GetBytes(jsonObj);
            sock.Send(bytes);

            // Получение результата регистрации
            try
            {
                responseEvent.WaitOne(5000);
                Response response = responseBuf;
                responseBuf = null;
                responseEvent.Reset();

                return (response.Result, response.Message);
            }
            catch
            {
                return (0, "Что-то не получилось...");
            }
        }

        // Получение информации о пользователе
        public (int, string, UserProfile) GetUserProfile(string userName)
        {
            User user = new User { UserName = userName };
            Data data = new Data { User = user };

            string jsonObj = JsonSerializer.Serialize<Data>(data);
            byte[] bytes = Encoding.Default.GetBytes(jsonObj);
            sock.Send(bytes);

            // Получение результата
            try
            {
                profileEvent.WaitOne(5000);
                UserProfile userProfile = userProfileBuf;
                userProfileBuf = null;
                profileEvent.Reset();

                return (1, "Распишитесь", userProfile);
            }
            catch
            {
                return (0, "Что-то не получилось...", null);
            }
        }

        public (int, string) ChangeProfileImage(string userName, byte[] imageBytes)
        {
            ProfileImage profileImage = new ProfileImage { UserName = userName, Image = imageBytes };
            Data data = new Data {ProfileImage = profileImage };

            string jsonObj = JsonSerializer.Serialize<Data>(data);
            byte[] bytes = Encoding.Default.GetBytes(jsonObj);
            sock.Send(bytes);

            try
            {
                responseEvent.WaitOne(5000);
                Response response = responseBuf;
                responseBuf = null;
                responseEvent.Reset();

                return (response.Result, response.Message);
            }
            catch
            {
                return (0, "Что-то не получилось...");
            }
        }

        private async void Recieve_dataAsync()
        {
            await Task.Run(() => Recieve_Data());
        }

        private void Recieve_Data()
        {
            while (true)
            {
                int bufSize = 510 * 1024;
                byte[] bytes = new byte[bufSize];
                int len = sock.Receive(bytes);
                string jsonObj = Encoding.Default.GetString(bytes);

                // Получаем сообщение из буфера
                // int endOfData = jsonObj.IndexOf((char)0x00);
                jsonObj = jsonObj.Substring(0, len);
                Data data = JsonSerializer.Deserialize<Data>(jsonObj);

                if (data.Response != null)
                {
                    responseBuf = data.Response;
                    responseEvent.Set();
                }

                if (data.UserProfile != null)
                {
                    // MessageBox.Show("Получил");
                    userProfileBuf = data.UserProfile;
                    profileEvent.Set();
                }

                if (data.Message != null)
                {
                    messageBuf = data.Message;
                    messageEvent.Set();
                }
            }
        }

        public Message Recieve_Message()
        {
            messageEvent.WaitOne();
            Message message = messageBuf;
            messageBuf = null;
            messageEvent.Reset();
            return message;
        }
    }
}
