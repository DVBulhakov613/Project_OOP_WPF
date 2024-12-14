using Project_OOP_WPF.UserControls;
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
        private List<Hospital> _hospitals = new List<Hospital>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewHospital_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new NewHospitalView();
        }

        private void ViewHospitals_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewHospitalsView();
        }

        private void Patients_AddPatientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new AddPatientView();
        }

        private void Patients_ViewPatients_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewPatientsView();
        }
    }
}