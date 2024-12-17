﻿using System;
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
    /// Interaction logic for CreatePatient.xaml
    /// </summary>
    public partial class CreatePatient : UserControl
    {
        private MainWindow _mainWindow;
        private Hospital _selectedHospital;
        public CreatePatient(MainWindow mainWindow, Hospital selectedHospital)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            PatientDataGrid.ItemsSource = _mainWindow.Hospitals;
            this._selectedHospital = selectedHospital;
        }

        private void AddPatient_DateOfBirth_Loaded(object sender, RoutedEventArgs e)
        {
            AddPatient_DateOfBirth.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(1), DateTime.MaxValue));
        }

        private void AddPatient_DateOfBirth_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
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

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // parse the info
                string firstName = AddPatient_FirstName.Text;
                string middleName = AddPatient_MiddleName.Text;
                string lastName = AddPatient_LastName.Text;
                DateTime birthDay;
                if (AddPatient_DateOfBirth.SelectedDate.HasValue)
                {
                    birthDay = AddPatient_DateOfBirth.SelectedDate.Value;
                }
                else
                    throw new ArgumentException("! PATIENT: Must always have a birthday.");
                
                // check stuff then try making an instance
                Patient patient = new Patient(firstName, middleName, lastName, birthDay, _selectedHospital);
                //if (AddPatient_HasRecordsCheckbox.IsChecked == true) { }

                _selectedHospital.AddPatient(firstName, middleName, lastName, birthDay);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Errors encountered", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
