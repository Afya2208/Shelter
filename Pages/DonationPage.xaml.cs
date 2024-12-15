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

namespace Shelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для DonationPage.xaml
    /// </summary>
    public partial class DonationPage : Page
    {
        public DonationPage()
        {
            InitializeComponent();
            List<Food> foods = MainWindow.HttpClient.GetFromJsonAsync<List<Food>>("http://localhost:5010/Food/GetList").Result;

            FoodCombo.ItemsSource = foods;
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        bool CheckCardNumver(string num)
        {
            foreach (char c in num)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        private void PayBt_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(AmountBox.Text, out var amount) || amount <= 0 ||FoodCombo.SelectedItem == null || !uint.TryParse(CVCCardBox.Text, out var cvc) ||
                cvc < 100 || cvc > 999 || !CheckCardNumver(NumberCardBox.Text) || !uint.TryParse(MonthCardBox.Text, out var month) || month < 1 || month > 12
                || !uint.TryParse(YearCardBox.Text, out var year) || year < DateTime.Now.Year || year == DateTime.Now.Year && month < DateTime.Now.Month)
            {
                MessageBox.Show("Вы ввели неправильные данные","Ошибка");
                MainWindow.Notifications.AppendLine($"{DateTime.Now} Неудачная попытка пожертвования");
                return;
            }

            DonationShort @short = new DonationShort()
            {
                Amount = int.Parse(AmountBox.Text),
                ClientId = Properties.Settings.Default.AuthorizedClientId,
                FoodId = (FoodCombo.SelectedItem as Food).FoodId,
                DateOfDonation = DateTime.Now
               
            };
            try
            {
                MainWindow.HttpClient.PostAsync("http://localhost:5010/Donation/Add", JsonContent.Create(@short));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы ввели неправильные данные", "Ошибка");
                MainWindow.Notifications.AppendLine($"{DateTime.Now} Неудачная попытка пожертвования");
                return;
            }
           
            NavigationService.Navigate(new ThankPage($"{MainWindow.AuthClient.FirstName} {MainWindow.AuthClient.LastName}", (amount * (FoodCombo.SelectedItem as Food).Price).ToString()));
            MainWindow.Notifications.AppendLine($"{DateTime.Now} Успешное пожертвование");
        }

        private void AmountBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(AmountBox.Text, out var amount))
            {
                TotalBlock.Text = $"Итого: {(amount * (FoodCombo.SelectedItem as Food).Price).ToString()}";
            }
        }


    }
}
