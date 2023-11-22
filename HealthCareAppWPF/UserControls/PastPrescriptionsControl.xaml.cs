using BL.DTO;
using BL.Managers.Interfaces;
using EFDal.Entities;
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
    /// Interaction logic for PastPrescriptionsControl.xaml
    /// </summary>
    public partial class PastPrescriptionsControl : UserControl
    {
        private IPrescriptionManager _prescriptionManager;
        private int _currentDoctorId;
        public PastPrescriptionsControl(IPrescriptionManager prescriptionManager, int currentDoctorId)
        {
            InitializeComponent();
            _currentDoctorId = currentDoctorId;
            _prescriptionManager = prescriptionManager;
            LoadPageInformation();
        }

        private async Task LoadPageInformation()
        {
            PrescriptionListView.ItemsSource = await _prescriptionManager.PrescriptionSearchAsync(new PrescriptionSearchValuesDTO() { DoctorID = _currentDoctorId });
        }

        private async void RepeatPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrescriptionViewDTO? selectedPrescriptionDTO = PrescriptionListView.SelectedItem as PrescriptionViewDTO;

                
                    int prescriptionId = selectedPrescriptionDTO.Id;

                    // Get the original Prescription
                    Prescription originalPrescription = await _prescriptionManager.GetById(prescriptionId);

                    // Create a copy of the original prescription and update PrescriptionDate to the current date
                    Prescription repeatedPrescription = new Prescription
                    {
                        DoctorID = originalPrescription.DoctorID,
                        PatientID = originalPrescription.PatientID,
                        PrescriptionDate = DateTime.Now,
                    };
                    repeatedPrescription.Medications.AddRange(originalPrescription.Medications);
                    bool addSuccesful = _prescriptionManager.Add(repeatedPrescription);
                if (addSuccesful)
                {
                    LoadPageInformation();
                    // Show a success message
                    MessageBox.Show("Prescription repeated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information); 
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show($"Error repeating prescription: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
