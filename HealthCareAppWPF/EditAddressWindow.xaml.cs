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
using System.Windows.Shapes;
using BL.DTO;
using BL.Managers.Interfaces;
using DAL.Entities;

namespace HealthCareAppWPF
{
    /// <summary>
    /// Interaction logic for EditAddressWindow.xaml
    /// </summary>
    public partial class EditAddressWindow : Window
    {
        private IPatientManager _patientManager;
        private PatientDTO _currentPatient;
        private PatientLandingControl _patientLandingControl;
        public EditAddressWindow(IPatientManager patientManager, PatientDTO currentPatient, PatientLandingControl patientLandingControl)
        {
            InitializeComponent();
            this._patientManager = patientManager;
            this._currentPatient = currentPatient;
            this._patientLandingControl = patientLandingControl;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AddressDTO address = new()
            {
                Street = StreetBox.Text,
                HouseNumber = HouseNumberBox.Text,
                Appartment = AppartmentBox.Text,
                City = CityBox.Text,
                PostalCode = PostalCodeBox.Text,
                Country = CountryBox.Text
            };

            _currentPatient.Address = address;
            List<string> validationErrors = _patientManager.ValidatePatient(_currentPatient);

            if (validationErrors.Count > 0)
            {
                // Display all validation errors to the user
                MessageBox.Show(string.Join("\n", validationErrors));
                return;
            }
            try
            {
                _patientManager.Update(_currentPatient);
                MessageBox.Show("Address updated succesfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _patientLandingControl.RefreshPageInformation(_currentPatient.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating address: {string.Join("\n", validationErrors)}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            this.Close();
        }
    }
}
