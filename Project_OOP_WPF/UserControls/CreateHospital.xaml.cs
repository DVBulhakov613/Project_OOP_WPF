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
using System.Xml.Linq;

namespace Project_OOP_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for CreateHospital.xaml
    /// </summary>
    public partial class CreateHospital : UserControl
    {
        private MainWindow _mainWindow;

        public CreateHospital(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            HospitalDataGrid.ItemsSource = _mainWindow.Hospitals;
        }

        private void AddHospitalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = AddHospital_Name.Text;
                string location = AddHospital_Location.Text;
                string[] roomsInput = AddHospital_Rooms.Text.Split(',');

                // parse rooms into a SortedSet
                SortedSet<int> rooms = new SortedSet<int>();
                foreach (var room in roomsInput)
                {
                    if (int.TryParse(room.Trim(), out int roomNumber))
                    {
                        rooms.Add(roomNumber);
                    }
                    else
                    {
                        throw new Exception($"Invalid room number: {room}");
                    }
                }

                // create a new Hospital object
                Hospital newHospital = new Hospital(name, location, rooms);

                // update main hospital list
                _mainWindow.Hospitals.Add(newHospital);

                // clear inputs after success
                AddHospital_Name.Clear();
                AddHospital_Location.Clear();
                AddHospital_Rooms.Clear();

                // update the grid
                HospitalDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveHospitalButton_Click(object sender, RoutedEventArgs e)
        {
            if (HospitalDataGrid.SelectedItem is Hospital selectedHospital)
            {
                _mainWindow.Hospitals.Remove(selectedHospital);

                HospitalDataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a hospital to remove.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SelectHospitalButton_Click(object sender, RoutedEventArgs e)
        {
            if (HospitalDataGrid.SelectedItem is Hospital selectedHospital)
            {
                _mainWindow._selectedHospital = selectedHospital;
                MessageBox.Show($"Hospital {selectedHospital.ID} has been selected.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Click on a row on the table to select a Hospital.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void default_hospital_click(object sender, RoutedEventArgs e)
        {
            Hospital newHospital = new Hospital("default", "default", new() { 1, 2, 3 });

            // update main hospital list
            _mainWindow.Hospitals.Add(newHospital);

            // clear inputs after success
            AddHospital_Name.Clear();
            AddHospital_Location.Clear();
            AddHospital_Rooms.Clear();

            _mainWindow._selectedHospital = newHospital;

            // update the grid
            HospitalDataGrid.Items.Refresh();
        }
    }
}
