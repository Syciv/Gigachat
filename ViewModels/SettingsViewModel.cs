using Gigachat.Models;
using Gigachat.Core;
using MessageBox = System.Windows.MessageBox;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;

namespace Chatt.ViewModels
{
    public class SettingsViewwModel : ViewModel
    {
        private ICommand saveButtonCommand;
        public ICommand SaveButtonCommand
        {
            get
            {
                return saveButtonCommand ?? (saveButtonCommand = new RelayCommand(obj => SaveButton_Click()));
            }
        }

        private string serverIp;
        public string ServerIp
        {
            get { return serverIp; }
            set
            {
                serverIp = value;
                OnPropertyChanged(nameof(ServerIp));
            }
        }

        private string port;
        public string Port
        {
            get { return port; }
            set
            {
                port = value;
                OnPropertyChanged(nameof(Port));
            }
        }

        public SettingsViewwModel()
        {

        }

        private void SaveButton_Click()
        {

        }
    }
}
