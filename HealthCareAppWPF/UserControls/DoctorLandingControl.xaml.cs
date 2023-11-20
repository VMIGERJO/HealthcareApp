using BL.Managers.Interfaces;
using EFDal.Entities;
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
        private Doctor _doctor;
        private MainWindow _mainWindow;
        public DoctorLandingControl(MainWindow mainWindow, Doctor doctor)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
            this._doctor = doctor;
            TitleTextBlock.Text = $"Welcome Dr. {doctor.FirstName} {doctor.LastName}";
            SpecializationTextBox.Text = $"{doctor.Specialization}";
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrescriptionsHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            IPrescriptionManager prescriptionManager = App.ServiceProvider.GetService<IPrescriptionManager>();
            PastPrescriptionsControl pastPrescriptionsControl = new(prescriptionManager, _doctor.Id);
            ;
            _mainWindow.NavigateToView(pastPrescriptionsControl);
        }

        private void ManagePatientsButton_Click(object sender, RoutedEventArgs e)
        {
            IPatientManager patientManager = App.ServiceProvider.GetService<IPatientManager>();
            PatientSearchControl patientSearchControl = new(patientManager, _mainWindow, _doctor);
            _mainWindow.NavigateToView(patientSearchControl);
        }
    }
}
