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
using System.Windows.Threading;

namespace NeNavredi.Pages
{
    /// <summary>
    /// Логика взаимодействия для Lock.xaml
    /// </summary>
    public partial class Lock : Page
    {
        private TimeSpan MaxTime;
        private TimeSpan UseTime = new TimeSpan(0,0,0);
        public Lock(int h, int m, int s)
        {
            InitializeComponent();
            MaxTime = new TimeSpan(h, m, s);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0,0,1);
            Timer.Tick += new EventHandler(TimerTick);
            Timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            UseTime += new TimeSpan(0,0,1);
            TextTimer.Text = (MaxTime - UseTime).ToString();

            if (UseTime >= MaxTime)
                Manager.Frame.Main.Navigate(new Pages.Auth());
        }
    }
}
