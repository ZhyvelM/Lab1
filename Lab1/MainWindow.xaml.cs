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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly IService service = new Service();
        Point startPoint;
        private bool initialized = false;

        public MainWindow()
        {
            InitializeComponent();
            initialized = true;
            ProductUpdate(service.GetCategories());
            MealUpdate();
            userInit(service.GetUser());
        }

        private void userInit(User user)
        {
            NameBox.Text = user.name;
            AgeBox.Text = user.age.ToString();
            HighBox.Text = user.height.ToString();
            WeightBox.Text = user.weight.ToString();
            RadioInit(user.activity);
        }

        private void RadioInit(Activity activity)
        {
            if (activity == Activity.low)
            {
                RadioLow.IsChecked = true;
            }
            else
            if (activity == Activity.normal)
            {
                RadioNormal.IsChecked = true;
            }
            else
            if (activity == Activity.average)
            {
                RadioAverage.IsChecked = true;
            }
            else RadioHigh.IsChecked = true;
        }

        public void ProductUpdate(IList<Category> categories)
        {
            ProductTree.ItemsSource = null;
            ProductTree.ItemsSource = categories;
        }

        public void MealUpdate()
        {
            MealTree.ItemsSource = null;
            MealTree.ItemsSource = service.GetMeals();
        }
        
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text != "")
            {
                ProductUpdate(service.Search(SearchBox.Text));
                foreach (var item in ProductTree.Items)
                {
                    DependencyObject dObj = ProductTree.ItemContainerGenerator.ContainerFromItem(item);
                    if (dObj != null)
                        ((TreeViewItem)dObj).IsExpanded = true;
                }
            }
            else
            {
                ProductUpdate(service.GetCategories());
            }
        }

        private void Check()
        {
            ErrorBox.Visibility = Visibility.Hidden;
            string name = "";
            int age = 0, weight = 0, high = 0;
            Activity activity = checkRadio();
            if (initialized)
            {
                if (NameBox.Text == "")
                {
                    ErrorOut();
                }
                else
                {
                    name = NameBox.Text;
                }
                if (AgeBox.Text == "" || AgeBox.Text.Length > 3 || !AgeBox.Text.All(char.IsDigit) || int.Parse(AgeBox.Text) == 0)
                {
                    ErrorOut();
                }
                else
                {
                    age = int.Parse(AgeBox.Text);
                }
                if (HighBox.Text == "" || HighBox.Text.Length > 3 || !HighBox.Text.All(char.IsDigit) || int.Parse(HighBox.Text) == 0)
                {
                    ErrorOut();
                }
                else
                {
                    high = int.Parse(HighBox.Text);
                }
                if (WeightBox.Text == "" || WeightBox.Text.Length > 3 || !WeightBox.Text.All(char.IsDigit) || int.Parse(WeightBox.Text) == 0)
                {
                    ErrorOut();
                }
                else
                {
                    weight = int.Parse(WeightBox.Text);
                }
            }
            if (ErrorBox.Visibility == Visibility.Hidden)
            {
                DailyNormBox.Text = service.GetDCR(name, age, high, weight, activity).ToString("0.###");
                ProgressBarChange();
            }
        }

        private Activity checkRadio()
        {
            if (RadioLow.IsChecked == true)
            {
                return Activity.low;
            }else
            if (RadioNormal.IsChecked == true)
            {
                return Activity.normal;
            }else
            if (RadioAverage.IsChecked == true)
            {
                return Activity.average;
            }else return Activity.high;
        }

        private void ErrorOut()
        {
            ErrorBox.Visibility = Visibility.Visible;
        }

        private void ProductTree_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }
        
        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            var item = ProductTree.SelectedItem;
            if (item != null && item is Product)
            {
                Product product = item as Product;
                service.RemoveProduct(product);
                ProductUpdate(service.GetCategories());
            }
        }

        private void RemoveProductMeal_Click(object sender, RoutedEventArgs e)
        {
            var item = MealTree.SelectedItem;
            if (item != null && item is Product)
            {
                Product product = item as Product;
                service.RemoveProduct(product.meal, product);
                ProgressBarChange();
                MealUpdate();
            }
        }

        private void RemoveCategory_Click(object sender, RoutedEventArgs e)
        {
            var item = ProductTree.SelectedItem;
            if (item != null && item is Category)
            {
                Category category = item as Category;
                service.RemoveCategory(category);
                ProductUpdate(service.GetCategories());
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductAddWindow PAW = new ProductAddWindow();
            PAW.Owner = this;
            PAW.Show();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoryAddWindow CAW = new CategoryAddWindow();
            CAW.Owner = this;
            CAW.Show();
        }

        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);
            return source as TreeViewItem;
        }

        private void ProductTree_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
            (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
            Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                if (ProductTree.SelectedItem != null && ProductTree.SelectedItem.GetType() == typeof(Product))
                {
                    Product product = (Product)ProductTree.SelectedItem;
                    DataObject dragData = new DataObject("Product", product);
                    DragDrop.DoDragDrop(ProductTree, dragData, DragDropEffects.Move);
                }
            }
        }

        private void MealTree_Drop(object sender, DragEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }

            if (MealTree.SelectedItem != null && MealTree.SelectedItem.GetType() == typeof(Meal))
            {
                if (e.Data.GetDataPresent("Product"))
                {
                    Product prod = e.Data.GetData("Product") as Product;
                    Product product = new Product(prod.Category, prod.Name, prod.Gramms, prod.Protein, prod.Fats, prod.Carbs, prod.Calories);
                    Meal meal = (Meal)MealTree.SelectedItem;
                    product.meal = meal;
                    service.AddProduct(meal, product);
                    ProgressBarChange();
                }
                MealUpdate();
            }
        }

        private void ProductTree_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void MealTree_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("Product") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void MealTree_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private void AddMeal_Click(object sender, RoutedEventArgs e)
        {
            MealAddWindow MAW = new MealAddWindow();
            MAW.Owner = this;
            MAW.Show();
        }

        private void RemoveMeal_Click(object sender, RoutedEventArgs e)
        {
            var item = MealTree.SelectedItem;
            if (item != null && item is Meal)
            {
                Meal meal = item as Meal;
                service.RemoveMeal(meal);
                MealUpdate();
            }
        }

        private void MealTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = MealTree.SelectedItem;
            if (item is Product)
            {
                Product product = item as Product;
                FullFill(product);
                ProductGramms.Visibility = Visibility.Visible;
            }
        }

        private void FullFill(Product product)
        {
            ProductCategory.Text = product.Category.name;
            ProductName.Text = product.Name;
            ProductCarbs.Text = product.Carbs.ToString();
            ProductFats.Text = product.Fats.ToString();
            ProductProtein.Text = product.Protein.ToString();
            ProductGramms.Text = product.Gramms.ToString();
            ProductCalories.Text = product.Calories.ToString();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var item = MealTree.SelectedItem;
            if(item is Product)
            {
                Product product = item as Product;
                FullFill(service.productCalc(service.GetCategory(product.Category.name).Products[product.Name], Convert.ToUInt16(e.NewValue)));
                product.Gramms = int.Parse(ProductGramms.Text);
                product.Protein = double.Parse(ProductProtein.Text);
                product.Fats = double.Parse(ProductFats.Text);
                product.Carbs = double.Parse(ProductCarbs.Text);
                product.Calories = double.Parse(ProductCalories.Text);
                ProgressBarChange();
            }
        }
               
        private void ProgressBarChange()
        {
            if (DailyNormBox.Text != "")
            {
                Progress.Value = service.GetCalories() / double.Parse(DailyNormBox.Text)*100;
                if (Progress.Value == 101)
                {
                    Progress.Foreground = Brushes.Red;
                }
                else
                {
                    Progress.Foreground = Brushes.Green;
                }
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (DailyNormBox.Text!="" && Progress.Value != 0)
            {
                service.Export(DailyNormBox.Text, service.GetCalories().ToString());
            }
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Check();
        }

        private void AgeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Check();
        }

        private void HighBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Check();
        }

        private void WeightBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Check();
        }

        private void RadioLow_Checked(object sender, RoutedEventArgs e)
        {
            Check();
        }

        private void RadioNormal_Checked(object sender, RoutedEventArgs e)
        {
            Check();
        }

        private void RadioAverage_Checked(object sender, RoutedEventArgs e)
        {
            Check();
        }

        private void RadioHigh_Checked(object sender, RoutedEventArgs e)
        {
            Check();
        }
    }
}