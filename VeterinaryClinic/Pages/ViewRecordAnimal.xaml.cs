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

namespace VeterinaryClinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewRecordAnimal.xaml
    /// </summary>
    public partial class ViewRecordAnimal : Page
    {
        public ViewRecordAnimal()
        {
            InitializeComponent();
            VisitList.ItemsSource = App.db.Visit.ToList();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.RecordClient(App.veti));
        }
    }
}
