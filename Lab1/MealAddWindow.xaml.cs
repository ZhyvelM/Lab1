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
    /// Interaction logic for MealAddWindow.xaml
    /// </summary>
    public partial class MealAddWindow : Window
    {
        public MealAddWindow()
        {
            InitializeComponent();
        }
        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            Service service = (Service)mainWindow.service;
            service.AddMeal(MealName.Text);
            mainWindow.MealUpdate();
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
