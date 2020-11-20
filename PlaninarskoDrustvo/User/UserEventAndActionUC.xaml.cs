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

    public partial class UserEventAndActionUC : UserControl
    {
        public ObservableCollection<_event> CollectionOfEvents { get; set; }
        public string TypeOfCollection { get; set; }
        public int SelectedEventId { get; set; }
        public string SelectedCoverImage { get; set; }
        public UserEventAndActionUC()
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
            Details.Visibility = Visibility.Visible;
        }

        private void ArrowBack_Click(object sender, RoutedEventArgs e)
        {
            Details.Visibility = Visibility.Collapsed;
        }
        public void GetHikingCollection()
        {
            TypeOfCollection = "hiking";
            using (Model1 model = new Model1())
            {
                var all = model.events.Select(m => new { m.id, m.name, m.start, m.cover, m.description, m.type }).Where(m => m.type == "hiking").Distinct().ToList();
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

    }
}
