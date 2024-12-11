﻿using System;
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

namespace NeNavredi.Pages.AssistansScientist
{
    /// <summary>
    /// Логика взаимодействия для AssistansScientist.xaml
    /// </summary>
    public partial class AssistansScientist : Page
    {
        private TimeSpan MaxTime = new TimeSpan(0,1,30);
        private TimeSpan PushTime = new TimeSpan(0,1,0);
        private TimeSpan UseTime = new TimeSpan(0,0,0);
        public AssistansScientist()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Manager.Frame.Main.Navigate(new Pages.Auth());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WelcomeText.Text = $"Добрый день, {Manager.User._currentUser.LastName} {Manager.User._currentUser.FirstName}";

            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0,0,1);
            Timer.Tick += new EventHandler(TimerTicker);
            Timer.Start();
        }

        private void TimerTicker(object sender, EventArgs e)
        {
            UseTime += new TimeSpan(0,0,1);
            TimerText.Text = (MaxTime - UseTime).ToString();

            if (UseTime == MaxTime - PushTime)
            {
                MessageBox.Show("Система скоро заблокируется. Завершите работу и сохраните данные!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (UseTime >= MaxTime)
            {
                Manager.Frame.Main.Navigate(new Pages.Lock(0,1,0));
            }
        }
    }
}
