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
    public partial class GalleryUC : UserControl
    {
        public string PreviewImage { get; set; }
        public ObservableCollection<gallery> CollectionOfGalleries { get; set; }
        public ObservableCollection<image> CollectionOfImages { get; set; }
        public ObservableCollection<image> CollectionOfImagesToSave { get; set; }
        public int NumberOfImages { get; set; }
        public string CoverImage { get; set; }
        public GalleryUC()
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
            Add.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Collapsed;
            AllPhotos.Visibility = Visibility.Collapsed;
        }

        public void GetNumberOfImages(int id)
        {
            using (Model1 model = new Model1())
            {
                var all = model.images.Select(m => new { m.id, m.path, m.gallery_id }).Where(m => m.gallery_id == id).ToList();
                NumberOfImages = all.Count();
                CoverImage = all.First().path;
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
                    string photoName = System.IO.Path.GetFileName(citem.path);
                    string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Photos\\", photoName);
                    CollectionOfImages.Add(new image() { id = citem.id, path = photoPath });
                    
                }
            }
            Photos.ItemsSource = CollectionOfImages;
        }
        private void ImagePreview_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.WholeWindow.Effect = new DropShadowEffect();
            mainWindow.WholeWindow.Opacity = 0.7;
            ImageWindow imageWindow = new ImageWindow();
            imageWindow.CollectionOfImages = CollectionOfImages;
            Border btn = sender as Border;
            var dataObject = btn.DataContext as image;
            imageWindow.PreviewImage = dataObject.path;
            /* imageWindow.Image.ImageSource = new BitmapImage(new Uri(dataObject.Image));
             imageWindow.Height = imageWindow.Image.ImageSource.Height > mainWindow.Height ? 630 : imageWindow.Image.ImageSource.Height; //ne radi bas
             imageWindow.Width = imageWindow.Image.ImageSource.Width > mainWindow.Width ? 1000 : imageWindow.Image.ImageSource.Width;
             imageWindow.Left = ((MainWindow)Application.Current.MainWindow).Left + (((MainWindow)Application.Current.MainWindow).Width - imageWindow.Width) / 2;
             imageWindow.Top = ((MainWindow)Application.Current.MainWindow).Top + (((MainWindow)Application.Current.MainWindow).Height - imageWindow.Height) / 2;
             double a = (((MainWindow)Application.Current.MainWindow).Height - imageWindow.Height) / 2;*/
            imageWindow.ImagePrepareToShow(dataObject);
            imageWindow.Show();
        }

        private void AddNewGallery_Click(object sender, RoutedEventArgs e)
        {
            using (Model1 model = new Model1())
            {
                var alreadyTaken = model.galleries.Select(x => x.event_id).ToArray();
                var availableEvents = model.events.Where(p => !alreadyTaken.Contains(p.id)).ToList();
                ObservableCollection<string> collectionOfEvents = new ObservableCollection<string>();
                foreach (var ev in availableEvents)
                {
                    collectionOfEvents.Add(ev.name);
                }
                AddEvent.ItemsSource = collectionOfEvents;
            }
            ErrorMessageAdd.Visibility = Visibility.Collapsed;
            AddTitle.Text = "";
            AddEvent.Text = "";
            Add.Visibility = Visibility.Visible;
        }

        private void AddImages_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files(*.png;*.jpg)|*.png;*.jpg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            CollectionOfImagesToSave = new ObservableCollection<image>();
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var im in openFileDialog.FileNames)
                {
                    image image = new image();
                    image.path = im;
                    CollectionOfImagesToSave.Add(image);
                    AddImages.AppendText(im);
                    AddImages.AppendText("\n");
                }
            }
        }

        private void AddSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            gallery newGallery;
            using (Model1 model = new Model1())
            {
                var gall = model.events.Select(m => new { m.id, m.name }).Where(m => m.name == AddEvent.Text).FirstOrDefault();
                newGallery = new gallery()
                {
                    name = AddTitle.Text,
                    time= DateTime.Now,
                    event_id =gall.id,
                };
                model.galleries.Add(newGallery);
                model.SaveChanges();
                var specificGallery = (from c in model.galleries where c.name == AddTitle.Text select c).FirstOrDefault();
                foreach (var item in CollectionOfImagesToSave)
                    {
                        var imageName = System.IO.Path.GetFileName(item.path);
                        string imageToSave = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Photos\\" + imageName;
                        System.IO.File.Copy(item.path, imageToSave, true);

                        var newImage = new image()
                        {
                            path = "..\\..\\Resources\\Photos\\"+ imageName,
                            gallery_id = specificGallery.id,
                        };
                        model.images.Add(newImage);
                        model.SaveChanges();
                    }
                    GetGalleryCollection();
            }
            Add.Visibility = Visibility.Collapsed;
        }
                catch (Exception ex)
                {
                    ErrorMessageAdd.Visibility = Visibility.Visible;
                }
            AddImages.Document.Blocks.Clear();

        }
     

    }
       
}
