using BL.DTO;
using BL.Managers;
using BL.Managers.Interfaces;
using DAL.Entities;
using HealthCareAppWPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DoctorSearchControl.xaml
    /// </summary>
    public partial class DoctorSearchControl : UserControl
    {
        private MainWindow _mainWindow;
        private IDoctorManager _doctorManager;
        public ObservableCollection<DoctorBasicDTO> DisplayedDoctors { get; set; }

        public DoctorSearchControl(MainWindow mainWindow, IDoctorManager doctorManager)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
            this._doctorManager = doctorManager;
            DisplayedDoctors = new ObservableCollection<DoctorBasicDTO>();
            DoctorListView.ItemsSource = DisplayedDoctors;
            try
            {
                LoadDoctorsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading doctors: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async Task LoadDoctorsAsync()
        {
            List<DoctorBasicDTO> allDoctors = await _doctorManager.GetAllDoctorsAsync();

            DisplayedDoctors.Clear();
            foreach (DoctorBasicDTO doctor in allDoctors)
            {
                DisplayedDoctors.Add(doctor);
            }

            UpdateSpecializationDropdown();
        }

        private async void DoctorSearchButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorSearchValuesDTO doctorQuery = new();
            doctorQuery.FirstName = DoctorFirstNameBox.Text;
            doctorQuery.LastName = DoctorLastNameBox.Text;
            doctorQuery.Specialization = SpecializationDropdown.SelectedItem as string;
            List<DoctorBasicDTO> matchingDoctors = await _doctorManager.DoctorSearchAsync(doctorQuery);
            DisplayedDoctors.Clear();
            foreach (DoctorBasicDTO doctor in matchingDoctors)
            {
                DisplayedDoctors.Add(doctor);
            }
        }

        private void UpdateSpecializationDropdown()
        {
            List<string> displayedSpecializations = DisplayedDoctors.Select(d => d.Specialization).Distinct().ToList();
            SpecializationDropdown.ItemsSource = displayedSpecializations;
        }
    }
}
