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
    public partial class LandingControl : UserControl
    {
        private IDoctorManager _doctorManager;
        private IPatientManager _patientManager;
        private MainWindow _mainWindow;
        public LandingControl(MainWindow mainWindow, IDoctorManager doctorManager, IPatientManager patientManager)
        {
            InitializeComponent();
            this._doctorManager = doctorManager;
            this._patientManager = patientManager;
            this._mainWindow = mainWindow;
            RoleDropdown.SelectedIndex = 0;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
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

        private void RegistrationToggle_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationToggle.Content.ToString() == "Create account")
            {
                SwitchToRegistrationMode();
            }
            else
            {
                SwitchToLoginMode();
            }
        }

        private void RoleDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool registrationModeActive = RegistrationToggle.IsChecked ?? false;
            ComboBoxItem selectedRoleOption = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            string previousRole = RoleDropdown.Text;
            string selectedRole = (string)selectedRoleOption.Content;

            if (registrationModeActive)
            {
                ToggleRegistrationFields(previousRole, false);
                ToggleRegistrationFields(selectedRole, true);
            }
            
        }

        private void SwitchToRegistrationMode()
        {
            RegistrationToggle.Content = "Switch to Login";
            SignInTitle.Content = "Create account as a:";
            RoleDropdown.Margin = new Thickness(340, 73, 0, 0);
            SignInTitle.Margin = new Thickness(120, 65, 0, 0);
            SignInButton.Content = "Create account";
            ToggleRegistrationFields(RoleDropdown.Text, true);
        }

        private void SwitchToLoginMode()
        {
            RegistrationToggle.Content = "Create account";
            SignInTitle.Content = "Log in as a: ";
            SignInTitle.Margin = new Thickness(180, 65, 0, 0);
            RoleDropdown.Margin = new Thickness(320, 73, 0, 0);
            SignInButton.Content = "Login";
            ToggleRegistrationFields(RoleDropdown.Text, false);
        }


        private void ToggleRegistrationFields(string role, bool showFields)
        {
            if (role == "Doctor")
            {

                SpecializationLabel.Visibility = showFields ? Visibility.Visible : Visibility.Hidden;
                SpecializationBox.Visibility = showFields ? Visibility.Visible : Visibility.Hidden;
            }

            if (role == "Patient")
            {
                AgeLabel.Visibility = showFields ? Visibility.Visible : Visibility.Hidden;
                AgeBox.Visibility = showFields ? Visibility.Visible : Visibility.Hidden;

                AddressLabel.Visibility = showFields ? Visibility.Hidden : Visibility.Visible;
                AddressBox.Visibility = showFields ? Visibility.Hidden : Visibility.Visible;
            }
        }
    }


}
