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
    public partial class DoctorLandingPage : UserControl
    {
        private Doctor _doctor;
        private MainWindow _mainWindow;
        public DoctorLandingPage(MainWindow mainWindow, Doctor doctor)
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
            PastPrescriptionsControl pastPrescriptionsControl = App.ServiceProvider.GetService<PastPrescriptionsControl>();
            _mainWindow.NavigateToView(pastPrescriptionsControl);
        }

        private void ManagePatientsButton_Click(object sender, RoutedEventArgs e)
        {
            PatientSearchControl patientSearchControl = App.ServiceProvider.GetService<PatientSearchControl>();
            _mainWindow.NavigateToView(patientSearchControl);
        }
    }
}
