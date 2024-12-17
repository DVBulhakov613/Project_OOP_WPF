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
    /// Interaction logic for CreateStaff.xaml
    /// </summary>
    public partial class CreateStaff : UserControl
    {
        private MainWindow _mainWindow;
        private Hospital _selectedHospital;
        private List<StaffRole> _selectedRoles = new List<StaffRole>();
        public CreateStaff(MainWindow mainWindow, Hospital selectedHospital)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            _selectedHospital = selectedHospital;
            StaffDataGrid.ItemsSource = selectedHospital.ActiveStaff;
        }


        private void AddStaff_DateOfBirth_Loaded(object sender, RoutedEventArgs e)
        {
            //AddStaff_DateOfBirth.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddYears(-18), DateTime.MaxValue));
            AddStaff_DateOfBirth.DisplayDateStart = DateTime.Today.AddYears(-60);
            AddStaff_DateOfBirth.DisplayDateEnd = DateTime.Today.AddYears(-18);
        }

        private void AddStaff_DateOfBirth_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                MessageBox.Show("! PATIENT: Invalid date format. Please enter a valid date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (e.Text != null)
            {
                MessageBox.Show($"! PATIENT: The date '{e.Text}' is not allowed. Please select a valid date.", "Date Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            e.ThrowException = false;
        }

        private void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // parse the info
                string firstName = AddStaff_FirstName.Text;
                string middleName = AddStaff_MiddleName.Text;
                string lastName = AddStaff_LastName.Text;
                DateTime birthDay;
                if (AddStaff_DateOfBirth.SelectedDate.HasValue)
                {
                    birthDay = AddStaff_DateOfBirth.SelectedDate.Value;
                }
                else
                    throw new ArgumentException("! PATIENT: Must always have a birthday.");

                // check stuff then try making an instance
                Staff staff = new Staff(firstName, middleName, lastName, birthDay, _selectedRoles, _selectedHospital);
                //if (AddPatient_HasRecordsCheckbox.IsChecked == true) { }

                _selectedHospital.AddStaff(staff);
                StaffDataGrid.ItemsSource = null;
                StaffDataGrid.ItemsSource = _selectedHospital.ActiveStaff;
                StaffDataGrid.Items.Refresh();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Errors encountered", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void SelectStaffButton_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItem = StaffDataGrid.SelectedItem;
            //if (selectedItem != null)
            //{
            //    if (selectedItem is Staff selectedStaff)
            //    {
            //        _mainWindow._selectedStaff = selectedStaff;
            //        MessageBox.Show($"Staff {selectedStaff.ID} has been selected.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Selected item is not of type Staff.", "Type Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Click on a row on the table to select a Staff member.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}

            if (StaffDataGrid.SelectedItem is Staff selectedStaff)
            {
                _mainWindow._selectedStaff = selectedStaff;
                MessageBox.Show($"Staff {selectedStaff.ID} has been selected.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Click on a row on the table to select a Patient.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void StaffRoleComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            StaffRoleComboBox.ItemsSource = Enum.GetValues(typeof(StaffRole)).Cast<StaffRole>();
        }

        private void AddRoleButton_Click(object sender, RoutedEventArgs e)
        {
            if (StaffRoleComboBox.SelectedItem is StaffRole selectedRole)
            {
                if (!_selectedRoles.Contains(selectedRole))
                {
                    _selectedRoles.Add(selectedRole);
                    RefreshSelectedRolesListBox();
                }
                else
                {
                    MessageBox.Show("Role already added.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a role to add.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoveRoleButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRolesListBox.SelectedItem is StaffRole selectedRole)
            {
                _selectedRoles.Remove(selectedRole);
                RefreshSelectedRolesListBox();
            }
            else
            {
                MessageBox.Show("Please select a role to remove.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshSelectedRolesListBox()
        {
            SelectedRolesListBox.ItemsSource = null;
            SelectedRolesListBox.ItemsSource = _selectedRoles;
        }

        // Optional: Access the selected roles to pass them into the constructor later
        public List<StaffRole> GetSelectedRoles()
        {
            return _selectedRoles;
        }
    }
}
