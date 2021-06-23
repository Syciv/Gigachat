using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Chatt.Models;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Collections.Specialized;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Controls;

namespace Chatt.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        // Команды Кнопок
        private ICommand sendButtonCommand;
        public ICommand SendButtonCommand
        {
            get
            {
                return sendButtonCommand ?? (sendButtonCommand = new RelayCommand(obj => SendButton_Click()));
            }
        }

        private ICommand attachButtonCommand;
        public ICommand AttachButtonCommand
        {
            get
            {
                return attachButtonCommand ?? (attachButtonCommand = new RelayCommand(obj => AttachButton_Click()));
            }
        }

        private Page ChatPage;

        private Page currentPage;
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }


        // Текст набираемого сообщения
        private string messageText;
        public string MessageText
        {
            get { return messageText; }
            set
            {
                messageText = value;
                OnPropertyChanged(nameof(MessageText));
            }
        }

        // Текст сообщения для пользователя
        private string clientMessage;
        public string ClientMessage
        {
            get { return clientMessage; }
            set
            {
                clientMessage = value;
                OnPropertyChanged(nameof(ClientMessage));
            }
        }

        private bool working;

        // Список сообщений в чате
        public ObservableCollection<VisibleMessage> Messages { get; set; }
        // Список клиентов онлайн
        public ObservableCollection<NewClient> ClientList { get; set; }
        ClientObject Client { get; set; }

        public ChatViewModel()
        {
            // ChatPage = new Pages.ChatPage();

            // CurrentPage = ChatPage;

            Client = new ClientObject();
            Messages = new ObservableCollection<VisibleMessage>();
            ClientList = new ObservableCollection<NewClient>();
            ClientList.Add(new NewClient { ClientName = Client.UserName });

            MessageText = "";
            try
            {
                // Client.Start();
            }
            // Не удалось подключиться
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
            working = true;
            // Listen_messagesAsync();
        }

        // Нажатие кнопки отправки сообщения
        private void SendButton_Click()
        {
            if (MessageText.Length != 0)
            {
                Client.Send_Message(MessageText);
                // OnPropertyChanged(nameof(Client.Messages));
                Messages.Add(new VisibleMessage { Name = Client.UserName, Text = MessageText, Time = DateTime.Now.ToString("HH:mm"), IsMy = "true" });
                MessageText = "";
            }
        }

        private void AttachButton_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string FileName = openFileDialog.FileName;
                MessageBox.Show(FileName);
            }
        }

        // Поток слушания соощений с сревера
        private async void Listen_messagesAsync()
        {
            await Task.Run(() => Listen_messages());
        }

        private void Listen_messages()
        {
            while (working)
            {
                try
                {
                    Data data = Client.Recieve_Message();
                    if (data.Message != null)
                    {
                        VisibleMessage vmes = new VisibleMessage { Name = data.Message.Name, Text = data.Message.Text, Time = data.Message.Time /*DateTime.Now.ToString("HH:mm") */ , IsMy = "false" };

                        Application.Current.Dispatcher.BeginInvoke(new Action(() => { Messages.Add(vmes); }));
                    }
                    if (data.NewClient != null)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() => { ClientList.Add(data.NewClient); }));
                    }
                }
                catch (Exception ex)
                {
                    working = false;
                    ClientMessage = "Соединение аотеряно. Переподключение...";
                    while (!working)
                    {
                        try
                        {
                            Client.Start();
                            ClientMessage = "";
                            working = true;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.ToString());
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
