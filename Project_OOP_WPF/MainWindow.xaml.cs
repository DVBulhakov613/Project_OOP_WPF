using Project_OOP_WPF.UserControls;
using System.Collections.ObjectModel;
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

namespace Project_OOP_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Hospital _hospitalReference;

        public Hospital _selectedHospital 
        {
            get => _hospitalReference; 
            set 
            {
                _hospitalReference = value;
                CurrentHospital.Text = $"Current Hospital: {value.ID} | {value.Name}";
            }
        }
        public ObservableCollection<Hospital> Hospitals = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void HospitalWindow_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CreateHospital(this);
        }

        private void Patients_AddPatientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CreatePatient(this, _selectedHospital);
        }

        private void Patients_ViewPatients_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = new ViewPatientsView();
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CreateAppointments();
        }
    }
}