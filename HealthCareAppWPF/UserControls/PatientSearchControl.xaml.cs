﻿using BL.DTO;
using BL.Managers;
using BL.Managers.Interfaces;
using DAL.Entities;
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
        private DoctorDTO _doctor;
        public PatientSearchControl(IPatientManager patientManager, MainWindow mainWindow, DoctorDTO doctor)
        {
            InitializeComponent();
            this._patientManager = patientManager;
            this._mainWindow = mainWindow;
            this._doctor = doctor;
        }

        private async void PatientSearchButton_Click(object sender, RoutedEventArgs e)
        {
            PatientSearchValuesDTO patientQuery = new();
            patientQuery.FirstName = PatientFirstNameBox.Text;
            patientQuery.LastName = PatientLastNameBox.Text;
            List<PatientBasicDTO> matchingPatients = await _patientManager.SearchPatientsAsync(patientQuery);
            PatientListView.ItemsSource = matchingPatients;
            PatientListView.SelectionChanged += PatientListView_SelectionChanged;

        }

        private async void PatientListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientListView.SelectedItem != null)
            {
                int patientId = ((PatientBasicDTO)PatientListView.SelectedItem).Id;
                PatientDTO selectedPatient = await _patientManager.GetPatientByIdIncludingAddressAsync(patientId);
                AddressTextBox.Text = selectedPatient.Address.ToString();
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
            PatientDTO selectedPatient = await _patientManager.GetPatientByIdIncludingAddressAsync(patientId);
            selectedPatient.MedicalHistory = MedicalHistoryTextBox.Text;
            
            try
            {
                _patientManager.Update(selectedPatient);
                MessageBox.Show("Patient medical history updated succesfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating patient medical history: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private async void CreatePrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientListView.SelectedItem != null)
            {
                int patientId = ((PatientBasicDTO)PatientListView.SelectedItem).Id;
                PatientDTO selectedPatient = await _patientManager.GetByIdAsync(patientId);
                IMedicationManager medicationManager = App.ServiceProvider.GetService<IMedicationManager>();
                IPrescriptionManager prescriptionManager = App.ServiceProvider.GetService<IPrescriptionManager>();
                CreatePrescriptionControl createPrescriptionControl = new(selectedPatient, _doctor, medicationManager, prescriptionManager);
                _mainWindow.NavigateToView(createPrescriptionControl);
            }
            
        }
    }
}
