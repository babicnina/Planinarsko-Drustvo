using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlaninarskoDrustvo
{
    public partial class UserMainWindow : Window
    {
        public bool HikingOpen { get; set; }
        public bool ActivityOpen { get; set; }
        bool AlreadyFaded;
        public static bool Lang1 = false;
        public static bool Lang2 = false;

        public UserMainWindow()
        {
            
            this.FontFamily = new FontFamily("Corbel");
            ChangeToUserLanguage(CurrentSession.User.language);

            InitializeComponent();

            setLoggedUserInfo();

            AddHandler(UserProfile.UserProfileChanged,
               new RoutedEventHandler(RefreshUserInfo));
            Event_Click(new object(), new RoutedEventArgs());
        }

        private void RefreshUserInfo(object sender, RoutedEventArgs e)
        {
            setLoggedUserInfo();
        }
       private void setLoggedUserInfo()
        {
            button.ToolTip = CurrentSession.User.member.first_name + CurrentSession.User.member.last_name;
            ChangeToUserTheme(CurrentSession.User.theme);
            var brush = new ImageBrush();
            string photoName = System.IO.Path.GetFileName(CurrentSession.User.profile_pic);
            string photoPath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Members\\", photoName);
            brush.ImageSource = new BitmapImage(new Uri(photoPath));
            button.Background = brush;
        }

        public void ChangeToUserTheme(int themeNumber)
        {
            if(themeNumber == 1)
                MkThemeSelector.SetCurrentThemeDictionary(this, new Uri("Theme1.xaml", UriKind.Relative));
            else if(themeNumber == 2)
                MkThemeSelector.SetCurrentThemeDictionary(this, new Uri("Theme2.xaml", UriKind.Relative));
            else
                MkThemeSelector.SetCurrentThemeDictionary(this, new Uri("Theme3.xaml", UriKind.Relative));
        }

        public void ChangeToUserLanguage(int languageNumber)
        {
            if (languageNumber == 1)
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("sr-Latn-BA");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            if (Info.Visibility == Visibility.Collapsed)
            {
                string fileName = "";
                if (CurrentSession.User.language == 1)
                    fileName = "aboutus.txt";
                else
                    fileName = "onama.txt";
                string filePath = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, fileName);
                using (StreamReader stream = new StreamReader(filePath))
                {
                    string line;
                    string allText = string.Empty;
                    while ((line = stream.ReadLine()) != null)
                    {
                        allText += line + "\n";
                    }
                    Gallery.Visibility = Visibility.Collapsed;
                    Events.Visibility = Visibility.Collapsed;
                    Profile.Visibility = Visibility.Collapsed;
                    Info.Visibility = Visibility.Visible;
                    InfoText.Text = allText;
                }
            }
            else if (Info.Visibility == Visibility.Visible)
            {
                Info.Visibility = Visibility.Collapsed;
            }

        }

        private void Gallery_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Collapsed;
            if (Gallery.Visibility == Visibility.Collapsed)
            {
                Gallery.Visibility = Visibility.Visible;
                Events.Visibility = Visibility.Collapsed;
                Info.Visibility = Visibility.Collapsed;
                Gallery.GalleryAlbum.Visibility = Visibility.Visible;
                Gallery.AllPhotos.Visibility = Visibility.Collapsed;
            }
            else if (Gallery.Visibility == Visibility.Visible)
            {
                Gallery.Visibility = Visibility.Collapsed;
            }
        }

        private void Action_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Collapsed;
            if (Events.Visibility == Visibility.Collapsed)
            {
                ActivityOpen = true;
                Events.GetActivityCollection();
                Events.Visibility = Visibility.Visible;
                Gallery.Visibility = Visibility.Collapsed;
                Info.Visibility = Visibility.Collapsed;
                Events.Details.Visibility = Visibility.Collapsed;
            }
            else if (Events.Visibility == Visibility.Visible && HikingOpen == true)
            {
                ActivityOpen = true;
                HikingOpen = false;
                Events.GetActivityCollection();
                Events.Visibility = Visibility.Visible;
                Gallery.Visibility = Visibility.Collapsed;
                Info.Visibility = Visibility.Collapsed;
                Events.Details.Visibility = Visibility.Collapsed;
            }
            else if (Events.Visibility == Visibility.Visible && HikingOpen == false)
            {
                ActivityOpen = false;
                Events.Visibility = Visibility.Collapsed;
            }
        }

        private void Event_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Collapsed;
            if (Events.Visibility == Visibility.Collapsed)
            {
                HikingOpen = true;
                Events.GetHikingCollection();
                Events.Visibility = Visibility.Visible;
                Gallery.Visibility = Visibility.Collapsed;
                Info.Visibility = Visibility.Collapsed;
                Events.Details.Visibility = Visibility.Collapsed;
            }
            else if (Events.Visibility == Visibility.Visible && ActivityOpen == true)
            {
                HikingOpen = true;
                ActivityOpen = false;
                Events.GetHikingCollection();
                Events.Visibility = Visibility.Visible;
                Gallery.Visibility = Visibility.Collapsed;
                Info.Visibility = Visibility.Collapsed;
                Events.Details.Visibility = Visibility.Collapsed;
            }
            else if (Events.Visibility == Visibility.Visible && ActivityOpen == false)
            {
                HikingOpen = false;
                Events.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (LanguageChooser.Visibility == Visibility.Collapsed)
                LanguageChooser.Visibility = Visibility.Visible;
            else if (LanguageChooser.Visibility == Visibility.Visible)
                LanguageChooser.Visibility = Visibility.Collapsed;

        }
        private void ButtonTheme_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeChooser.IsDropDownOpen == false)
            {
                ThemeChooser.Visibility = Visibility.Visible;
                ThemeChooser.IsDropDownOpen = true;
            }
            else if (ThemeChooser.IsDropDownOpen == true)
            {
                ThemeChooser.Visibility = Visibility.Hidden;
                ThemeChooser.IsDropDownOpen = false;
            }

        }
        private void ButtonWindowRestore_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonRestore.Kind == MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize)
            {
                ButtonRestore.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowRestore;
                this.WindowState = WindowState.Maximized;
            }
            else if (ButtonRestore.Kind == MaterialDesignThemes.Wpf.PackIconKind.WindowRestore)
            {
                ButtonRestore.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
                this.WindowState = WindowState.Normal;
            }

        }

        private void Language1_Click(object sender, MouseButtonEventArgs e)
        {
            LanguageChooser.Visibility = Visibility.Collapsed;
            Lang1 = true;
            Lang2 = false;
            using (Model1 model = new Model1())
            {
                var user = (from c in model.accounts
                            where c.username == CurrentSession.User.username
                            select c).FirstOrDefault();
                user.language = 1;
                model.SaveChanges();
            }

            UserMainWindow mainWindow = new UserMainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void Language2_Click(object sender, MouseButtonEventArgs e)
        {
            LanguageChooser.Visibility = Visibility.Collapsed;
            Lang1 = false;
            Lang2 = true;
            using (Model1 model = new Model1())
            {
                var user = (from c in model.accounts
                            where c.username == CurrentSession.User.username
                            select c).FirstOrDefault();
                user.language = 2;
                model.SaveChanges();
            }
            UserMainWindow mainWindow = new UserMainWindow();
            mainWindow.Show();
            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AlreadyFaded = false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!AlreadyFaded)
            {
                AlreadyFaded = true;
                e.Cancel = true;
                var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
                anim.Completed += new EventHandler(anim_Completed);
                this.BeginAnimation(UIElement.OpacityProperty, anim);
            }
        }

        void anim_Completed(object sender, EventArgs e)
        {
            Close();
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ThemeChooser.Visibility = Visibility.Hidden;
        }

        private void Logout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void ProfileImage_Click(object sender, RoutedEventArgs e)
        {
            if (Profile.Visibility == Visibility.Collapsed)
            {
                Profile.Visibility = Visibility.Visible;
                Profile.Details_Show();
            }

            else
                Profile.Visibility = Visibility.Collapsed;
        }
    }
}
