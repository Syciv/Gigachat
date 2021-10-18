using Gigachat.Models;
using Gigachat.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Input;
using System.Text;

namespace Chatt.ViewModels
{
    class RegistrationViewModel : INotifyPropertyChanged
    {

        private ICommand regButtonCommand;
        public ICommand RegButtonCommand
        {
            get
            {
                return regButtonCommand ?? (regButtonCommand = new RelayCommand(obj => RegButton_Click()));
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

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
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

        private string confirmedPassword;
        public string ConfirmedPassword
        {
            get { return confirmedPassword; }
            set
            {
                confirmedPassword = value;
                OnPropertyChanged(nameof(ConfirmedPassword));
            }
        }

        private MainViewModel ParentVM;

        public RegistrationViewModel(MainViewModel vm)
        {
            this.ParentVM = vm;
            UserName = "";
            Name = "";
            Surname = "";
        }

        private void RegButton_Click()
        {
            // Проверка ввода всех обязательных полей
            if (UserName == "" || Name == "" || Surname == "")
            {
                MessageBox.Show("Не все поля заполнены!");
                return ;
            }

            // Проверка совпадения пароля и подтвердения пароля
            if (Password != ConfirmedPassword)
            {
                MessageBox.Show("Пароль не подтверждён!");
                return ;
            }

            (int result, string message) = ParentVM.Client.Registration(Name, Surname, UserName, Password);
            if (result == 1)
            {
                // MessageBox.Show("Регистрация прошла успешно (на самом деле не прошла (ладно всё-таки прошла))");
                ParentVM.AuthentificationChecked(userName);
            }
            else
            {
                MessageBox.Show(message);
            }

            // MessageBox.Show(UserName + " " + Name + " " + Surname + " " + Password + " " + ConfirmedPassword);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}