using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;

namespace PlaninarskoDrustvo
{

    public partial class UserGalleryUC : UserControl
    {
        public string PreviewImage { get; set; }
        public ObservableCollection<gallery> CollectionOfGalleries { get; set; }
        public ObservableCollection<image> CollectionOfImages { get; set; }
        public ObservableCollection<image> CollectionOfImagesToSave { get; set; }
        public int NumberOfImages { get; set; }
        public string CoverImage { get; set; }

        public UserGalleryUC()
        {
            InitializeComponent();
            GetGalleryCollection();
        }

        private void GalleryOpen_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as gallery;
            GetImageCollection(dataObject.id);
            AllPhotos.Visibility = Visibility.Visible;
        }

        private void ArrowBack_Click(object sender, RoutedEventArgs e)
        {
            AllPhotos.Visibility = Visibility.Collapsed;
        }

        public void GetNumberOfImages(int id)
        {
            using (Model1 model = new Model1())
            {
                var all = model.images.Select(m => new { m.id, m.path, m.gallery_id }).Where(m => m.gallery_id == id).Distinct().ToList();
                NumberOfImages = all.Count();
                CoverImage = all[0].path;
            }
        }
        public void GetGalleryCollection()
        {
            using (Model1 model = new Model1())
            {
                var all = model.galleries.Select(m => new { m.id, m.name, m.time }).Distinct().ToList();
                CollectionOfGalleries = new ObservableCollection<gallery>();
                foreach (var citem in all)
                {
                    GetNumberOfImages(citem.id);
                    string photoName = System.IO.Path.GetFileName(CoverImage);
                    string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Photos\\", photoName);
                    CollectionOfGalleries.Add(new gallery() { id = citem.id, name = citem.name, time = citem.time, formatedDate = citem.time.ToString("dd.MM.yyyy"), numOfImages = NumberOfImages, coverImage = photoPath });
                }
            }
            Galleries.ItemsSource = CollectionOfGalleries;
        }
        public void GetImageCollection(int id)
        {

            using (Model1 model = new Model1())
            {
                var all = model.images.Select(m => new { m.id, m.path, m.gallery_id }).Where(m => m.gallery_id == id).Distinct().ToList();
                CollectionOfImages = new ObservableCollection<image>();
                foreach (var citem in all)
                {
                    string photoName = System.IO.Path.GetFileName(CoverImage);
                    string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Photos\\", photoName);
                    CollectionOfImages.Add(new image() { id = citem.id, path = photoPath });
                }
            }
            Photos.ItemsSource = CollectionOfImages;
        }
        private void ImagePreview_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as UserMainWindow;
            mainWindow.WholeWindow.Effect = new DropShadowEffect();
            mainWindow.WholeWindow.Opacity = 0.7;
            UserImageWindow imageWindow = new UserImageWindow();
            imageWindow.CollectionOfImages = CollectionOfImages;
            Border btn = sender as Border;
            var dataObject = btn.DataContext as image;
            imageWindow.PreviewImage = dataObject.path;
            imageWindow.ImagePrepareToShow(dataObject);
            imageWindow.Show();
        }


    }

}
