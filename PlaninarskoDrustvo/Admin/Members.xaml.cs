using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using BCrypt.Net;
using System.IO;

namespace PlaninarskoDrustvo
{
  
    public partial class Members : UserControl
    {
        public ObservableCollection<member> CollectionOfMembers { get; set; }
        public int SelectedMemberId { get; set; }
        public string SelectedMemberPicPath { get; set; }

        public Members()
        {
            InitializeComponent();
            GetActiveUserCollection();
        }

        public void GetActiveUserCollection()
        {
            using (Model1 model = new Model1())
            {
                var all = model.members.Select(m => new { m.id, m.first_name, m.last_name, m.join_date, m.leaving_date, m.account, m.account.profile_pic }).Where(m => m.leaving_date == null).Distinct().ToList();
                CollectionOfMembers = new ObservableCollection<member>();
                foreach (var citem in all)
                {
                    string photoName = System.IO.Path.GetFileName(citem.account.profile_pic);
                    string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Members\\", photoName);
                    citem.account.profile_pic = photoPath;
                    CollectionOfMembers.Add(new member()
                    {
                        id = citem.id,
                        first_name = citem.first_name,
                        last_name = citem.last_name,
                        formatedJoinDate = citem.join_date.ToString("dd.MM.yyyy"),
                        formatedLeavingDate = citem.leaving_date.HasValue ? citem.leaving_date.Value.ToString("dd.MM.yyyy") : "",
                        account = citem.account,                        
                    });
                }
            }
            Users.ItemsSource = CollectionOfMembers;
        }

        public void GetInactiveUserCollection()
        {
            using (Model1 model = new Model1())
            {
                var all = model.members.Select(m => new { m.id, m.first_name, m.last_name, m.join_date, m.leaving_date, m.account }).Where(m => m.leaving_date != null).Distinct().ToList();
                CollectionOfMembers = new ObservableCollection<member>();
                foreach (var citem in all)
                {
                    string photoName = System.IO.Path.GetFileName(citem.account.profile_pic);
                    string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Members\\", photoName);
                    citem.account.profile_pic = photoPath;
                    CollectionOfMembers.Add(new member()
                    {
                        id = citem.id,
                        first_name = citem.first_name,
                        last_name = citem.last_name,
                        formatedJoinDate = citem.join_date.ToString("dd.MM.yyyy"),
                        formatedLeavingDate = citem.leaving_date.Value.ToString("dd.MM.yyyy"),
                        account = citem.account
                    });
                }
            }
            Users.ItemsSource = CollectionOfMembers;
        }
        private void Active_Click(object sender, RoutedEventArgs e)
        {
            Cursor.Margin = new Thickness(0, 30, 0, 0);
            GetActiveUserCollection();
            Details.Visibility = Visibility.Collapsed;
            Filters.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
        }
        private void Inactive_Click(object sender, RoutedEventArgs e)
        {
            Cursor.Margin = new Thickness(200, 30, 0, 0);
            GetInactiveUserCollection();
            Details.Visibility = Visibility.Collapsed;
            Filters.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
        }

        private void ArrowBack_Click(object sender, RoutedEventArgs e)
        {
            Details.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
        }
        private void Details_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            Details.Visibility = Visibility.Visible;
            ListBoxItem btn = sender as ListBoxItem;
            var dataObject = btn.DataContext as member;
            string photoName = System.IO.Path.GetFileName(dataObject.account.profile_pic);
            string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Members\\", photoName);
            DetailsPic.ImageSource = new BitmapImage(new Uri(photoPath));
            DetailsUsername.Text = dataObject.account.username;
            DetailsType.Text = dataObject.account.type;
            DetailsFirstName.Text = dataObject.first_name;
            DetailsLastName.Text = dataObject.last_name;
            DetailsJoindate.Text = dataObject.formatedJoinDate;
            DetailsLeavingDate.Text = dataObject.formatedLeavingDate;
        }
        private void Edit_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Edit.Visibility = Visibility.Visible;
            ListBoxItem btn = sender as ListBoxItem;
            var dataObject = btn.DataContext as member;
            SelectedMemberId = dataObject.id;
            string photoName = System.IO.Path.GetFileName(dataObject.account.profile_pic);
            string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Members\\", photoName);
            SelectedMemberPicPath = photoPath;
            EditPic.ImageSource = new BitmapImage(new Uri(photoPath));
            EditUsername.Text = dataObject.account.username;
            EditType.Text = dataObject.account.type;
            EditFirstName.Text = dataObject.first_name;
            EditLastName.Text = dataObject.last_name;
            EditJoinDate.Text = dataObject.formatedJoinDate;
            EditLeavingDate.Text = dataObject.formatedLeavingDate;
        }
        private void Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files(*.png;*.jpg)|*.png;*.jpg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFileDialog.ShowDialog() == true)
            {
                EditPic.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                SelectedMemberPicPath = openFileDialog.FileName;
            }
        }
        private void EditSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var imageName = System.IO.Path.GetFileName(SelectedMemberPicPath);
                string profileImage = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Members\\" + imageName;
                System.IO.File.Copy(SelectedMemberPicPath, profileImage, true);

                using (Model1 model = new Model1())
                {
                    var editAcc = (from c in model.accounts where c.member_id == SelectedMemberId select c).FirstOrDefault();
                    if (editAcc != null)
                    {
                        editAcc.username = EditUsername.Text;
                        editAcc.profile_pic = "..\\..\\Resources\\Members\\" + imageName;
                        model.SaveChanges();
                    }
                    var editMem = (from c in model.members where c.id == SelectedMemberId select c).FirstOrDefault(); 
                    if (editMem != null)
                    {
                        editMem.first_name = EditFirstName.Text;
                        editMem.last_name = EditLastName.Text;
                        editMem.join_date = DateTime.ParseExact(EditJoinDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        if (EditLeavingDate.Text != "")
                            editMem.leaving_date = DateTime.ParseExact(EditLeavingDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        else
                            editMem.leaving_date = null;
                        model.SaveChanges();
                    }
                }
                if (EditLeavingDate.Text != "")
                {
                    Inactive_Click(sender, e);
                }
                else
                {
                    Active_Click(sender, e);
                }
                Edit.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }

        private void All_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GetActiveUserCollection();
        }
        private void MembershipFeeUnpayed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            using (Model1 model = new Model1())
            {
                var unpayedFee = model.membership_fee.Select(m => new { m.member_id, m.year }).Where(m => m.year != DateTime.Now.Year).ToList();              
                foreach (var citem in unpayedFee)
                {
                    var unpayedFeeMembers = model.members.Select(m => new { m.id, m.first_name, m.last_name, m.join_date, m.leaving_date, m.account }).Where(m => m.id==citem.member_id).Distinct().ToList();
                    CollectionOfMembers = new ObservableCollection<member>();
                    foreach(var el in unpayedFeeMembers)
                    CollectionOfMembers.Add(new member()
                    {
                        id = el.id,
                        first_name = el.first_name,
                        last_name = el.last_name,
                        formatedJoinDate = el.join_date.ToString("dd.MM.yyyy"),
                        formatedLeavingDate = el.leaving_date.HasValue ? el.leaving_date.Value.ToString("dd.MM.yyyy") : "",
                        account = el.account
                    });
                }
            }
            Users.ItemsSource = CollectionOfMembers;
        }
        private void MembershipFeePayed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            using (Model1 model = new Model1())
            {
                var unpayedFee = model.membership_fee.Select(m => new { m.member_id, m.year }).Where(m => m.year == DateTime.Now.Year).ToList();
                foreach (var citem in unpayedFee)
                {
                    var unpayedFeeMembers = model.members.Select(m => new { m.id, m.first_name, m.last_name, m.join_date, m.leaving_date, m.account }).Where(m => m.id == citem.member_id).Distinct().ToList();
                    CollectionOfMembers = new ObservableCollection<member>();
                    foreach (var el in unpayedFeeMembers)
                        CollectionOfMembers.Add(new member()
                        {
                            id = el.id,
                            first_name = el.first_name,
                            last_name = el.last_name,
                            formatedJoinDate = el.join_date.ToString("dd.MM.yyyy"),
                            formatedLeavingDate = el.leaving_date.HasValue ? el.leaving_date.Value.ToString("dd.MM.yyyy") : "",
                            account = el.account
                        });
                }
            }
            Users.ItemsSource = CollectionOfMembers;
        }
        private void AddNewMember_Click(object sender, RoutedEventArgs e)
        {
            AddFirstName.Text = "";
            AddLastName.Text = "";
            AddType.Text = "";
            AddUsername.Text = "";
            Add.Visibility = Visibility.Visible;
       
        }
        private void AddSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                member newMember;
                account newAccount;
                using (Model1 model = new Model1())
                {

                    newMember = new member()
                    {
                        first_name = AddFirstName.Text,
                        last_name = AddLastName.Text,
                        join_date = DateTime.Now,
                    };

                    model.members.Add(newMember);
                    model.SaveChanges();

                    var member = model.members.Select(m => new { m.id }).OrderByDescending(m => m.id).First();

                    string myPassword = AddUsername.Text;
                    string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
                    string myHash = BCrypt.Net.BCrypt.HashPassword(myPassword, mySalt);
                    newAccount = new account()
                    {
                        username = AddUsername.Text,
                        password = myHash,
                        type = AddType.Text,
                        profile_pic = "pack://application:,,,/Resources/user-default.png",
                        theme = 1,
                        member_id=member.id,
                    };
                    model.accounts.Add(newAccount);
                    model.SaveChanges();

                }
                GetActiveUserCollection();
                Add.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                ErrorMessageAdd.Visibility = Visibility.Visible;
                using (Model1 model = new Model1())
                {
                    var acc = model.accounts.Select(m => new { m.member_id }).OrderByDescending(m => m.member_id).First();
                    var mem2 = (from c in model.members orderby c.id descending
                               select c).FirstOrDefault();
                    if ( mem2!= null && acc!=null && acc.member_id!= mem2.id)
                    {
                        model.members.Remove(mem2);
                        model.SaveChanges();
                    }
                }
            }
        }

    }
}
