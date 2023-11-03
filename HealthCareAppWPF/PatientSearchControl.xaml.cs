using BL.DTO;
using BL.Managers;
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
        }

        
    }
}
