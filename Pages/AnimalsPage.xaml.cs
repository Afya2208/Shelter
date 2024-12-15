using Shelter.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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

namespace Shelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AnimalsPage.xaml
    /// </summary>
    public partial class AnimalsPage : Page
    {
     
        List<Animal> animals;
      DispatcherTimer timer = new DispatcherTimer();    
        int seconds = 0;
        public AnimalsPage()
        {
            InitializeComponent();
            List<TakingAnimal> takingAnimals = MainWindow.HttpClient.GetFromJsonAsync<List<TakingAnimal>>("http://localhost:5010/TakingAnimal/GetList").Result;

            animals = MainWindow.HttpClient.GetFromJsonAsync<List<Animal>>("http://localhost:5010/Animals/GetList").Result;
            foreach (var t in takingAnimals)
            {
                if (animals.Contains(t.Animal))
                {
                    animals.Remove(t.Animal);
                }
            }
            
            AnimalPanel1.Tag = 0;
            AnimalPanel2.Tag = 1;
            AnimalPanel3.Tag = 2;
            AnimalPanel1.DataContext = animals[(int)AnimalPanel1.Tag];
            AnimalPanel2.DataContext = animals[(int)AnimalPanel2.Tag];
            AnimalPanel3.DataContext = animals[(int)AnimalPanel3.Tag];
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += CheckClientAndNextCard;
            timer.Start();
        }

        private void CheckClientAndNextCard(object sender, EventArgs e)
        {
            if (MainWindow.AuthClient != null)
            {
                Menu.IsEnabled = true;
            }
            if (seconds != 10)
            {
                seconds++;
            }
            else
            {
                NextCard();
                seconds = 0;
                
            }

        }

        void NextCard()
        {
            if ((int)AnimalPanel1.Tag == animals.Count - 1)
            {
                AnimalPanel1.Tag = 0;
                AnimalPanel1.DataContext = animals[(int)AnimalPanel1.Tag];
            }
            else { AnimalPanel1.Tag = (int)AnimalPanel1.Tag + 1; AnimalPanel1.DataContext = animals[(int)AnimalPanel1.Tag]; }
            if ((int)AnimalPanel2.Tag == animals.Count - 1)
            {
                AnimalPanel2.Tag = 0;
                AnimalPanel2.DataContext = animals[(int)AnimalPanel2.Tag];
            }
            else { AnimalPanel2.Tag = (int)AnimalPanel2.Tag + 1; AnimalPanel2.DataContext = animals[(int)AnimalPanel2.Tag]; }
            if ((int)AnimalPanel3.Tag == animals.Count - 1)
            {
                AnimalPanel3.Tag = 0;

            }
            else { AnimalPanel3.Tag = (int)AnimalPanel3.Tag + 1; 
                AnimalPanel3.DataContext = animals[(int)AnimalPanel3.Tag]; }

            AnimalPanel3.DataContext = animals[(int)AnimalPanel3.Tag];

        }
        void PreviousCard()
        {
            if ((int)AnimalPanel1.Tag == 0)
            {
                AnimalPanel1.Tag = animals.Count-1;
                AnimalPanel1.DataContext = animals[(int)AnimalPanel1.Tag];
            }
            else AnimalPanel1.Tag = (int)AnimalPanel1.Tag - 1;
            if ((int)AnimalPanel2.Tag == 0)
            {
                AnimalPanel2.Tag = animals.Count - 1;
                AnimalPanel2.DataContext = animals[(int)AnimalPanel2.Tag];
            }
            else AnimalPanel2.Tag = (int)AnimalPanel2.Tag - 1;
            if ((int)AnimalPanel3.Tag == 0)
            {
                AnimalPanel3.Tag = animals.Count - 1;
            }
            else AnimalPanel3.Tag = (int)AnimalPanel3.Tag - 1;

            AnimalPanel1.DataContext = animals[(int)AnimalPanel1.Tag];
            AnimalPanel2.DataContext = animals[(int)AnimalPanel2.Tag];
            AnimalPanel3.DataContext = animals[(int)AnimalPanel3.Tag];
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void LeftBt_Click(object sender, RoutedEventArgs e)
        {
            PreviousCard();
        }

        private void RightBt_Click(object sender, RoutedEventArgs e)
        {
            NextCard();
        }

        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GoHomePage(animals[(int)AnimalPanel2.Tag]));
        }
        private void MenuItem2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DonationPage());
        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsVisible)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }
    }
}
