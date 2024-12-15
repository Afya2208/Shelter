using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AllAuthsPage.xaml
    /// </summary>
    public partial class AllAuthsPage : Page
    {
        public AllAuthsPage()
        {
            InitializeComponent();
            
            List<string> strings = new List<string>();
            foreach (FileInfo file in new DirectoryInfo("C:\\Users\\masha\\Desktop\\docs\\").GetFiles())
            {
                if (file.Exists)
                {
                    string[] nots = File.ReadAllLines(file.FullName);
                    foreach (string s in nots)
                    {
                        if (s.Contains("Успешный вход"))
                        {
                            strings.Add(s);
                        }
                    }
                }
            }
            AuthList.ItemsSource = strings;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
