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
        
        // me and wpf are in a love-hate relationship
        public CreateAppointments(Hospital hospitalReference)
        {
            InitializeComponent();
            _hospitalReference = hospitalReference;

            PatientDataGrid.ItemsSource = hospitalReference.Patients;
            StaffDataGrid.ItemsSource = hospitalReference.ActiveStaff;
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
            if (selectedPatient == null)
            {
                MessageBox.Show("Please select a patient.");
                return;
            }

            if (selectedStaff.Count == 0)
            {
                MessageBox.Show("Please select at least one staff member.");
                return;
            }

            if (!StartDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a valid start date.");
                return;
            }
            DateTime startDate = StartDate.SelectedDate.Value;

            if (!EndDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a valid end date.");
                return;
            }
            DateTime endDate = EndDate.SelectedDate.Value;

            if (!int.TryParse(StartHour.Text, out int startHour) || startHour < 0 || startHour > 23)
            {
                MessageBox.Show("Please enter a valid start hour (0-23).");
                return;
            }

            if (!int.TryParse(StartMinute.Text, out int startMinute) || startMinute < 0 || startMinute > 59)
            {
                MessageBox.Show("Please enter a valid start minute (0-59).");
                return;
            }

            if (!int.TryParse(EndHour.Text, out int endHour) || endHour < 0 || endHour > 23)
            {
                MessageBox.Show("Please enter a valid end hour (0-23).");
                return;
            }

            if (!int.TryParse(EndMinute.Text, out int endMinute) || endMinute < 0 || endMinute > 59)
            {
                MessageBox.Show("Please enter a valid end minute (0-59).");
                return;
            }

            DateTime startDateTime = startDate.AddHours(startHour).AddMinutes(startMinute);
            DateTime endDateTime = endDate.AddHours(endHour).AddMinutes(endMinute);

            if (startDateTime >= endDateTime)
            {
                MessageBox.Show("Start time must be earlier than end time.");
                return;
            }

            if (RoomTextBlock.Text != null && int.TryParse(RoomTextBlock.Text, out int roomNumber))
            {
                if (!_hospitalReference.Rooms.Contains(roomNumber))
                {
                    MessageBox.Show($"{RoomTextBlock.Text} does not exist within this Hospital.");
                }
            } else { MessageBox.Show($"{RoomTextBlock.Text} is not a valid room number."); return; }

            try
            {

                selectedPatient.Schedule.CreateAppointment(roomNumber, startDateTime, endDateTime, new List<Staff>(selectedStaff), selectedPatient, _selectedPurpose);
                
                AppointmentsDataGrid.ItemsSource = null;
                AppointmentsDataGrid.ItemsSource = selectedPatient.Schedule.Appointments;
                AppointmentsDataGrid.Items.Refresh();


                MessageBox.Show("Appointment created successfully!");
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            selectedStaff.Clear();
            StaffList.Items.Clear();
            StartDate.SelectedDate = null;
            EndDate.SelectedDate = null;
        }
    }
}
