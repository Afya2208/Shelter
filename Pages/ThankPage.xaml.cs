using Microsoft.Win32;
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
    /// Логика взаимодействия для ThankPage.xaml
    /// </summary>
    public partial class ThankPage : Page
    {
        public ThankPage(string client, string sum)
        {
            InitializeComponent();
            SumBlock.Text = sum;
            NameBlock.Text = client;
            SaveControlImage(Sert, 650, 450, $"C:\\Users\\masha\\Desktop\\docs\\сертификаты\\{NameBlock.Text}_{SumBlock.Text.Replace(",", ".")}" + ".png");
            MainWindow.Notifications.AppendLine($"{DateTime.Now} Успешное сохранение сертификата {client} на {sum} рублей");
        }

        private void SaveControlImage(
    Visual baseElement, int imageWidth, int imageHeight, string pathToOutputFile)
        {
            // 1) get current dpi
            var pSource = PresentationSource.FromVisual(Application.Current.MainWindow);
            Matrix m = pSource.CompositionTarget.TransformToDevice;
            double dpiX = m.M11 * 96;
            double dpiY = m.M22 * 96;

            // 2) create RenderTargetBitmap
            var elementBitmap = new RenderTargetBitmap(imageWidth, imageHeight, dpiX, dpiY, PixelFormats.Default);

            // 3) undo element transformation
            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                var visualBrush = new VisualBrush(baseElement);
                drawingContext.DrawRectangle(
                    visualBrush,
                    null,
                    new Rect(new Point(0, 0), new Size(imageWidth / m.M11, imageHeight / m.M22)));
            }

            // 4) draw element
            elementBitmap.Render(drawingVisual);

            // 5) create PNG image
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(elementBitmap));

            // 6) save image to file
            using (var imageFile = new FileStream(pathToOutputFile, FileMode.Create, FileAccess.Write))
            {
                encoder.Save(imageFile);
                imageFile.Flush();
                imageFile.Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //if (saveFileDialog.ShowDialog() == true)
            //{
                
            //    SaveControlImage(Sert, 650, 450, $"{new FileInfo(saveFileDialog.FileName).Directory.FullName}{DateTime.Now.ToString("dd.MM.yyyy.HH.mm.ss")}_{NameBlock.Text}_{SumBlock.Text.Replace(",", ".")}" + ".png");
            //}

            
        }
    }
}
