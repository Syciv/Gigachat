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

namespace Chatt.ViewModels
{
    public class EntryViewModel : INotifyPropertyChanged
    {
        private ICommand entryButtonCommand;
        public ICommand EntryButtonCommand
        {
            get
            {
                return entryButtonCommand ?? (entryButtonCommand = new RelayCommand(obj => EntryButton_Click()));
            }
        }

        private string userName;
        public string UserName { 
            get { return userName; }
            set 
            { 
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        ClientObject Client { get; set; }

        public EntryViewModel()
        {
            Client = new ClientObject();
        }

        private void EntryButton_Click()
        {
            Client.UserName = UserName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
