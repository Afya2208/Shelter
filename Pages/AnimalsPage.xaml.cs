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
    /// Логика взаимодействия для AnimalsPage.xaml
    /// </summary>
    public partial class AnimalsPage : Page
    {
        bool IsCtrlPressed;
        List<Animal> animals;
        int middlePos = 1;
        public AnimalsPage()
        {
            InitializeComponent();
            animals = DB.entities.Animal.ToList();
            if ()
            {

            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl) 
            {
                IsCtrlPressed = true;
            }
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                IsCtrlPressed = false;
            }
            if (IsCtrlPressed && e.Key == Key.Q)
            {
                NavigationService.Navigate(new DonationsPage());
            }
        }
    }
}
