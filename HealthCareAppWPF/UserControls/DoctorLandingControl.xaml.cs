using BL.DTO;
using BL.Managers;
using BL.Managers.Interfaces;
using DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
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

namespace HealthCareAppWPF
{
    /// <summary>
    /// Interaction logic for DoctorLandingPage.xaml
    /// </summary>
    public partial class DoctorLandingControl : UserControl
    {
        private DoctorDTO _doctor;
        private MainWindow _mainWindow;
        private IDoctorManager _doctorManager;
        public DoctorLandingControl(MainWindow mainWindow, IDoctorManager doctorManager, DoctorDTO doctor)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
            this._doctor = doctor;
            this._doctorManager = doctorManager;
            TitleTextBlock.Text = $"Welcome Dr. {doctor.FirstName} {doctor.LastName}";
            SpecializationTextBox.Text = $"{doctor.Specialization}";
        }


        private void PrescriptionsHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            IPrescriptionManager prescriptionManager = App.ServiceProvider.GetService<IPrescriptionManager>();
            PastPrescriptionsControl pastPrescriptionsControl = new(prescriptionManager, _doctor.Id);
            _mainWindow.NavigateToView(pastPrescriptionsControl);
        }

        private void ManagePatientsButton_Click(object sender, RoutedEventArgs e)
        {
            IPatientManager patientManager = App.ServiceProvider.GetService<IPatientManager>();
            PatientSearchControl patientSearchControl = new(patientManager, _mainWindow, _doctor);
            _mainWindow.NavigateToView(patientSearchControl);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _doctor.Specialization = SpecializationTextBox.Text;

            try
            {
                _doctorManager.Update(_doctor);
                MessageBox.Show("Specialization updated succesfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating specialization: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
    }
}
