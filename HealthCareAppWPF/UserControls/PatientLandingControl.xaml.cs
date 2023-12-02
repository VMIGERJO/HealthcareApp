using BL.Managers.Interfaces;
using BL.DTO;
using DAL.Entities;
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
        private PatientDTO _patient;
        private MainWindow _mainWindow;
        private IPatientManager _patientManager;
        private IPrescriptionManager _prescriptionManager;
        public PatientLandingControl(IPatientManager patientManager, IPrescriptionManager prescriptionManager, int patientId, MainWindow mainWindow)
        {
            InitializeComponent();

            this._mainWindow = mainWindow;
            this._patientManager = patientManager;
            this._prescriptionManager = prescriptionManager;
            RefreshPageInformation(patientId);

        }

        public async Task RefreshPageInformation(int patientId) {
            this._patient = await _patientManager.GetPatientByIdIncludingAddressAsync(patientId);
            LoadPageInformation();
        }

        public async Task LoadPageInformation()
        {
            TitleTextBlock.Text = $"Welcome {_patient.FirstName} {_patient.LastName}";
            AddressTextBox.Text = _patient.Address.ToString();
            MedicalHistoryTextBox.Text = _patient.MedicalHistory;
            PrescriptionListView.ItemsSource = await _prescriptionManager.PrescriptionSearchAsync(new PrescriptionSearchValuesDTO() { PatientID = _patient.Id });
        }


        private void SearchDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorSearchControl doctorSearchControl = App.ServiceProvider.GetService<DoctorSearchControl>();
            _mainWindow.NavigateToView(doctorSearchControl);
            
        }

        private void EditAddressButton_Click(object sender, RoutedEventArgs e)
        {
            IPatientManager patientManager = App.ServiceProvider.GetService<IPatientManager>();
            EditAddressWindow editAddressWindow = new(patientManager, _patient, this);
            editAddressWindow.Show();
        }
    }
}
