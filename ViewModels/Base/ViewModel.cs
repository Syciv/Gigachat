using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chatt.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {

        private Brush background;
        public Brush Background
        {
            get { return background; }
            set
            {
                background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        private Brush element1;
        public Brush Element1
        {
            get { return element1; }
            set
            {
                element1 = value;
                OnPropertyChanged(nameof(Element1));
            }
        }

        private Brush element2;
        public Brush Element2
        {
            get { return element2; }
            set
            {
                element2 = value;
                OnPropertyChanged(nameof(Element2));
            }
        }

        private Brush foreground;
        public Brush Foreground
        {
            get { return foreground; }
            set
            {
                foreground = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }

        public void UpdateSettings()
        {
            System.Drawing.Color backgroundColor = Properties.Settings.Default.BackgroundColor;
            Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(backgroundColor.R, backgroundColor.G, backgroundColor.B));

            System.Drawing.Color elementColor1 = Properties.Settings.Default.ElementColor1;
            Element1 = new SolidColorBrush(System.Windows.Media.Color.FromRgb(elementColor1.R, elementColor1.G, elementColor1.B));

            System.Drawing.Color elementColor2 = Properties.Settings.Default.ElementColor2;
            Element2 = new SolidColorBrush(System.Windows.Media.Color.FromRgb(elementColor2.R, elementColor2.G, elementColor2.B));

            System.Drawing.Color foregroundColor = Properties.Settings.Default.ForegroundColor;
            Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(foregroundColor.R, foregroundColor.G, foregroundColor.B));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
