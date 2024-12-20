using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CreateAppointments.xaml
    /// </summary>
    public partial class CreateAppointments : UserControl
    {
        private Hospital _hospitalReference;
        private Patient selectedPatient;
        private List<Staff> selectedStaff = new List<Staff>();
        private AppointmentPurpose _selectedPurpose;
        private Action<DataGrid, Patient> refreshAppointments = (DataGrid, Patient) =>
        {
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = Patient.Schedule.Appointments;
            DataGrid.Items.Refresh();
        };
        private Func<bool> validateAllInputs;
        private Func<bool> validatePatient;
        private Func<bool> validateStaff;
        private Func<bool> validateDates;
        private Func<bool> validateTime;
        private Func<bool> validateRoom;
        // me and wpf are in a love-hate relationship
        public CreateAppointments(Hospital hospitalReference)
        {
            InitializeComponent();
            _hospitalReference = hospitalReference;

            PatientDataGrid.ItemsSource = hospitalReference.Patients;
            StaffDataGrid.ItemsSource = hospitalReference.ActiveStaff;

            #region Delegates
            validatePatient = () => selectedPatient != null;
            validateStaff = () => selectedStaff.Count > 0;
            validateDates = () => StartDate.SelectedDate.HasValue && EndDate.SelectedDate.HasValue;
            validateTime = () =>
            {
                return int.TryParse(StartHour.Text, out int startHour) && startHour >= 0 && startHour < 24 &&
                       int.TryParse(StartMinute.Text, out int startMinute) && startMinute >= 0 && startMinute < 60 &&
                       int.TryParse(EndHour.Text, out int endHour) && endHour >= 0 && endHour < 24 &&
                       int.TryParse(EndMinute.Text, out int endMinute) && endMinute >= 0 && endMinute < 60;
            };
            validateRoom = () =>
            {
                return RoomTextBlock.Text != null &&
                       int.TryParse(RoomTextBlock.Text, out int roomNumber) &&
                       _hospitalReference.Rooms.Contains(roomNumber);
            };

            validateAllInputs = () =>
            {
                if (!validatePatient())
                {
                    MessageBox.Show("Please select a patient.");
                    return false;
                }

                if (!validateStaff())
                {
                    MessageBox.Show("Please select at least one staff member.");
                    return false;
                }

                if (!validateDates())
                {
                    MessageBox.Show("Please select valid start and end dates.");
                    return false;
                }

                if (!validateTime())
                {
                    MessageBox.Show("Please enter valid time values.");
                    return false;
                }

                if (!validateRoom())
                {
                    MessageBox.Show("Invalid room number.");
                    return false;
                }

                return true;
            };
            #endregion
        }

        private void Error_Date_Loaded(object sender, RoutedEventArgs e)
        {
            StartDate.DisplayDateStart = DateTime.Today;
            StartDate.DisplayDateEnd = DateTime.Today.AddMonths(3);
            EndDate.DisplayDateStart = DateTime.Today;
            EndDate.DisplayDateEnd = DateTime.Today.AddMonths(3);
        }

        private void PurposeBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            if (AppointmentPurposeComboBox.SelectedItem is AppointmentPurpose selectedPurpose)
            {
                _selectedPurpose = selectedPurpose;
            }
        }
        private void SelectPatient_Click(object sender, RoutedEventArgs e)
        {
            if (PatientDataGrid.SelectedItem is Patient patient)
            {
                selectedPatient = patient;

                AppointmentsDataGrid.ItemsSource = null;
                AppointmentsDataGrid.ItemsSource = selectedPatient.Schedule.Appointments;
                AppointmentsDataGrid.Items.Refresh();

                MessageBox.Show($"Selected Patient: {patient.FirstName} {patient.LastName}");
            }
            else
            {
                MessageBox.Show("Please select a patient.");
            }
        }

        private void AddStaff_Click(object sender, RoutedEventArgs e)
        {
            if (StaffDataGrid.SelectedItem is Staff staff)
            {
                if (!selectedStaff.Contains(staff))
                {
                    selectedStaff.Add(staff);
                    StaffList.Items.Add($"{staff.ID} {staff.GetFullName()}");
                }
                else
                {
                    MessageBox.Show("Staff member is already selected.");
                }
            }
            else
            {
                MessageBox.Show("Please select a staff member.");
            }
        }

        private void RemoveStaff_Click(object sender, RoutedEventArgs e)
        {
            if (StaffList.SelectedIndex >= 0)
            {
                string staffName = (string)StaffList.SelectedItem;
                var staffToRemove = selectedStaff.FirstOrDefault(s => $"{s.ID} {s.GetFullName}" == staffName);
                if (staffToRemove != null)
                {
                    selectedStaff.Remove(staffToRemove);
                    StaffList.Items.Remove(staffName);
                }
            }
            else
            {
                MessageBox.Show("Please select a staff member to remove.");
            }
        }

        private void CreateAppointment_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if(!validateAllInputs())
                return;

            try
            {
                DateTime startDateTime = StartDate.SelectedDate.Value
                    .AddHours(int.Parse(StartHour.Text))
                    .AddMinutes(int.Parse(StartMinute.Text));
                DateTime endDateTime = EndDate.SelectedDate.Value
                    .AddHours(int.Parse(EndHour.Text))
                    .AddMinutes(int.Parse(EndMinute.Text));

                if (startDateTime >= endDateTime)
                {
                    MessageBox.Show("Start time must be earlier than end time.");
                    return;
                }

                int roomNumber = int.Parse(RoomTextBlock.Text);
                selectedPatient.Schedule.CreateAppointment(
                    roomNumber, 
                    startDateTime, 
                    endDateTime, 
                    new List<Staff>(selectedStaff), 
                    selectedPatient, 
                    _selectedPurpose);
                
                refreshAppointments(AppointmentsDataGrid, selectedPatient);

                MessageBox.Show("Appointment created successfully!");
                ClearForm();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ClearForm()
        {
            selectedStaff.Clear();
            StaffList.Items.Clear();
            StartDate.SelectedDate = null;
            EndDate.SelectedDate = null;
        }

        private void RemoveAppointment_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
