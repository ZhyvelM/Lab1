using Lab1.Business_Layer;
using Lab1.ServiceLayer;
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
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for ProductAddWindow.xaml
    /// </summary>
    public partial class ProductAddWindow : Window
    {
        public ProductAddWindow()
        {
            InitializeComponent();
        }

        private void AddProductOKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AddProductName.Text == "" || AddProductGramms.Text == "" || AddProductProtein.Text == "" || AddProductCarbs.Text == "" || AddProductCalories.Text == "" || AddProductFats.Text == "")
            {
                return;
            }
            MainWindow mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            if (mainWindow.ProductTree.SelectedItem != null)
            {
                Service service = (Service)mainWindow.service;
                Category category = service.GetCategory(((Category)mainWindow.ProductTree.SelectedItem).name);
                int weight;
                double protein, carbs, calories, fats;
                int.TryParse(AddProductGramms.Text, out weight);
                double.TryParse(AddProductProtein.Text, out protein);
                double.TryParse(AddProductCarbs.Text, out carbs);
                double.TryParse(AddProductCalories.Text, out calories);
                double.TryParse(AddProductFats.Text, out fats);
                service.AddProduct(category, AddProductName.Text, weight, protein, fats, carbs, calories);
                mainWindow.ProductUpdate(service.GetCategories());
                Close();
            }
        }

        private void AddProductCanselBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
