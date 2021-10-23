using System;
using System.Threading.Tasks;
using Gigachat.Models;
using Gigachat.Core;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Controls;

namespace Chatt.ViewModels
{
    public class ChatViewModel : ViewModel
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
        public ObservableCollection<User> ClientList { get; set; }
        ClientObject Client { get; set; }

        public ChatViewModel(ClientObject Client)
        {
            UpdateSettings();
            this.Client = Client;
            Messages = new ObservableCollection<VisibleMessage>();
            ClientList = new ObservableCollection<User>();
            ClientList.Add(new User { UserName = Client.UserName });

            MessageText = "";     
        }

        // Нажатие кнопки отправки сообщения
        private void SendButton_Click()
        {
            if (MessageText.Length != 0)
            {
                Client.Send_Message(MessageText);
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
                    Message message = Client.Recieve_Message();
                    VisibleMessage vmes = new VisibleMessage { Name = message.Name, Text = message.Text, Time = message.Time /*DateTime.Now.ToString("HH:mm") */ , IsMy = "false" };
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => { Messages.Add(vmes); }));

                    //if (data.Message != null)
                    //{
                    //    VisibleMessage vmes = new VisibleMessage { Name = data.Message.Name, Text = data.Message.Text, Time = data.Message.Time /*DateTime.Now.ToString("HH:mm") */ , IsMy = "false" };

                    //    Application.Current.Dispatcher.BeginInvoke(new Action(() => { Messages.Add(vmes); }));
                    //}
                    //if (data.NewClient != null)
                    //{
                    //    Application.Current.Dispatcher.BeginInvoke(new Action(() => { ClientList.Add(data.NewClient); }));
                    //}
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

    }
}
