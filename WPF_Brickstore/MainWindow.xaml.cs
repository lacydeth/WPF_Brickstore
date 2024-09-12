using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.IO;
using System.Collections.ObjectModel;

namespace WPF_Brickstore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Items> legoBricks = new ObservableCollection<Items>();
        ObservableCollection<Items> selectedLegoBricks = new ObservableCollection<Items>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "BSX fájl(.bsx)|*.bsx",
                Multiselect = false,
                Title = "Importálás"
            };

            if (ofd.ShowDialog() == true)
            {
                XDocument xaml = XDocument.Load(ofd.FileName);

                legoBricks.Clear();
                foreach (var item in xaml.Descendants("Item"))
                {
                    var itemIdElement = item.Element("ItemID");
                    var itemNameElement = item.Element("ItemName");
                    var categoryNameElement = item.Element("CategoryName");
                    var colorNameElement = item.Element("ColorName");
                    var qtyElement = item.Element("Qty");
                    Items lego = new Items(
                        itemIdElement.Value,
                        itemNameElement.Value,
                        categoryNameElement.Value,
                        colorNameElement.Value,
                        int.Parse(qtyElement.Value)
                    );
                    if (!cbCategories.Items.Contains(lego.CategoryName))
                    {
                        cbCategories.Items.Add(lego.CategoryName);
                    }
                    legoBricks.Add(lego);
                }
            }
            dgItems.ItemsSource = legoBricks;
            dgItems.Items.Refresh();
        }

        private void FilterSearch()
        {
            string searchName = tbSearchName.Text;
            string searchId = tbSearchId.Text;
            string selectedCategory = (string)cbCategories.SelectedItem; 

            selectedLegoBricks.Clear();

            foreach (var item in legoBricks)
            {
                bool matchName = string.IsNullOrEmpty(searchName) || item.ItemName.StartsWith(searchName, StringComparison.OrdinalIgnoreCase);
                bool matchId = string.IsNullOrEmpty(searchId) || item.ItemId.StartsWith(searchId, StringComparison.OrdinalIgnoreCase);
                bool matchCategory = string.IsNullOrEmpty(selectedCategory) || item.CategoryName == selectedCategory;

                if (matchName && matchId && matchCategory)
                {
                    selectedLegoBricks.Add(item);
                }
            }

            var matchingCategories = selectedLegoBricks.Select(item => item.CategoryName).Distinct().ToList();

            cbCategories.SelectionChanged -= cbCategories_SelectionChanged;
            cbCategories.Items.Clear();
            cbCategories.Items.Add("");

            foreach (var category in matchingCategories)
            {
                cbCategories.Items.Add(category);
            }

            if (matchingCategories.Contains(selectedCategory))
            {
                cbCategories.SelectedItem = selectedCategory;
            }
            else
            {
                cbCategories.SelectedItem = ""; 
            }

            cbCategories.SelectionChanged += cbCategories_SelectionChanged; 

            dgItems.ItemsSource = selectedLegoBricks;
            dgItems.Items.Refresh();
        }


        private void tbSearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterSearch();
        }

        private void tbSearchId_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterSearch();
        }

        private void cbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSearch();
        }

        private void cbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
