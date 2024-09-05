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
                    var itemIdElement = item.Element("ItemId");
                    var itemNameElement = item.Element("ItemName");
                    var categoryNameElement = item.Element("CategoryName");
                    var colorNameElement = item.Element("ColorName");
                    var qtyElement = item.Element("Qty");

                    if (itemIdElement != null && itemNameElement != null && categoryNameElement != null && colorNameElement != null && qtyElement != null)
                    {
                        Items lego = new Items(
                            int.Parse(itemIdElement.Value),
                            itemNameElement.Value,
                            categoryNameElement.Value,
                            colorNameElement.Value,
                            int.Parse(qtyElement.Value)
                        );
                        legoBricks.Add(lego);
                    }
                }
                dgItems.ItemsSource = null;
                dgItems.ItemsSource = legoBricks;
                dgItems.Items.Refresh();
            }
        }

    }
}