using BL.Managers.Interfaces;
using EFDal.Entities;
using BL.DTO;
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
using System.Collections.ObjectModel;
using BL.Managers;
using HealthCareAppWPF.DTO;
using BL;

namespace HealthCareAppWPF
{
    /// <summary>
    /// Interaction logic for CreatePrescriptionControl.xaml
    /// </summary>
    public partial class CreatePrescriptionControl : UserControl
    {
        private Patient _patient;
        private Doctor _doctor;
        private IMedicationManager _medicationManager;
        private IPrescriptionManager _prescriptionManager;
        private ObservableCollection<MedicationBasicDTO> _currentPrescriptionMedications = new();
        public CreatePrescriptionControl(Patient patient, Doctor doctor, IMedicationManager medicationManager, IPrescriptionManager prescriptionManager)
        {
            InitializeComponent();
            this._patient = patient;
            this._medicationManager = medicationManager;
            this._prescriptionManager = prescriptionManager;
            this._doctor = doctor;
            TitleTextBlock.Text = $"{DateTime.Now:yyyy-MM-dd} - {patient.FirstName} {patient.LastName}";
            PrescriptionListView.ItemsSource = _currentPrescriptionMedications;
            LoadMedicationsAsync();
        }

        private async Task LoadMedicationsAsync()
        {
            List<MedicationBasicDTO> allMedications = await _medicationManager.GetAllMedicationsAsync();
            MedicationsListView.ItemsSource = allMedications;
        }

        private void AddToPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            MedicationBasicDTO? selectedMedication = MedicationsListView.SelectedItem as MedicationBasicDTO;

            if (selectedMedication != null)
            {
                _currentPrescriptionMedications.Add(selectedMedication);
            }

        }

        private void RemoveFromPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            MedicationBasicDTO? selectedMedication = PrescriptionListView.SelectedItem as MedicationBasicDTO;

            if (selectedMedication != null)
            {
                _currentPrescriptionMedications.Remove(selectedMedication);
            }
        }

        private async void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            MedicationSearchValuesDTO medicationQuery = new();
            medicationQuery.Name = FilterByNameBox.Text;
            List<MedicationBasicDTO> matchingMedications = await _medicationManager.MedicationSearchAsync(medicationQuery);
            MedicationsListView.ItemsSource = matchingMedications;
        }

        private async void CreatePrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> medicationIds = new();

            Prescription newPrescription = new()
            {
                PatientID = _patient.Id,
                DoctorID = _doctor.Id,
                PrescriptionDate = DateTime.Now
            };

            foreach (MedicationBasicDTO medicationDTO in _currentPrescriptionMedications)
            {
                newPrescription.Medications.Add(new Medication() { Id = medicationDTO.Id });

            }
            try
            {
                _prescriptionManager.Add(newPrescription);
                MessageBox.Show("Prescription created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating prescription: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
    }
}
