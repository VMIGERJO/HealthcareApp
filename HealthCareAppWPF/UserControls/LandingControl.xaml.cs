using BL.DTO;
using BL.Managers.Interfaces;
using EFDal.Entities;
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
            bool registrationModeActive = RegistrationToggle.IsChecked ?? false;
            ComboBoxItem selectedRole = RoleDropdown.SelectedItem as ComboBoxItem;

            if (selectedRole != null)
            {
                string role = selectedRole.Content.ToString();

                if (registrationModeActive)
                {
                    HandleRegistration(role);
                }
                else
                {
                    HandleLogin(role);
                }
            }
        }

        private void HandleRegistration(string role)
        {
            switch (role)
            {
                case "Patient":
                    HandlePatientRegistration();
                    break;
                case "Doctor":
                    HandleDoctorRegistration();
                    break;
            }
        }

        private void HandlePatientRegistration()
        {
            string firstName = DoctorLoginFirstNameBox.Text;
            string lastName = DoctorLoginLastNameBox.Text;
            string address = AddressBox.Text;
            int age;

            // Validate and parse age input
            if (!int.TryParse(AgeBox.Text, out age))
            {
                // Handle invalid age input, e.g., display an error message
                MessageBox.Show("Invalid age input. Please enter a valid number.");
                return;
            }

            Patient newPatient = new Patient
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Age = age
            };

            bool registrationSuccess = _patientManager.Add(newPatient);

            if (registrationSuccess)
            {
                // Display a success message
                MessageBox.Show("Patient registration successful!");
            }
            else
            {
                // Handle registration failure by displaying an error message
                MessageBox.Show("Patient registration failed. Please try again.");
            }
        }

        private void HandleDoctorRegistration()
        {
            string firstName = DoctorLoginFirstNameBox.Text;
            string lastName = DoctorLoginLastNameBox.Text;
            string specialization = SpecializationBox.Text;
            

            Doctor newDoctor = new Doctor
            {
                FirstName = firstName,
                LastName = lastName,
                Specialization = specialization,
            };

            bool registrationSuccess = _doctorManager.Add(newDoctor);

            if (registrationSuccess)
            {
                // Display a success message
                MessageBox.Show("Doctor registration successful!");
            }
            else
            {
                // Handle registration failure by displaying an error message
                MessageBox.Show("Doctor registration failed. Please try again.");
            }
        }

        private void HandleLogin(string role)
        {
            switch (role)
            {
                case "Patient":
                    HandlePatientLogin();
                    break;
                case "Doctor":
                    HandleDoctorLogin();
                    break;
            }
        }


        private void HandlePatientLogin()
        {
            PatientSearchValuesDTO patientQuery = new();
            patientQuery.FirstName = DoctorLoginFirstNameBox.Text;
            patientQuery.LastName = DoctorLoginLastNameBox.Text;
            Patient loggedInPatient = _patientManager.UniquePatientSearch(patientQuery);
            IPrescriptionManager prescriptionManager = App.ServiceProvider.GetService<IPrescriptionManager>();
            PatientLandingControl patientLandingControl = new(_patientManager, prescriptionManager, loggedInPatient, _mainWindow);
            _mainWindow.NavigateToView(patientLandingControl);
        }

        private void HandleDoctorLogin()
        {
            DoctorSearchValuesDTO doctorQuery = new();
            doctorQuery.FirstName = DoctorLoginFirstNameBox.Text;
            doctorQuery.LastName = DoctorLoginLastNameBox.Text;
            Doctor loggedInDoctor = _doctorManager.UniqueDoctorSearch(doctorQuery);
            DoctorLandingControl doctorLandingControl = new(_mainWindow, loggedInDoctor);
            _mainWindow.NavigateToView(doctorLandingControl);
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

                AddressLabel.Visibility = showFields ? Visibility.Visible : Visibility.Hidden;
                AddressBox.Visibility = showFields ? Visibility.Visible : Visibility.Hidden;
            }
        }
    }


}
