using Gigachat.Models;
using Gigachat.Core;
using MessageBox = System.Windows.MessageBox;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;

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

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
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

        private BitmapSource profileImage;
        public BitmapSource ProfileImage
        {
            get { return profileImage; }
            set
            {
                profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
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

            Login = "";
            FullName = "";
            date = "";
            ProfileImage = null;
            LoadProfile();
        }

        public void LoadProfile()
        {
            int result;
            string message;
            (result, message, UserProfile) = Client.GetUserProfile(Client.UserName);
            MessageBox.Show(UserProfile.Name + " " + UserProfile.Surname);
            Login = UserProfile.UserName.Substring(1, UserProfile.UserName.Length - 2);
            FullName = UserProfile.Name.Substring(1, UserProfile.Name.Length-2) + " " + UserProfile.Surname.Substring(1, UserProfile.Surname.Length - 2);
            Date = "Дата регистрации: " + UserProfile.Date;

            ProfileImage = LoadImage(ImageToByteArray(Image.FromFile("E:\\gigachad.jpg")));
            
        }

        BitmapSource LoadImage(byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
