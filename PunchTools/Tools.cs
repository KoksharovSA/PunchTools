using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PunchTools
{
    public class Tools
    {
        public Tools()
        {
        }

        public Tools(int nomenclatureNumberTools, string nameTools, string dirFotoTools, string typeTools, string sizesTools, int quantity, int minQuantity, double mainSize, double minMainSize)
        {
            NomenclatureNumberTools = nomenclatureNumberTools;
            NameTools = nameTools ?? throw new ArgumentNullException(nameof(nameTools));
            TypeTools = typeTools ?? throw new ArgumentNullException(nameof(typeTools));
            SizesTools = sizesTools ?? throw new ArgumentNullException(nameof(sizesTools));
            Quantity = quantity;
            MinQuantity = minQuantity;
            MainSize = mainSize;
            MinMainSize = minMainSize;
        }

        public int NomenclatureNumberTools { get; set; }
        public string NameTools { get; set; }
        public string TypeTools { get; set; }
        public string SizesTools { get; set; }
        public int Quantity { get; set; }
        public int MinQuantity { get; set; }
        public double MainSize { get; set; }
        public double MinMainSize { get; set; }

        public Grid CreateFormTools()
        {
            Grid grid = new Grid();
            grid.Margin = new Thickness(5, 5, 5, 5);
            // grid.Height = 208;
            //grid.Width = 208;

            Rectangle rectangle = new Rectangle();
            //rectangle.Height = 204;
            //rectangle.Width = 204;
            rectangle.StrokeThickness = 2;
            rectangle.Stroke = Brushes.Green;
            rectangle.RadiusX = 5;
            rectangle.RadiusY = 5;

            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Vertical;
            panel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            panel.Width = 200;

            panel.Margin = new Thickness(5,5,5,5); 

            

            Image image = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = File.Exists(Directory.GetCurrentDirectory() + "\\IMG\\" + NomenclatureNumberTools + ".jpg") ? new Uri(Directory.GetCurrentDirectory() + "\\IMG\\" + NomenclatureNumberTools + ".jpg"): new Uri(Directory.GetCurrentDirectory() + "\\IMG\\no.jpg");
            bitmap.EndInit();
            image.Source = bitmap;
            image.Height = 150;

            panel.Children.Add(new TextBlock() { Text = NomenclatureNumberTools.ToString() }) ;
            panel.Children.Add(image);
            panel.Children.Add(new TextBlock() { Text = "Название инструмента: " + NameTools, TextWrapping = TextWrapping.Wrap });
            panel.Children.Add(new TextBlock() { Text = "Тип: " + TypeTools });
            panel.Children.Add(new TextBlock() { Text = "Размеры: " + SizesTools });
            panel.Children.Add(new TextBlock() { Text = "Количество: " + Quantity });
            panel.Children.Add(new TextBlock() { Text = "Осталось переточек: " + Math.Round(((MainSize - MinMainSize) / 0.2),0).ToString() });

            rectangle.Stroke = (Quantity - MinQuantity) <= 0 ? Brushes.Red : Brushes.Green;
            rectangle.Stroke = ((MainSize - MinMainSize)/0.2) <= 3 ? Brushes.Red : Brushes.Green;

            grid.Children.Add(rectangle);
            grid.Children.Add(panel);


            return grid;
        }

        public override string ToString()
        {
            return NameTools;
        }
    }
}
