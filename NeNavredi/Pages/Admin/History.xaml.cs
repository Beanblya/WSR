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
using DBLibrary;

namespace NeNavredi.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {
        public History()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Ne_navredyEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(u => u.Reload());
            DGridHistory.ItemsSource = Ne_navredyEntities.GetContext().Authorization.ToList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.Frame.Main.GoBack();
        }
    }
}
