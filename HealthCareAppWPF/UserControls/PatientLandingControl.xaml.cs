using BL.Managers.Interfaces;
using BL.DTO;
using EFDal.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace HealthCareAppWPF
{
    /// <summary>
    /// Interaction logic for PatientLandingControl.xaml
    /// </summary>
    public partial class PatientLandingControl : UserControl
    {
        private Patient _patient;
        private MainWindow _mainWindow;
        private IPatientManager _patientManager;
        private IPrescriptionManager _prescriptionManager;
        public PatientLandingControl(IPatientManager patientManager, IPrescriptionManager prescriptionManager, Patient patient, MainWindow mainWindow)
        {
            InitializeComponent();
            this._patient = patient;
            this._mainWindow = mainWindow;
            this._patientManager = patientManager;
            this._prescriptionManager = prescriptionManager;
            LoadPageInformation();

            
        }

        private async Task LoadPageInformation()
        {
            TitleTextBlock.Text = $"Welcome {_patient.FirstName} {_patient.LastName}";
            AddressTextBox.Text = _patient.Address;
            MedicalHistoryTextBox.Text = _patient.MedicalHistory;
            PrescriptionListView.ItemsSource = await _prescriptionManager.PrescriptionSearchAsync(new PrescriptionSearchValuesDTO() { PatientID = _patient.Id });
        }

        private void SearchDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorSearchControl doctorSearchControl = App.ServiceProvider.GetService<DoctorSearchControl>();
            _mainWindow.NavigateToView(doctorSearchControl);
            
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddressTextBox.Text != _patient.Address)
            {
                _patient.Address = AddressTextBox.Text;
                _patientManager.Update(_patient);
            }
            else
            {
                MessageBox.Show("No changes to update.");
            }

        }

        private void EditAddressButton_Click(object sender, RoutedEventArgs e)
        {
            AddressTextBox.IsReadOnly = !AddressTextBox.IsReadOnly;
            UpdateButton.Visibility = UpdateButton.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
