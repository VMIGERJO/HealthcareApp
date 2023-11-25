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
using System.Windows.Shapes;
using BL.Managers.Interfaces;
using EFDal.Entities;

namespace HealthCareAppWPF
{
    /// <summary>
    /// Interaction logic for EditAddressWindow.xaml
    /// </summary>
    public partial class EditAddressWindow : Window
    {
        private IPatientManager _patientManager;
        private Patient _currentPatient;
        public EditAddressWindow(IPatientManager patientManager, Patient currentPatient)
        {
            InitializeComponent();
            this._patientManager = patientManager;
            this._currentPatient = currentPatient;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Address address = new()
            {
                Street = StreetBox.Text,
                HouseNumber = HouseNumberBox.Text,
                Appartment = AppartmentBox.Text,
                City = CityBox.Text,
                PostalCode = PostalCodeBox.Text,
                Country = CountryBox.Text
            };

            _currentPatient.Address = address;
            try
            {
                _patientManager.Update(_currentPatient);
                MessageBox.Show("Address updated succesfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating address: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            this.Close();
        }
    }
}
