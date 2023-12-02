using BL.DTO;
using BL.Managers;
using BL.Managers.Interfaces;
using DAL.Entities;
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

namespace HealthCareAppWPF.UserControls
{
    /// <summary>
    /// Interaction logic for HealthAgencyDashboardControl.xaml
    /// </summary>
    public partial class HealthAgencyDashboardControl : UserControl
    {
        private IMedicationManager _medicationManager;
        public HealthAgencyDashboardControl(IMedicationManager medicationManager)
        {
            InitializeComponent();
            _medicationManager = medicationManager;
        }

        private void AddMedicationButton_Click(object sender, RoutedEventArgs e)
        {
            CreateMedicationDTO newMedicationDTO = new()
            {
                Name = MedicationNameTextBox.Text,
                Dosage = DosageTextBox.Text,
                ActiveSubstance = ActiveSubstanceTextBox.Text,
                Manufacturer = ManufacturerTextBox.Text
            };
            try
            {
                
                bool addSuccesful = _medicationManager.Add(newMedicationDTO);
                if (addSuccesful)
                {
                    // Show a success message
                    MessageBox.Show("Medication added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show($"Error adding medication: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
