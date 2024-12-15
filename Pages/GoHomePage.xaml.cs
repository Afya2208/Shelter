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
using Aspose.Words;
using System.Net.Http.Json;

namespace Shelter.Pages
{
    /// <summary>
    /// Логика взаимодействия для GoHomePage.xaml
    /// </summary>
    public partial class GoHomePage : Page
    {
        Animal selectedAnimal;
 
        public GoHomePage(Animal selectedAnimal)
        {
            InitializeComponent();
            this.selectedAnimal = selectedAnimal;
        }

        private void PrintBt_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Trim() == "" || EmailBox.Text.Trim() == "" || PhoneBox.Text.Trim() == "" || Phone2Box.Text.Trim() == "" || IssuedByBox.Text.Trim() == "" ||
                DateOfIssueBox.Text.Trim() == "" || OtherInfoBox.Text.Trim() == "" || ResidentalBox.Text.Trim() == "" || RegistrationBox.Text.Trim() == "" || SerialBox.Text.Trim() == "" || NumberBox.Text.Trim() == "")
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка");
                MainWindow.Notifications.AppendLine($"{DateTime.Now} Некорректная попытка заполнить заявление на взятие животного");
                return;
            }
            uint serial, number;
            DateTime date;
            string fName, lName;
            if (!uint.TryParse(SerialBox.Text, out serial) || serial < 1000 || serial > 9999 || !uint.TryParse(NumberBox.Text, out number) || number < 100000 || number > 999999
                || !DateTime.TryParse(DateOfIssueBox.Text, out date) || date.Year > DateTime.Now.Year || date.Year == DateTime.Now.Year && date.Month > DateTime.Now.Month
                || date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month && date.Day > DateTime.Now.Day || (NameBox.Text.Split(' ')).Length < 2 || NameBox.Text.Split(' ').Length > 2)
            {
                MessageBox.Show("Все поля должны быть заполнены верными значениями", "Ошибка");
                MainWindow.Notifications.AppendLine($"{DateTime.Now} Некорректная попытка заполнить заявление на взятие животного");
                return;
            }
            fName = NameBox.Text.Split(' ')[0];
            lName = NameBox.Text.Split(' ')[1];
            PassportData passport = new PassportData()
            {
                Serial = serial.ToString(),
                Number = number.ToString(),
                DateOfIssue = date,
                RegistrationAddress = RegistrationBox.Text,
                IssuedBy = IssuedByBox.Text,
                Address = ResidentalBox.Text

            };
            TakingAnimalShort @short = new TakingAnimalShort()
            {
                DateOfTaking = DateTime.Now,
                ClientId = Properties.Settings.Default.AuthorizedClientId,
                AnimalId = selectedAnimal.AnimalId

            };
            try
            {
                MainWindow.HttpClient.PostAsync("http://localhost:5010/TakingAnimal/Add", JsonContent.Create(@short));
                MainWindow.HttpClient.PutAsync($"http://localhost:5010/Client/AddPassportData?id={@short.ClientId}", JsonContent.Create(passport));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Некорректные данные","Ошибка");
                MainWindow.Notifications.AppendLine($"{DateTime.Now} Некорректная попытка заполнить заявление на взятие животного");
                return;
            }

            


            MainWindow.Notifications.AppendLine($"{DateTime.Now} Успешное заполнение договора на взятие животного {selectedAnimal.Name}");
            Document doc = new Document(@"C:\Users\masha\Desktop\docs\договоры\ДОГОВОР О ПЕРЕДАЧЕ БЕЗДОМНОГО (НАЙДЕННОГО) ЖИВОТНОГО.docx");
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.MoveToBookmark("DateDay");
            builder.Write($"{DateTime.Today.Day}");
            builder.MoveToBookmark("DateMonth");
            builder.Write($"{DateTime.Today.ToString("MMMM")}");
            builder.MoveToBookmark("DateYearShort");
            builder.Write($"{DateTime.Today.Year%100}");
            builder.MoveToBookmark("FIO");
            builder.Write($"{fName} {lName}");
            builder.MoveToBookmark("RegisAddress");
            builder.Write($"{RegistrationBox.Text}");
            builder.MoveToBookmark("ResidentalAddress");
            builder.Write($"{ResidentalBox.Text}");
            builder.MoveToBookmark("Serial");
            builder.Write($"{SerialBox.Text}");
            builder.MoveToBookmark("Number");
            builder.Write($"{NumberBox.Text}");
            builder.MoveToBookmark("IssuedBy");
            builder.Write($"{IssuedByBox.Text}");
            builder.MoveToBookmark("DateOfIssue");
            builder.Write($"{DateOfIssueBox.Text}");
            builder.MoveToBookmark("Phone1");
            builder.Write($"{PhoneBox.Text}");
            builder.MoveToBookmark("Phone2");
            builder.Write($"{Phone2Box.Text}");
            builder.MoveToBookmark("Email");
            builder.Write($"{EmailBox.Text}");
            builder.MoveToBookmark("Other");
            builder.Write($"{OtherInfoBox.Text}");


            doc.Save($@"C:\Users\masha\Desktop\docs\договоры\{selectedAnimal.Name}_{fName}_{lName}_{DateTime.Today.ToShortDateString()}.pdf", SaveFormat.Pdf);
            NavigationService.GoBack();
            MainWindow.Notifications.AppendLine($"{DateTime.Now} Успешное сохранение договора в pdf по взятию животного {selectedAnimal.Name}");
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
