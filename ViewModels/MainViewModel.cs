using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Chatt.Models;
using System.Windows.Input;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Controls;
using Chatt.Views;

namespace Chatt.ViewModels
{
    public class MainViewModel:ViewModel
    {
        private ICommand chatButtonCommand;
        public ICommand ChatButtonCommand
        {
            get
            {
                return chatButtonCommand ?? (chatButtonCommand = new RelayCommand(obj => ChatButtonCommand_Click()));
            }
        }

        private ICommand customizationButtonCommand;
        public ICommand CustomizationButtonCommand
        {
            get
            {
                return customizationButtonCommand ?? (customizationButtonCommand = new RelayCommand(obj => CustomizationButtonCommand_Click()));
            }
        }

        private ICommand settingsButtonCommand;
        public ICommand SettingsButtonCommand
        {
            get
            {
                return settingsButtonCommand ?? (settingsButtonCommand = new RelayCommand(obj => SettingsButtonCommand_Click()));
            }
        }


        public Page ChatPage;
        private Page CustomizationPage;
        // private Page SettingsPage;

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

        private Visibility isVisible; // = Visibility.Hidden;
        public Visibility IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public ClientObject Client { get; set; }

        private EntryWindow entwnd;
        private RegistrationWindow regwnd;

        public MainViewModel()
        {
            UpdateSettings();
            IsVisible = Visibility.Hidden;
            

            Client = new ClientObject("хихи");
            try
            {
                Client.Start();
            }
            // Не удалось подключиться
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
            // working = true;
            // AuthentificationChecked();
            ShowEntryWindow();

        }

        public void ShowEntryWindow()
        {
            entwnd = new EntryWindow();
            entwnd.DataContext = new EntryViewModel(this);
            entwnd.Show();
        }

        public void ShowRegistrationWindow()
        {
            regwnd = new RegistrationWindow();
            regwnd.DataContext = new RegistrationViewModel(this);
            entwnd.Close();
            regwnd.Show();
        }

        public void AuthentificationChecked()
        {
            ChatPage = new Pages.ChatPage();
            ChatPage.DataContext = new ChatViewModel(Client);
            CustomizationPage = new Pages.CustomizationSettingsPage();
            CustomizationPage.DataContext = new CustomizationSettingsViewModel(this);
            //SettingsPage = new Pages.ChatPage();
            // ChatPage.DataContext = new ChatViewModel(Client);

            CurrentPage = ChatPage;

            IsVisible = Visibility.Visible;
            // EntryIsVisible = Visibility.Hidden;
            entwnd.Close();
            regwnd.Close();

        }

        private void ChatButtonCommand_Click()
        {
            CurrentPage = ChatPage;
        }

        private void CustomizationButtonCommand_Click()
        {
            CurrentPage = CustomizationPage;
        }

        private void SettingsButtonCommand_Click()
        {
            // CurrentPage = SettingsPage;
        }

        public void Exit()
        {

        }

    }
}
