using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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

namespace PlaninarskoDrustvo
{

    public partial class EventAndActionsUC : UserControl
    {
        public ObservableCollection<_event> CollectionOfEvents { get; set; }
        public string TypeOfCollection { get; set; }
        public int SelectedEventId { get; set; }
        public string SelectedCoverImage { get; set; }

        public EventAndActionsUC()
        {
            InitializeComponent();
        }

        private void EventAction_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as _event;
            SelectedEventId = dataObject.id;
            string photoName = System.IO.Path.GetFileName(dataObject.cover);
            string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Events\\", photoName);
            SelectedCoverImage = photoPath;
            EventName.Text = dataObject.name;
            EventDescription.Text = dataObject.description;

            if (TypeOfCollection == "hiking")
                DeleteEvent.Visibility = Visibility.Visible;
            else
                DeleteEvent.Visibility = Visibility.Collapsed;
            Details.Visibility = Visibility.Visible;
        }

        private void ArrowBack_Click(object sender, RoutedEventArgs e)
        {
            Details.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Collapsed;
        }
        public void GetHikingCollection()
        {
            TypeOfCollection = "hiking";
            using (Model1 model = new Model1())
            {
                var all = model.events.Select(m => new { m.id, m.name, m.start,m.cover, m.description, m.type }).Where(m => m.type == "hiking").Distinct().ToList();
                CollectionOfEvents = new ObservableCollection<_event>();
                foreach (var citem in all)
                {
                    string photoName = System.IO.Path.GetFileName(citem.cover);
                    string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Events\\", photoName);
                    CollectionOfEvents.Add(new _event() { id = citem.id, name = citem.name, start = citem.start, formatedStartDate = citem.start.ToString("dd.MM.yyyy"), cover = photoPath, description = citem.description, shortDescription = citem.description.Length > 50 ? citem.description.Substring(0, 45) : citem.description }) ;
                 
                }
            }
            Events.ItemsSource = CollectionOfEvents;
        }
        public void GetActivityCollection()
        {
            TypeOfCollection = "activity";
            using (Model1 model = new Model1())
            {
                var all = model.events.Select(m => new { m.id, m.name, m.start, m.cover, m.description, m.type }).Where(m => m.type == "activity").Distinct().ToList();
                CollectionOfEvents = new ObservableCollection<_event>();
                foreach (var citem in all)
                {
                    string photoName = System.IO.Path.GetFileName(citem.cover);
                    string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Events\\", photoName);
                    CollectionOfEvents.Add(new _event() { id = citem.id, name = citem.name, start = citem.start, formatedStartDate = citem.start.ToString("dd.MM.yyyy"), cover = photoPath, description = citem.description, shortDescription = citem.description.Length > 50 ? citem.description.Substring(0, 45) : citem.description });

                }
            }
            Events.ItemsSource = CollectionOfEvents;
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files(*.png;*.jpg)|*.png;*.jpg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFileDialog.ShowDialog() == true)
            {
                AddPic.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                EditPic.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                SelectedCoverImage = openFileDialog.FileName;
            }
        }
        private void AddNewEventOrAction_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessageAdd.Visibility = Visibility.Collapsed;
            AddPic.ImageSource = null;
            AddTitle.Text = "";
            AddStart.Text = "";
            AddEnd.Text = "";
            AddDescription.Text = "";
            Add.Visibility = Visibility.Visible;
        }
        private void EditEvent_Click(object sender, RoutedEventArgs e)
        {
            Edit.Visibility = Visibility.Visible;
            ErrorMessageEdit.Visibility = Visibility.Collapsed;
            using(Model1 model = new Model1())
            {
                var editEv = (from c in model.events where c.id == SelectedEventId select c).FirstOrDefault();
                if( editEv!=null)
                {
                    string photoName = System.IO.Path.GetFileName(SelectedCoverImage);
                    string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Events\\", photoName);
                    EditPic.ImageSource= new BitmapImage(new Uri(photoPath));
                    EditTitle.Text = editEv.name;
                    EditStart.Text = editEv.start.ToString("dd.MM.yyyy");
                    EditEnd.Text =editEv.end.HasValue? editEv.end.Value.ToString("dd.MM.yyyy"):"";
                    EditDescription.Text = editEv.description;
                }
            } 
        }
        private void EditSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Model1 model = new Model1())
                {
                    var imageName = System.IO.Path.GetFileName(SelectedCoverImage);
                    string coverImage = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Events\\" + imageName;
                    System.IO.File.Copy(SelectedCoverImage, coverImage, true); 

                    var editEv = (from c in model.events where c.id == SelectedEventId select c).FirstOrDefault(); 
                    if (editEv != null)
                    {
                        editEv.cover = "..\\..\\Resources\\Events\\" + imageName;
                        editEv.name = EditTitle.Text;
                        editEv.start = DateTime.ParseExact(EditStart.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        if (EditEnd.Text != "")
                            editEv.end = DateTime.ParseExact(EditEnd.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        else
                            editEv.end = null;
                        editEv.description = EditDescription.Text;
                        model.SaveChanges();
                    }
                }
                if (TypeOfCollection == "hiking")
                    GetHikingCollection();
                else
                    GetActivityCollection();
                Edit.Visibility = Visibility.Collapsed;
                Details.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                ErrorMessageEdit.Visibility = Visibility.Visible;
            }
        }

        private void AddSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var imageName = System.IO.Path.GetFileName(SelectedCoverImage);
                string coverImage = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Events\\" + imageName;
                System.IO.File.Copy(SelectedCoverImage, coverImage, true);

                _event newEvent;
                using (Model1 model = new Model1())
                {
                    newEvent = new _event()
                    {
                        name = AddTitle.Text,
                        start = DateTime.ParseExact(AddStart.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        description = AddDescription.Text,
                        type=TypeOfCollection,
                    };
                    if (AddPic.ImageSource!=null)
                    {
                        newEvent.cover = "..\\..\\Resources\\Events\\" + imageName;
                    }
                    else
                    {
                        newEvent.cover = "pack://application:,,,/Resources/event-default.jpg";
                    }
                    if (AddEnd.Text != "")
                        newEvent.end = DateTime.ParseExact(EditEnd.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    else
                        newEvent.end = null;
                    model.events.Add(newEvent);
                    model.SaveChanges();

                }
                if (TypeOfCollection == "hiking")
                    GetHikingCollection();
                else
                    GetActivityCollection();
                Add.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                ErrorMessageAdd.Visibility = Visibility.Visible;
            }
        
    }

        private void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            using (Model1 model = new Model1())
                {
                    var ev = (from c in model.events
                            where c.id == SelectedEventId
                            select c).FirstOrDefault();
                    var gall = (from c in model.galleries where c.event_id == ev.id
                               select c).FirstOrDefault();
             
                foreach (var citem in gall.images.ToList())
                {   
                    if (citem != null)
                    {
                         model.images.Remove(citem);
                        model.SaveChanges();
                    }
                }
                model.galleries.Remove(gall);
                model.SaveChanges();
                model.events.Remove(ev);
                model.SaveChanges();
            }
            Details.Visibility = Visibility.Collapsed;
            if (TypeOfCollection == "hiking")
            {
                GetHikingCollection();
            }
            else
            {
                GetActivityCollection();
            }
        }
    }
}
