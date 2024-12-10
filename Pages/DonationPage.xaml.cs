using Shelter.Database;
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
    /// Логика взаимодействия для DonationPage.xaml
    /// </summary>
    public partial class DonationPage : Page
    {
        public DonationPage()
        {
            InitializeComponent();
            FoodCombo.ItemsSource = DB.entities.Food.ToList();
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PayBt_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(AmountBox.Text, out var amount) || amount < 1 || FoodCombo.SelectedItem == null)
            {
                MessageBox.Show("Вы ввели неправильные данные","Ошибка");
                return;
            }
            Donation donation = new Donation()
            {
                Amount = int.Parse(AmountBox.Text),
                ClientId = MainWindow.AuthClient.ClientId,
                FoodId = (FoodCombo.SelectedItem as Food).FoodId,
                DateOfDonation = DateTime.Now
               
            };
            DB.entities.Donation.Add(donation);
            DB.entities.SaveChanges();
            NavigationService.GoBack();
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
