using BL.DTO;
using BL.Managers.Interfaces;
using HealthCareAppWPF.DTO;
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
using System.Windows.Shapes;

namespace HealthCareAppWPF
{
    /// <summary>
    /// Interaction logic for PatientSearchWindow.xaml
    /// </summary>
    public partial class PatientSearchWindow : Window
    {
        private IPatientManager _patientManager;
        public PatientSearchWindow(IPatientManager patientManager)
        {
            InitializeComponent();
            this._patientManager = patientManager;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            PatientSearchValuesDTO patientQuery = new();
            patientQuery.FirstName = FirstNameBox.Text;
            patientQuery.LastName = LastNameBox.Text;

            List<PatientBasicDTO> matchingPatients = _patientManager.PatientSearch(patientQuery);

            PatientListView.ItemsSource = matchingPatients;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the DoctorSearchWindow and return to the Main Window.
            this.Close();
        }

    }
}
