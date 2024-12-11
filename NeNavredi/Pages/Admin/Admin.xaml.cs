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

namespace NeNavredi.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WelcomeText.Text = $"Добрый день, {Manager.User._currentUser.LastName} {Manager.User._currentUser.FirstName}";
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Manager.Frame.Main.Navigate(new Pages.Auth());
        }

        private void BtnHistory_Click(object sender, RoutedEventArgs e)
        {
            Manager.Frame.Main.Navigate(new Pages.Admin.History());
        }
    }
}
