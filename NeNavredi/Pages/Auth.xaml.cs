using DBLibrary;
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

namespace NeNavredi.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void ShowPwd_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                PwdText.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                PwdText.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordBox.Password = PwdText.Text;
                PasswordBox.Visibility = Visibility.Visible;
                PwdText.Visibility = Visibility.Collapsed;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Manager.User._currentUser = null;
        }

        private void RegenCaptcha_Click(object sender, RoutedEventArgs e)
        {
            ReGenCaptcha();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility != Visibility.Visible)
                PasswordBox.Password = PwdText.Text;
            
            StringBuilder errs = new StringBuilder();

            if (Login.Text == string.Empty)
                errs.AppendLine("Заполните логин.");
            if (PasswordBox.Password == string.Empty)
                errs.AppendLine("Заполните пароль.");
            if (CaptchaSP.Visibility == Visibility.Visible && CaptchaTBox.Text == string.Empty)
                errs.AppendLine("Заполните Captcha.");

            if (!string.IsNullOrEmpty(errs.ToString()))
            {
                MessageBox.Show(errs.ToString(), "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CaptchaSP.Visibility == Visibility.Visible && CaptchaTBox.Text != CaptchaBox.CaptchaText)
            {
                MessageBox.Show("Captcha введена неверно!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                ReGenCaptcha();
                return;
            }

            User user;

            try
            {
                user = Manager.DB.context.User.FirstOrDefault(u => u.Login == Login.Text && u.Password == PasswordBox.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user == null && CaptchaSP.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Логин или пароль введены неверно. Система будет заблакирована!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                Manager.Frame.Main.Navigate(new Pages.Lock(0,0,15));
            }

            if (user == null)
            {
                MessageBox.Show("Логин или пароль введены неправильно, проверьте правильность написания.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                CaptchaSP.Visibility = Visibility.Visible;
                ReGenCaptcha();
                return;
            }

            Manager.User._currentUser = user;

            switch(Manager.User._currentUser.RoleID)
            {
                case 1:
                    Manager.Frame.Main.Navigate(new Pages.Admin.Admin());
                    break;
                case 2:
                    Manager.Frame.Main.Navigate(new Pages.Accountant.Accountant());
                    break;
                case 3:
                    Manager.Frame.Main.Navigate(new Pages.Assistant.Assistant());
                    break;
                case 4:
                    Manager.Frame.Main.Navigate(new Pages.AssistansScientist.AssistansScientist());
                    break;
                case 5:
                    Manager.Frame.Main.Navigate(new Pages.Client.Client());
                    break;
                default:
                    MessageBox.Show($"Для вашей роли {Manager.User._currentUser.Role.Name} страница не предусмотренна!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        private void ReGenCaptcha()
        {
            CaptchaBox.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 6);
        }
    }
}
