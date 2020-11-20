using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

    public partial class UserProfile : UserControl
    {
        public string SelectedMemberPicPath { get; set; }

        public static readonly RoutedEvent UserProfileChanged = EventManager.RegisterRoutedEvent("UserProfileChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserProfile));

        public UserProfile()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler SettingConfirmed
        {
            add { AddHandler(UserProfileChanged, value); }
            remove { RemoveHandler(UserProfileChanged, value); }
        }
        private void ArrowBack_Click(object sender, RoutedEventArgs e)
        {
            Details.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Collapsed;
        }
        public void Details_Show()
        {
            Details.Visibility = Visibility.Visible;
            string photoName = System.IO.Path.GetFileName(CurrentSession.User.profile_pic);
            string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Members\\", photoName);
            DetailsPic.ImageSource = new BitmapImage(new Uri(photoPath));
            DetailsUsername.Text = CurrentSession.User.username;
            DetailsType.Text = CurrentSession.User.type;
            DetailsFirstName.Text = CurrentSession.User.member.first_name;
            DetailsLastName.Text = CurrentSession.User.member.last_name;
            DetailsJoindate.Text = CurrentSession.User.member.join_date.ToShortDateString();
        }
        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            Details.Visibility = Visibility.Collapsed;
            Edit.Visibility = Visibility.Visible;
            string photoName = System.IO.Path.GetFileName(CurrentSession.User.profile_pic);
            string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Members\\", photoName);
            EditPic.ImageSource = new BitmapImage(new Uri(photoPath));
            EditUsername.Text = CurrentSession.User.username;
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
                    var editAcc = (from c in model.accounts where c.member_id == CurrentSession.User.member_id select c).FirstOrDefault();
                    if (editAcc != null)
                    {
                        editAcc.username = EditUsername.Text;
                        if (SelectedMemberPicPath != "")
                            editAcc.profile_pic = "..\\..\\Resources\\Members\\" + imageName; ;
                        if (EditPassword.Text != "")
                        {
                            string myPassword = EditPassword.Text;
                            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
                            string myHash = BCrypt.Net.BCrypt.HashPassword(myPassword, mySalt);
                            editAcc.password = myHash;
                        }
                        model.SaveChanges();
                    }
                }
                CurrentSession.User.username = EditUsername.Text;
                if (SelectedMemberPicPath != "")
                    CurrentSession.User.profile_pic = profileImage;
                RaiseEvent(new RoutedEventArgs(UserProfile.UserProfileChanged));
                Edit.Visibility = Visibility.Collapsed;
                Details.Visibility = Visibility.Visible;
                Details_Show();

            }
            catch (Exception ex)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }

    }
}
