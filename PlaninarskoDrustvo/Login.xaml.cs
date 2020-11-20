using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlaninarskoDrustvo
{
    public partial class Login : Window
    {
        bool AlreadyFaded;
        public Login()
        {
            InitializeComponent();
            loginImage.Source = new BitmapImage(new Uri( "pack://application:,,,/Resources/loginImage.jpg"));
        }

        private void WindowDrag(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text == Password.Password)
            {
                LoginPage.Visibility = Visibility.Collapsed;
                PasswordChangePage.Visibility = Visibility.Visible;
            }
            else
            {
                using (Model1 model = new Model1())
                {
                    var user = (from c in model.accounts
                                where c.username == Username.Text
                                select c).FirstOrDefault();
                    if (user != null)
                    {
                        bool doesPasswordMatch =  BCrypt.Net.BCrypt.Verify(Password.Password, user.password);
                        if (doesPasswordMatch)
                            if (user.type == "admin")
                            {
                                CurrentSession.User = user;
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                CurrentSession.User = user;
                                UserMainWindow mainWindow = new UserMainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                        else
                        {
                            Username.Text = "";
                            Password.Password = "";
                            ErrorIcon.Visibility = Visibility.Visible;
                            ErrorMessage.Visibility = Visibility.Visible;
                            ErrorIconPassword.Visibility = Visibility.Visible;
                            ErrorMessagePassword.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        Username.Text = "";
                        Password.Password = "";
                        ErrorIcon.Visibility = Visibility.Visible;
                        ErrorMessage.Visibility = Visibility.Visible;
                        ErrorIconPassword.Visibility = Visibility.Visible;
                        ErrorMessagePassword.Visibility = Visibility.Visible;
                    }

                }
            }
        }

        private void ButtonSavePassword_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordNew.Password != PasswordNewConfirm.Password)
            {
                ErrorMessagePasswordConfirm.Visibility = Visibility.Visible;
                ErrorIconPasswordConfirm.Visibility = Visibility.Visible;
            }
            else
            {
                using (Model1 model = new Model1())
                {
                    var user = (from c in model.accounts
                                where c.username == Username.Text
                                select c).FirstOrDefault();
                    string myPassword = PasswordNew.Password;
                    string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
                    string myHash = BCrypt.Net.BCrypt.HashPassword(myPassword, mySalt);
                    user.password = myHash;
                    model.SaveChanges();                

                }
                PasswordChangePage.Visibility = Visibility.Collapsed;
                LoginPage.Visibility = Visibility.Visible;
                Password.Password = PasswordNew.Password;
            }
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

        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            ErrorIcon.Visibility = Visibility.Hidden;
            ErrorMessage.Visibility = Visibility.Hidden;
            ErrorIconPassword.Visibility = Visibility.Hidden;
            ErrorMessagePassword.Visibility = Visibility.Hidden;
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            ErrorIcon.Visibility = Visibility.Hidden;
            ErrorMessage.Visibility = Visibility.Hidden;
            ErrorIconPassword.Visibility = Visibility.Hidden;
            ErrorMessagePassword.Visibility = Visibility.Hidden;
        }

        private void PasswordChange_GotFocus(object sender, RoutedEventArgs e)
        {
            ErrorIconPasswordConfirm.Visibility = Visibility.Hidden;
            ErrorMessagePasswordConfirm.Visibility = Visibility.Hidden;
        }
    }
}
