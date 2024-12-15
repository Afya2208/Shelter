using Aspose.Words.Bibliography;
using Shelter.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
    /// Логика взаимодействия для AddEditClientPage.xaml
    /// </summary>
    public partial class AddEditClientPage : Page
    {
        Client client;
        
        List<Country> countries;
        public AddEditClientPage()
        {
            InitializeComponent();
            if (client == null) 
            client = new Client();
            DataContext = client;
            ReadCountryList();
            CountryCombo.ItemsSource = countries;
        }
        public AddEditClientPage(int id) : this()
        {
            InitializeComponent();
            try
            {
                client = MainWindow.HttpClient.GetFromJsonAsync<Client>($"http://localhost:5010/Client/FindClientById?id={id}").Result;
                DataContext = client;
            } catch 
            {
                client = null;
            }
            DataContext = client;
        }

        void ReadCountryList()
        {
            countries = MainWindow.HttpClient.GetFromJsonAsync<List<Country>>("http://localhost:5010/Countries/GetCountriesList").Result;
        }
        private void CountryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            if (firstBox.Text == "" || lastBox.Text == "" || PhoneBox.Text == "" ||AddressBox.Text == "" || PostalBox.Text == "" || RegionBox.Text == ""
                || EmailBox.Text == "" || CountryCombo.SelectedItem == null || LoginBox.Text == "" && Password.Text != "" || LoginBox.Text != "" && Password.Text == "")
            {
                MessageBox.Show("Вы ввели некорректные данные","Ошибка");
                return;
            }
            ClientShort clientShort = new ClientShort()
            {
                Email = EmailBox.Text,
                FirstName = firstBox.Text, LastName = lastBox.Text,
                Address = AddressBox.Text,
                Region = RegionBox.Text,
                CountryId = (CountryCombo.SelectedItem as Country).CountryId,
                Phone = PhoneBox.Text,
                Login = LoginBox.Text,
                Password = Password.Text,
                PostalZip = PostalBox.Text,
            };
            
            int ID = client.ClientId;
            if (ID == 0)
            {
                // добавление - пост
                JsonContent json = JsonContent.Create(clientShort);
                 // переводим ClientShort в json форму
                MainWindow.HttpClient.PostAsync($"http://localhost:5010/Client/CreateClient", json);
            }
            else {
                // редактирование - пут
                JsonContent json = JsonContent.Create(clientShort);
                MainWindow.HttpClient.PutAsync($"http://localhost:5010/Client/ChangeClient?id={ID}", json);
                
            }

            
            NavigationService.GoBack();
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
