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

namespace Project_OOP_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for CreateMedicalRecords.xaml
    /// </summary>
    public partial class CreateMedicalRecords : UserControl
    {
        private List<string> _diagnoses = new();
        private List<string> _treatments = new();
        private List<string> _medications = new();
        private DateTime? _date = null;
        private Patient _PatientReference;
        public CreateMedicalRecords(Patient patientReference)
        {
            InitializeComponent();
            _PatientReference = patientReference;

            MedicalRecordDataGrid.ItemsSource = patientReference.MedicalHistory;
        }

        public CreateMedicalRecords(Patient patientReference, DateTime? date)
        {
            InitializeComponent();
            _PatientReference = patientReference;
            _date = date;
            MedicalRecordDataGrid.ItemsSource = patientReference.MedicalHistory;
        }

        // tab methods: diagnoses
        private void AddDiagnoses_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DiagnosesInput.Text))
            {
                _diagnoses.Add(DiagnosesInput.Text.Trim());
                DiagnosesListBox.Items.Refresh();
                DiagnosesInput.Clear();
            }
        }

        private void RemoveDiagnoses_Click(object sender, RoutedEventArgs e)
        {
            if (DiagnosesListBox.SelectedItem != null)
            {
                _diagnoses.Remove(DiagnosesListBox.SelectedItem.ToString());
                DiagnosesListBox.Items.Refresh();
            }
        }
        
        // tab methods: treatments
        private void AddTreatment_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TreatmentsInput.Text))
            {
                _treatments.Add(TreatmentsInput.Text.Trim());
                TreatmentsListBox.Items.Refresh();
                TreatmentsInput.Clear();
            }
        }

        private void RemoveTreatment_Click(object sender, RoutedEventArgs e)
        {
            if (TreatmentsListBox.SelectedItem != null)
            {
                _treatments.Remove(TreatmentsListBox.SelectedItem.ToString());
                TreatmentsListBox.Items.Refresh();
            }
        }

        // Notes Tab Methods
        private void AddMedication_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MedicationsInput.Text))
            {
                _medications.Add(MedicationsInput.Text.Trim());
                MedicationsListBox.Items.Refresh();
                MedicationsInput.Clear();
            }
        }

        private void RemoveMedication_Click(object sender, RoutedEventArgs e)
        {
            if (MedicationsListBox.SelectedItem != null)
            {
                _medications.Remove(MedicationsListBox.SelectedItem.ToString());
                MedicationsListBox.Items.Refresh();
            }
        }

        // Save the MedicalRecord
        private void SaveMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            _PatientReference.AddMedicalRecord(_diagnoses, _treatments, _medications);

            MessageBox.Show("Medical Record Saved Successfully!");
        }
    }
}
