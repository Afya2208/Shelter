using Shelter.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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

namespace Shelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        bool IsCtrlPressed = false;
        public AuthPage()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            if (Properties.Settings.Default.EnablingTime != null)
            {
                if (DateTime.Now >= Properties.Settings.Default.EnablingTime)
                {
                   
                    MainPanel.IsEnabled = true;
                }
                else
                {
                    seconds = (Properties.Settings.Default.EnablingTime - DateTime.Now).Seconds;
                    timer.Start();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (seconds == 30)
            {
                timer.Stop();
                MainPanel.IsEnabled = true;
            }
            
            else seconds++;
        } 

        int counts = 0;
        int seconds = 0;
        DispatcherTimer timer = new DispatcherTimer();
        private void AuthBt_Click(object sender, RoutedEventArgs e)
        {
            
            if (LoginBox.Text == "" || PasswordBox.Password == "")
            {
                MessageBox.Show("Вы оставили пустое поле", "Ошибка");
                MainWindow.Notifications.AppendLine($"{DateTime.Now} Некорректная попытка войти");
                return;
            }
            Client client;
            try
            {
                client = MainWindow.HttpClient.GetFromJsonAsync<Client>($"http://localhost:5010/Client/FindClientByLoginAndPassword?login={LoginBox.Text}&password={PasswordBox.Password}").Result;
            }
            catch (Exception ex)
            {
                client=null;
            }
            if (client != null)
            {   
               
                MainWindow.AuthClient = client;
                counts = 0;
                Properties.Settings.Default.AuthorizedClientId = client.ClientId;
                Properties.Settings.Default.Save();
                MainWindow.Notifications.AppendLine($"{DateTime.Now} Успешный вход {MainWindow.AuthClient.Login}");
                NavigationService.GoBack();
            } 
            else
            {
                counts++;
                MessageBox.Show("Неправильный пароль или логин", "Ошибка");
                MainWindow.Notifications.AppendLine($"{DateTime.Now} Неуспешная попытка войти");
                if (counts < 5)
                {
                    MessageBox.Show($"У вас осталось {5 - counts} попыток до блокировки", "Ошибка");
                } else
                {
                    Properties.Settings.Default.EnablingTime = DateTime.Now.AddSeconds(30);
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Авторизация будет доступна через 30 секунд", "Ошибка");
                    timer.Start();
                    MainPanel.IsEnabled = false;
                    counts = 0;
                    MainWindow.Notifications.AppendLine($"{DateTime.Now} Блокировка авторизации");
                }
                
                
                return;
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // если есть то меняем картинку, если нет - то заглушку
            Client client;
            try
            {
                client = MainWindow.HttpClient.GetFromJsonAsync<Client>($"http://localhost:5010/Client/FindClientByLogin?login={LoginBox.Text}").Result;
            }
            catch (Exception ex)
            {
                client = null;
            }
            if (client != null)
            {
                if (client.Photo == null)
                {
                    UserImage.Source = new BitmapImage(new Uri("\\Resources\\заглушка.jpg", UriKind.Relative));
                    UserImage.DataContext = null;
                } else
                UserImage.DataContext = client;
                
            } 
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Properties.Settings.Default.EnablingTime != null)
            {
                if (DateTime.Now >= Properties.Settings.Default.EnablingTime)
                {
                    
                    MainPanel.IsEnabled = true;
                }
                else
                {
                    seconds = (Properties.Settings.Default.EnablingTime - DateTime.Now).Seconds;
                    timer.Start();
                }
            }
        }

        private void Page_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            
        }


        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           
        }
    }
}
