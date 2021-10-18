using Gigachat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Chatt.ViewModels
{
    class EntryViewModel: ViewModel
    {
        private ICommand entryButtonCommand;
        public ICommand EntryButtonCommand
        {
            get
            {
                return entryButtonCommand ?? (entryButtonCommand = new RelayCommand(obj => EntryButton_Click()));
            }
        }

        private ICommand regButtonCommand;
        public ICommand RegButtonCommand
        {
            get
            {
                return regButtonCommand ?? (regButtonCommand = new RelayCommand(obj =>RegButton_Click()));
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

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
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

        private MainViewModel ParentVM;

        public EntryViewModel(MainViewModel vm)
        {
            ParentVM = vm;
        }

        private void EntryButton_Click()
        {
            if (UserName == "" || Password == "")
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            (int result, string message) = ParentVM.Client.Authentification(UserName, Password);

            if (result == 1)
            {
                // MessageBox.Show("Залазей");
                ParentVM.AuthentificationChecked(userName);
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void RegButton_Click() 
        {
            ParentVM.ShowRegistrationWindow();
        }
    }
}
