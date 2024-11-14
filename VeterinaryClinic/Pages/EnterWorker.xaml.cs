using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
using VeterinaryClinic.Database;

namespace VeterinaryClinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для EnterWorker.xaml
    /// </summary>
    public partial class EnterWorker : Page
    {
        public static List<Vet> vet { get; set; }
        public EnterWorker()
        {
            InitializeComponent();
        }

        private void Button_Click_Enter(object sender, RoutedEventArgs e)
        {
            string name = LastNameTB.Text.Trim();
            string firstName = FirstNameTB.Text.Trim();

            vet = new List<Vet>(App.db.Vet.ToList());
            Vet currentVet = vet.FirstOrDefault(x => x.Vet_FirstName == name && x.Vet_LastName == firstName);
            if (currentVet != null)
            {
                LastNameTB.Text = "";
                FirstNameTB.Text = "";
                App.veti = currentVet;
                NavigationService.Navigate(new Pages.RecordClient(currentVet));
            }
            else
            {
                MessageBox.Show("Мы не нашли ваши данные в системе, попробуйте зайти снова");
                LastNameTB.Text = "";
                FirstNameTB.Text = "";
            }
        }
    }
}
