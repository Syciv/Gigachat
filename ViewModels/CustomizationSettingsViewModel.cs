using Gigachat.Models;
using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace Chatt.ViewModels
{
    class CustomizationSettingsViewModel:ViewModel 
    {
        private MainViewModel ParentVM;
        public CustomizationSettingsViewModel(MainViewModel vm)
        {
            this.ParentVM = vm;
            UpdateSettings();
        }

        private void BackgroundColorCommand_Click()
        {
            ColorDialog clrdlg = new ColorDialog();

            if (clrdlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackgroundColor = clrdlg.Color;
                Properties.Settings.Default.Save();
                Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(clrdlg.Color.R, clrdlg.Color.G, clrdlg.Color.B));
                ParentVM.UpdateSettings();
            }
        }

        private void ElementColor1Command_Click()
        {
            ColorDialog clrdlg = new ColorDialog();

            if (clrdlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ElementColor1 = clrdlg.Color;
                Properties.Settings.Default.Save();
                Element1 = new SolidColorBrush(System.Windows.Media.Color.FromRgb(clrdlg.Color.R, clrdlg.Color.G, clrdlg.Color.B));
                ParentVM.UpdateSettings();
            }
        }

        private void ElementColor2Command_Click()
        {
            ColorDialog clrdlg = new ColorDialog();

            if (clrdlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ElementColor2 = clrdlg.Color;
                Properties.Settings.Default.Save();
                Element2 = new SolidColorBrush(System.Windows.Media.Color.FromRgb(clrdlg.Color.R, clrdlg.Color.G, clrdlg.Color.B));
                ParentVM.UpdateSettings();
            }
        }

        private void ForegroundCommand_Click()
        {
            ColorDialog clrdlg = new ColorDialog();

            if (clrdlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ForegroundColor = clrdlg.Color;
                Properties.Settings.Default.Save();
                Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(clrdlg.Color.R, clrdlg.Color.G, clrdlg.Color.B));
                ParentVM.UpdateSettings();
            }
        }

 
        private ICommand backgroundColorCommand;
        public ICommand BackgroundColorCommand
        {
            get
            {
                return backgroundColorCommand ?? (backgroundColorCommand = new RelayCommand(obj => BackgroundColorCommand_Click()));
            }
        }

        private ICommand elementColor1Command;
        public ICommand ElementColor1Command
        {
            get
            {
                return elementColor1Command ?? (elementColor1Command = new RelayCommand(obj => ElementColor1Command_Click()));
            }
        }

        private ICommand elementColor2Command;
        public ICommand ElementColor2Command
        {
            get
            {
                return elementColor2Command ?? (elementColor2Command = new RelayCommand(obj => ElementColor2Command_Click()));
            }
        }

        private ICommand foregroundCommand;
        public ICommand ForeGroundCommand
        {
            get
            {
                return foregroundCommand ?? (foregroundCommand = new RelayCommand(obj => ForegroundCommand_Click()));
            }
        }

    }
}
