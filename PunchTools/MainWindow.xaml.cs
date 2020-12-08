using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
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

namespace PunchTools
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Collection<Tools> toolsBase;
        public MainWindow()
        {
            InitializeComponent();
            toolsBase = DB.ReadDBTools(new SQLiteConnection("Data source=C:\\Users\\User\\Desktop\\PunchTools.db;Version=3"));
            TreeViewAddComponent(toolsBase);
            FillInWrapPanel(toolsBase);           
        }

        public void FillInWrapPanel(IEnumerable<Tools> tools)
        {
            ToolsWrapPanel.Children.Clear();
            foreach (var item in tools)
            {
                ToolsWrapPanel.Children.Add(item.CreateFormTools());
            }            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void TreeViewAddComponent(IEnumerable<Tools> tools)
        {
            TreeViewTools.Items.Clear();
            TreeViewItem treeViewItem = new TreeViewItem();
            treeViewItem.Header = "Все";
            treeViewItem.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e) =>
            {
                FillInWrapPanel(tools);
            };
            TreeViewTools.Items.Add(treeViewItem);
            foreach (var item in tools.Select(x=>x.TypeTools).Distinct())
            {
                TreeViewItem treeViewItem1 = new TreeViewItem();
                treeViewItem1.Header = item;
                treeViewItem1.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e) =>
                {
                    FillInWrapPanel(tools.Where(x=>x.TypeTools.Contains(item)));
                };
                TreeViewTools.Items.Add(treeViewItem1);
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (TextBoxSearch.Text != "" && TextBoxSearch.Text != " ")
            {
                TreeViewAddComponent(toolsBase.Where(x=>x.NameTools.Contains(TextBoxSearch.Text) || x.NomenclatureNumberTools.Contains(TextBoxSearch.Text) || x.Note.Contains(TextBoxSearch.Text)));
                FillInWrapPanel(toolsBase.Where(x => x.NameTools.Contains(TextBoxSearch.Text) || x.NomenclatureNumberTools.Contains(TextBoxSearch.Text) || x.Note.Contains(TextBoxSearch.Text)));
            }
            else
            {
                TreeViewAddComponent(toolsBase);
                FillInWrapPanel(toolsBase);
            }
        }
    }
}
