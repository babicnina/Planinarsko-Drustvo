using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlaninarskoDrustvo
{
  
    public partial class UserImageWindow : Window
    {
        public ObservableCollection<image> CollectionOfImages { get; set; }
        public bool IsDeactivate { get; set; }
        public string PreviewImage { get; set; }

        public UserImageWindow()
        {
            InitializeComponent();
            IsDeactivate = false;
        }

        public void ImagePrepareToShow(image img)
        {
            var mainWindow = Application.Current.Windows.OfType<UserMainWindow>().FirstOrDefault();
            Image.ImageSource = new BitmapImage(new Uri(img.path));
            this.Height = Image.ImageSource.Height > mainWindow.Height ? 630 : Image.ImageSource.Height; 
            this.Width = Image.ImageSource.Width > mainWindow.Width ? 1000 : Image.ImageSource.Width;
            this.Left = Application.Current.Windows.OfType<UserMainWindow>().FirstOrDefault().Left + (Application.Current.Windows.OfType<UserMainWindow>().FirstOrDefault().Width - this.Width) / 2;
            this.Top = Application.Current.Windows.OfType<UserMainWindow>().FirstOrDefault().Top + (Application.Current.Windows.OfType<UserMainWindow>().FirstOrDefault().Height - this.Height) / 2;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Close();
            IsDeactivate = true;
            var mainWindow = Application.Current.Windows.OfType<UserMainWindow>().FirstOrDefault();
            mainWindow.WholeWindow.Effect = new DropShadowEffect();
            mainWindow.WholeWindow.Opacity = 1;
        }

        private void ArrowBack_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;
            foreach (var el in CollectionOfImages)
            {
                if (el.path == PreviewImage && a > 0)
                {
                    a -= 1;
                    ImagePrepareToShow(CollectionOfImages[a]);
                    PreviewImage = CollectionOfImages[a].path;
                }
                else
                {
                }
                a++;
            }
        }

        private void ArrowForward_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;
            foreach (var el in CollectionOfImages)
            {
                if (el.path == PreviewImage && a < CollectionOfImages.Count - 1)
                {
                    a += 1;
                    PreviewImage = CollectionOfImages[a].path;
                    ImagePrepareToShow(CollectionOfImages[a]);
                }
                else
                {
                }
                a++;
            }
        }

    }
}
