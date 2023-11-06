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
    /// Interaction logic for DoctorLoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        private IDoctorManager _doctorManager;
        private IPatientManager _patientManager;
        private MainWindow _mainWindow;
        public LoginControl(MainWindow mainWindow, IDoctorManager doctorManager, IPatientManager patientManager)
        {
            InitializeComponent();
            this._doctorManager = doctorManager;
            this._patientManager = patientManager;
            this._mainWindow = mainWindow;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedRole = RoleDropdown.SelectedItem as ComboBoxItem;

            if (selectedRole != null)
            {
                string role = selectedRole.Content.ToString();

                if (role == "Patient")
                {
                    HandlePatientLogin();
                }
                else if (role == "Doctor")
                {
                    HandleDoctorLogin();
                }
            }
        }

        private void HandlePatientLogin()
        {
            PatientSearchValuesDTO patientQuery = new();
            patientQuery.FirstName = DoctorLoginFirstNameBox.Text;
            patientQuery.LastName = DoctorLoginLastNameBox.Text;
            Patient loggedInPatient = _patientManager.UniquePatientSearch(patientQuery);
            PatientLandingControl patientLandingControl = new(loggedInPatient, _mainWindow);
            _mainWindow.NavigateToView(patientLandingControl);
        }

        private void HandleDoctorLogin()
        {
            DoctorSearchValuesDTO doctorQuery = new();
            doctorQuery.FirstName = DoctorLoginFirstNameBox.Text;
            doctorQuery.LastName = DoctorLoginLastNameBox.Text;
            Doctor loggedInDoctor = _doctorManager.UniqueDoctorSearch(doctorQuery);
            DoctorLandingPage doctorLandingPage = new(_mainWindow, loggedInDoctor);
            _mainWindow.NavigateToView(doctorLandingPage);
        }
    }
}
