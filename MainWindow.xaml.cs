using Shelter.Database;
using Shelter.Pages;
using Svg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Shelter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Client AuthClient;
        DispatcherTimer timer = new DispatcherTimer();
        public static HttpClient HttpClient = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += CheckNotificationsAndNoActivity;
            timer.Start();
            MainFrame.Navigate(new AnimalsPage());
            if (Properties.Settings.Default.AuthorizedClientId != 0)
            {
                MainWindow.AuthClient = DB.entities.Client.FirstOrDefault(c => c.ClientId == Properties.Settings.Default.AuthorizedClientId);
                if (AuthClient == null)
                {
                    ChangeInterfaceByAccount(false);
                    Properties.Settings.Default.AuthorizedClientId = 0;
                    Properties.Settings.Default.Save();

                } else
                {
                    ChangeInterfaceByAccount(true);
                }

            }
        }
        int seconds = 0;
        int lastLength;
        int notReadNotifications;
        private void CheckNotificationsAndNoActivity(object sender, EventArgs e)
        {
            if (seconds == 30 && AuthClient != null)
            {
                ChangeInterfaceByAccount(false);
                Properties.Settings.Default.AuthorizedClientId = 0;
                Properties.Settings.Default.Save();
                Notifications.AppendLine($"{DateTime.Now} Выход из-за бездействия {MainWindow.AuthClient.Login}");
                AuthClient = null;
                seconds = 0;
            }
            else if (seconds == 30)
            {
                seconds = 0;
            }
            else
            {
                seconds++;
            }

            
                if (lastLength != Notifications.ToString().Split('\n').Length) // если есть новые уведомления
                {
                    notReadNotifications = Notifications.ToString().Split('\n').Length - lastLength;
                    lastLength = Notifications.ToString().Split('\n').Length;
                }
            

            if (notReadNotifications != 0)
            {
                NotificationsNumber.Visibility = Visibility.Visible;
                NNumber.Visibility = Visibility.Visible;
                NNumber.Text = notReadNotifications.ToString();
            }
            else
            {
                NotificationsNumber.Visibility = Visibility.Collapsed;
                NNumber.Visibility = Visibility.Collapsed;
            }
        }

        public static StringBuilder Notifications = new StringBuilder();
        private void LoginBt_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AuthPage());
        }

        bool IsCtrlPressed = false;
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
           
            if (AuthClient == null)
            {
                ChangeInterfaceByAccount(false);
            }
            else
            {
                ChangeInterfaceByAccount(true);
            }

            if (MainFrame.CanGoBack)
            {
                BackBt.Visibility = Visibility.Visible;
            }
            else
            {
                BackBt.Visibility = Visibility.Collapsed;
            }
        }
        void ChangeInterfaceByAccount(bool IsSignUp)
        {
            if (IsSignUp) 
            {
                LogoutBt.Visibility = Visibility.Visible;
                LoginBt.Visibility = Visibility.Collapsed;
                NotificationBt.Visibility = Visibility.Visible;
                NotificationsNumber.Visibility = Visibility.Visible;
                NNumber.Visibility = Visibility.Visible;
            }
            else 
            {
                LoginBt.Visibility = Visibility.Visible;
                LogoutBt.Visibility = Visibility.Collapsed;
                NotificationBt.Visibility = Visibility.Collapsed;
                NotificationsNumber.Visibility = Visibility.Collapsed;
                NNumber.Visibility = Visibility.Collapsed;
            }
        }
        private void LogoutBt_Click(object sender, RoutedEventArgs e)
        {
            Notifications.AppendLine($"{DateTime.Now} Успешный выход {MainWindow.AuthClient.Login}");
            AuthClient = null;
            Properties.Settings.Default.AuthorizedClientId = 0;
            Properties.Settings.Default.Save();
            ChangeInterfaceByAccount(false);
        }

        private void BackBt_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        private void NotificationBt_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new NotificationsPage());
            NotificationBt.Visibility = Visibility.Collapsed;
            NotificationsNumber.Visibility = Visibility.Collapsed;
            NNumber.Visibility = Visibility.Collapsed;
            notReadNotifications = 0;
            NNumber.Text = notReadNotifications.ToString();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.WriteAllText($"C:\\Users\\masha\\Desktop\\docs\\уведомления_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.txt", Notifications.ToString());
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            seconds = 0;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            seconds = 0;
            if (e.Key == Key.LeftCtrl)
            {
                IsCtrlPressed = true;
            }
            if (IsCtrlPressed && e.Key == Key.H)
            {

                if ((MainFrame.Content as AuthPage) != null)
                {
                    MainFrame.Navigate(new AllAuthsPage());
                }
            }
            if (IsCtrlPressed && e.Key == Key.Q)
            {
                if ((MainFrame.Content as AnimalsPage) != null)
                {
                    MainFrame.Navigate(new DonationsPage());
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            seconds = 0;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            seconds = 0;
            if (e.Key == Key.LeftCtrl)
            {
                IsCtrlPressed = false;
            }
        }

        private void ClientBt_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientsPage());
        }
    }
}
