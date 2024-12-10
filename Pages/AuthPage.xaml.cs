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
        }
        int counts = 0;

        private void AuthBt_Click(object sender, RoutedEventArgs e)
        {
            
            if (LoginBox.Text == "" || PasswordBox.Password == "")
            {
                MessageBox.Show("", "");
                return;
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                IsCtrlPressed = true;
            }
            if (IsCtrlPressed && e.Key == Key.H)
            {
                NavigationService.Navigate(new AllAuthsPage());
            }
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                IsCtrlPressed = false;
            }
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // если есть то меняем картинку, если нет - то заглушку
        }
    }
}
