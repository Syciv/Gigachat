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
using Chatt.Views;

namespace Chatt.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private ICommand entryButtonCommand;
        public ICommand EntryButtonCommand
        {
            get
            {
                return entryButtonCommand ?? (entryButtonCommand = new RelayCommand(obj => EntryButton_Click()));
            }
        }

        private ICommand entryEnterCommand;
        public ICommand EntryEnterCommand
        {
            get
            {
                return entryEnterCommand ?? (entryEnterCommand = new RelayCommand(obj => EntryButton_Click()));
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public Page ChatPage;
        private Page EntryPage;

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

        private Visibility entryIsVisible; // = Visibility.Hidden;
        public Visibility EntryIsVisible
        {
            get { return entryIsVisible; }
            set
            {
                entryIsVisible = value;
                OnPropertyChanged(nameof(EntryIsVisible));
            }
        }

        private EntryWindow entwnd;

        public MainViewModel()
        {
            ChatPage = new Pages.ChatPage();
            // EntryPage = new Pages.EntryPage();

            IsVisible = Visibility.Hidden;
            EntryIsVisible = Visibility.Visible;

            ShowWindow();


            CurrentPage = ChatPage;
        }

        public void ShowWindow()
        {
            entwnd = new EntryWindow();
            entwnd.DataContext = this;
            entwnd.Show();
        }

        public void AuthrizationChecked(string ClientName)
        {
            IsVisible = Visibility.Visible;

        }
        
        ClientObject Client { get; set; }

        private void EntryButton_Click()
        {
            UserName = UserName;
            IsVisible = Visibility.Visible;
            EntryIsVisible = Visibility.Hidden;
            entwnd.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
