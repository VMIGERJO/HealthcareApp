using BL.DTO;
using BL.Managers;
using BL.Managers.Interfaces;
using EFDal.Entities;
using HealthCareAppWPF.DTO;
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
    /// Interaction logic for PatientSearchControl.xaml
    /// </summary>
    public partial class PatientSearchControl : UserControl
    {
        private IPatientManager _patientManager;
        private MainWindow _mainWindow;
        public PatientSearchControl(IPatientManager patientManager, MainWindow mainWindow)
        {
            InitializeComponent();
            this._patientManager = patientManager;
            this._mainWindow = mainWindow;
        }

        private void PatientSearchButton_Click(object sender, RoutedEventArgs e)
        {
            PatientSearchValuesDTO patientQuery = new();
            patientQuery.FirstName = PatientFirstNameBox.Text;
            patientQuery.LastName = PatientLastNameBox.Text;
            List<PatientBasicDTO> matchingPatients = _patientManager.PatientSearch(patientQuery);
            PatientListView.ItemsSource = matchingPatients;
            PatientListView.SelectionChanged += PatientListView_SelectionChanged;

        }

        private async void PatientListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientListView.SelectedItem != null)
            {
                int patientId = ((PatientBasicDTO)PatientListView.SelectedItem).Id;
                Patient selectedPatient = await _patientManager.GetById(patientId);
                AddressTextBox.Text = selectedPatient.Address;
                MedicalHistoryTextBox.Text = selectedPatient.MedicalHistory;
                PatientDetailsContent.Visibility = Visibility.Visible;
            }
            else
            {
                PatientDetailsContent.Visibility = Visibility.Hidden;
            }

        }

        private async void UpdateMedicalHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            int patientId = ((PatientBasicDTO)PatientListView.SelectedItem).Id;
            Patient selectedPatient = await _patientManager.GetById(patientId);
            selectedPatient.MedicalHistory = MedicalHistoryTextBox.Text;
            _patientManager.Update(selectedPatient);
        }

        private async void CreatePrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientListView.SelectedItem != null)
            {
                int patientId = ((PatientBasicDTO)PatientListView.SelectedItem).Id;
                Patient selectedPatient = await _patientManager.GetById(patientId);
                IMedicationManager medicationManager = App.ServiceProvider.GetService<IMedicationManager>();
                CreatePrescriptionControl createPrescriptionControl = new(selectedPatient, medicationManager);
                _mainWindow.NavigateToView(createPrescriptionControl);
            }
            
        }
    }
}
