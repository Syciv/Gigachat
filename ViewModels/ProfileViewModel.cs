using Gigachat.Models;
using Gigachat.Core;
using MessageBox = System.Windows.MessageBox;

namespace Chatt.ViewModels
{
    class ProfileViewModel:ViewModel 
    {

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private UserProfile userProfile;
        public UserProfile UserProfile
        {
            get { return userProfile; }
            set
            {
                userProfile = value;
                OnPropertyChanged(nameof(UserProfile));
            }
        }

        ClientObject Client { get; set; }

        public ProfileViewModel(ClientObject Client)
        {
            UpdateSettings();
            this.Client = Client;

            FullName = "";
            date = "";
            LoadProfile();
        }

        public void LoadProfile()
        {
            int result;
            string message;
            (result, message, UserProfile) = Client.GetUserProfile(Client.UserName);
            MessageBox.Show(UserProfile.Name + " " + UserProfile.Surname);
            FullName = UserProfile.Name.Substring(1, UserProfile.Name.Length-2) + " " + UserProfile.Surname.Substring(1, UserProfile.Surname.Length - 2);
            Date = UserProfile.Date;

        }
    }
}
