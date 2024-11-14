using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
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
using static System.Net.Mime.MediaTypeNames;

namespace VeterinaryClinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecordClient.xaml
    /// </summary>
    public partial class RecordClient : Page
    {
        public Vet veti;
        public Visit visit;
        public RecordClient(Vet vet)
        {
            InitializeComponent();
            veti = vet;
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                var owner = App.db.Owner;
                var peopleList = owner
                   .Select(p => new
                   {
                       FullName = p.Owner_FirstName + " " + p.Owner_LastName
                   })
                   .ToList();

                // Привязываем список к ComboBox через ItemSource
                OwnerCb.ItemsSource = peopleList.Select(p => p.FullName).ToList();
                var context = App.db.Animal;
                // Создаем список объединенных строк (FirstName, LastName, Patronymic)
                var animalList = context
                    .Select(p => new
                    {
                        FullName = p.Name
                    })
                    .ToList();

                // Привязываем список к ComboBox через ItemSource
                AnimalNameCB.ItemsSource = animalList.Select(p => p.FullName).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении данных: " + ex.Message);
            }
        }

        private void myTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //
        }
        private void RemoveText(object sender, RoutedEventArgs e)
        {
            if (TreatmentTB.Text == "Способ лечения")
            {
                TreatmentTB.Text = "";
            }
            if (DiaqnosisTB.Text == "Диагноз")
            {
                DiaqnosisTB.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TreatmentTB.Text))
            {
                TreatmentTB.Text = "Способ лечения";
            }
            if (string.IsNullOrWhiteSpace(DiaqnosisTB.Text))
            {
                DiaqnosisTB.Text = "Диагноз";
            }
        }
        private void Button_Click_Registration(object sender, RoutedEventArgs e)
        {
            try
            {
                // Устанавливаем данные клиента и услуги
                //visit.Visit_Date = ; // Индекс клиента
                //visit.Animal_ID = ;
                //visit.Vet_ID = ;
                //visit.Diagnosis = ;
                //visit.Treatment = ;

                App.db.Visit.Add(visit);
                App.db.SaveChanges();
                MessageBox.Show("Запись прошла успешно!");

                // Переход на другую страницу после успешной записи
                NavigationService.Navigate(new Pages.EnterWorker());
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка формата: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void AnimalNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int owner = AnimalNameCB.SelectedIndex;
            MessageBox.Show($"{owner}");
            if (owner == 0)
            {
                //owner += 1;
                OwnerCb.SelectedIndex = 0;
            }
            else
            {
                OwnerCb.SelectedIndex = Convert.ToInt32(App.db.Animal.FirstOrDefault(x => x.Animal_ID == (owner)).Owner_ID.ToString());

            }
        }

        private void Button_Click_ViewRecord(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ViewRecordAnimal());
        }
    }
}
