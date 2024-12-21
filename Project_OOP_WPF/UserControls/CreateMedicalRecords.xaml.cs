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
        private Patient _patientReference;
        public CreateMedicalRecords(Patient patientReference)
        {
            InitializeComponent();
            _patientReference = patientReference;

            MedicalRecordDataGrid.ItemsSource = patientReference.MedicalHistory;
        }

        public CreateMedicalRecords(Patient patientReference, DateTime? date)
        {
            InitializeComponent();
            _patientReference = patientReference;
            _date = date;
            MedicalRecordDataGrid.ItemsSource = patientReference.MedicalHistory;
        }

        private void AddMedicalError_Date_Loaded(object sender, RoutedEventArgs e)
        {
            //AddMedicalRecord_Date.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(1), DateTime.MaxValue));
            AddMedicalRecord_Date.DisplayDateEnd = DateTime.Today;
        }

        private void AddMedicalError_Date_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show("! MEDICAL RECORD: Invalid date format. Please enter a valid date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (e.Text != null)
            {
                MessageBox.Show($"! MEDICAL RECORD: The date '{e.Text}' is not allowed. Please select a valid date.", "Date Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            e.ThrowException = false;
        }

        // tab methods: diagnoses
        private void AddDiagnoses_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DiagnosesInput.Text))
            {
                _diagnoses.Add(DiagnosesInput.Text.Trim());

                DiagnosesListBox.ItemsSource = null;
                DiagnosesListBox.ItemsSource = _diagnoses;

                DiagnosesInput.Clear();
                DiagnosesInput.Focus();
            }
            else MessageBox.Show("! MEDICAL RECORD: Cannot have an empty field.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

                TreatmentsListBox.ItemsSource = null;
                TreatmentsListBox.ItemsSource = _treatments;

                TreatmentsInput.Clear();
                TreatmentsInput.Focus();
            }
            else MessageBox.Show("! MEDICAL RECORD: Cannot have an empty field.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

                MedicationsListBox.ItemsSource = null;
                MedicationsListBox.ItemsSource = _medications;

                MedicationsInput.Clear();
                MedicationsInput.Focus();
            }
            else MessageBox.Show("! MEDICAL RECORD: Cannot have an empty field.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            DateTime date;
            if (AddMedicalRecord_Date.SelectedDate.HasValue)
            {
                date = AddMedicalRecord_Date.SelectedDate.Value;
            }
            else
                throw new ArgumentException("! PATIENT: Must always have a birthday.");

            try { _patientReference.AddMedicalRecord(_diagnoses, _treatments, _medications, date); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Errors encountered!", MessageBoxButton.OK); }
            MedicalRecordDataGrid.ItemsSource = null;
            MedicalRecordDataGrid.ItemsSource = _patientReference.MedicalHistory;

            ResetMedicalRecordCreation();

            MessageBox.Show("Medical Record Saved Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ResetMedicalRecordCreation()
        {
            _diagnoses.Clear();
            _treatments.Clear();
            _medications.Clear();
            DiagnosesListBox.ItemsSource = null;
            TreatmentsListBox.ItemsSource = null;
            MedicationsListBox.ItemsSource = null;
        }
    }
}
