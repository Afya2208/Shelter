using Shelter.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

namespace Shelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        List<Client> clients;
        List<Country> countries;
        public ClientsPage()
        {
            InitializeComponent();
            HttpResponseMessage message;
            try 
            {
                message = MainWindow.HttpClient.GetAsync("http://localhost:5010/Client/Clients").Result;
            } catch (Exception ex)
            {
                MessageBox.Show("ВКЛЮЧИ ВЕБ!!!");
                page.IsEnabled = false;
                return;
            }

            message = MainWindow.HttpClient.GetAsync("http://localhost:5010/Client/Clients").Result;
            if (!message.IsSuccessStatusCode)
            {
                MessageBox.Show("ошибка API","Ошибка");
                page.IsEnabled = false;
                
            } else
            {
                ReadClientList();
                ReadCountryList();
                ClientsList.ItemsSource = clients;
                FieldsCombo.ItemsSource = new List<string>()
                {
                    "Имя",
                    "Фамилия",
                    "Email",
                    "Логин"
                };
                CountryCombo.ItemsSource = countries;
               
            }

            
        }
        void ReadClientList()
        {
            clients = MainWindow.HttpClient.GetFromJsonAsync<List<Client>>("http://localhost:5010/Client/Clients").Result;
            ClientsList.ItemsSource = clients;
        }
        void ReadCountryList()
        {
            countries = MainWindow.HttpClient.GetFromJsonAsync<List<Country>>("http://localhost:5010/Countries/GetCountriesList").Result;
        }
        void Sort()
        {
            try
            {
                clients = MainWindow.HttpClient.GetFromJsonAsync<List<Client>>($"http://localhost:5010/Client/SortClientsByField?fieldName={FieldsCombo.SelectedItem as string}").Result;
                ClientsList.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                clients.Clear();
                ClientsList.ItemsSource = clients;
            }

        }
        void Filter()
        {
            try
            {
                clients = MainWindow.HttpClient.GetFromJsonAsync<List<Client>>($"http://localhost:5010/Client/FilterClientsByCountryId?cointryId={(CountryCombo.SelectedItem as Country).CountryId}").Result;
                ClientsList.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                clients.Clear();
                ClientsList.ItemsSource = clients;
            }
            
        }
        void Search(string searchText)
        {
            try
            {
                List<Client> clients = MainWindow.HttpClient.GetFromJsonAsync<List<Client>>($"http://localhost:5010/Client/FindClientsByText?searchText={searchText}").Result;
                ClientsList.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                clients.Clear();
                ClientsList.ItemsSource = clients;
            }

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                ReadClientList();
            } else
            {
                Search(SearchBox.Text);
            }
            

            
        }

        private void FieldsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void CountryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((sender as FrameworkElement)?.Tag);
            NavigationService.Navigate(new AddEditClientPage(id));
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditClientPage());
        }

        private void deleleBt_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((sender as FrameworkElement)?.Tag);
            MainWindow.HttpClient.DeleteAsync($"http://localhost:5010/Client/DeleteClientById?id={id}");
            ReadClientList();
        }
    }
}
