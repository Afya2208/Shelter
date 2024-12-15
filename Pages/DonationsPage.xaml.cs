using Aspose.Words.Bibliography;
using Shelter.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для DonationsPage.xaml
    /// </summary>
    public partial class DonationsPage : Page
    {
        public DonationsPage()
        {
            InitializeComponent();
            List<Donation> dons = MainWindow.HttpClient.GetFromJsonAsync<List<Donation>>("http://localhost:5010/Donation/GetList").Result;
            DonationsList.ItemsSource = dons;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
