using BL.DTO;
using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Exceptions;
using HealthCareAppWPF.UserControls;
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
            string firstName = LoginFirstNameBox.Text;
            string lastName = LoginLastNameBox.Text;
            int age;

            // Validate and parse age input
            if (!int.TryParse(AgeBox.Text, out age))
            {
                // Handle invalid age input, e.g., display an error message
                MessageBox.Show("Invalid age input. Please enter a valid number.");
                return;
            }

            AddressDTO address = new()
            {
                Street = StreetBox.Text,
                HouseNumber = HouseNumberBox.Text,
                Appartment = AppartmentBox.Text,
                City = CityBox.Text,
                PostalCode = PostalCodeBox.Text,
                Country = CountryBox.Text
            };

            PatientDTO newPatient = new PatientDTO
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Age = age
            };

            bool registrationSuccess = false;
            try
            {
                registrationSuccess = _patientManager.Add(newPatient);
            }
            catch (ArgumentException invalidArgEx)
            {

                MessageBox.Show(invalidArgEx.Message);
                return;
            } 


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
            string firstName = LoginFirstNameBox.Text;
            string lastName = LoginLastNameBox.Text;
            string specialization = SpecializationBox.Text;


            DoctorDTO newDoctor = new DoctorDTO
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
                case "Health Agency":
                    HandleHealthAgencyLogin();
                    break;
            }
        }

        private void HandleHealthAgencyLogin()
        {
            HealthAgencyDashboardControl healthAgencyDashboardControl = App.ServiceProvider.GetService<HealthAgencyDashboardControl>();
            _mainWindow.NavigateToView(healthAgencyDashboardControl);
        }

        private async Task HandlePatientLogin()
        {
            PatientSearchValuesDTO patientQuery = new();
            patientQuery.FirstName = LoginFirstNameBox.Text.Trim();
            patientQuery.LastName = LoginLastNameBox.Text.Trim();
            try
            {
                PatientDTO loggedInPatient = await _patientManager.SearchPatientWithAdressAsync(patientQuery);
                IPrescriptionManager prescriptionManager = App.ServiceProvider.GetService<IPrescriptionManager>();
                IPatientManager patientManager = App.ServiceProvider.GetService<IPatientManager>();
                PatientLandingControl patientLandingControl = new(patientManager, prescriptionManager, loggedInPatient.Id, _mainWindow);
                _mainWindow.NavigateToView(patientLandingControl);
            }
            catch (NoResultsFoundException noResultsEx)
            {

                MessageBox.Show($"This Patient was not found, please create an account first.", "No results found", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (NonUniqueQueryException)
            {
                MessageBox.Show($"Please review your input, too many results were found", "Too many results", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw ex; // or log, rethrow, etc.
            }
        }

        private async Task HandleDoctorLogin()
        {
            DoctorSearchValuesDTO doctorQuery = new();
            doctorQuery.FirstName = LoginFirstNameBox.Text;
            doctorQuery.LastName = LoginLastNameBox.Text;
            DoctorDTO loggedInDoctor = await _doctorManager.UniqueDoctorSearchAsync(doctorQuery);
            IDoctorManager doctorManager = App.ServiceProvider.GetService<IDoctorManager>();
            DoctorLandingControl doctorLandingControl = new(_mainWindow, doctorManager, loggedInDoctor);
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
                ToggleFields("registration", false, previousRole);
                ToggleFields("registration", true, selectedRole);
            }
            else
            {
                if (selectedRole == "Health Agency")
                {
                    ToggleFields("login", false);
                }
                else
                {
                    ToggleFields("login", true);
                }
            }
        }

        private void SwitchToRegistrationMode()
        {
            RegistrationToggle.Content = "Switch to Login";
            SignInTitle.Content = "Create account as a:";
            RoleDropdown.Margin = new Thickness(340, 73, 0, 0);
            HealthAgencyOption.Visibility = Visibility.Collapsed;
            SignInTitle.Margin = new Thickness(120, 65, 0, 0);
            SignInButton.Content = "Create account";
            ToggleFields("registration", true, RoleDropdown.Text);
        }

        private void SwitchToLoginMode()
        {
            RegistrationToggle.Content = "Create account";
            SignInTitle.Content = "Log in as a: ";
            SignInTitle.Margin = new Thickness(180, 65, 0, 0);
            RoleDropdown.Margin = new Thickness(320, 73, 0, 0);
            HealthAgencyOption.Visibility = Visibility.Visible;
            SignInButton.Content = "Login";
            ToggleFields("registration", false, RoleDropdown.Text);
        }

        private void ToggleFields(string fieldType, bool showFields, string role = "")
        {
            Visibility visibility = showFields ? Visibility.Visible : Visibility.Collapsed;

            switch (fieldType)
            {
                case "login":
                    SetVisibility(visibility, RegistrationToggle, LoginFirstNameBox, LoginLastNameBox, LoginFirstNameLabel, LoginLastNameLabel);
                    break;

                case "registration":
                    if (role == "Doctor")
                    {
                        SetVisibility(visibility, SpecializationLabel, SpecializationBox);

                    } else if (role == "Patient")
                    {
                        SetVisibility(visibility, AgeLabel, AgeBox, AddressFields);
                    }
                    break;
            }
        }

        private void SetVisibility(Visibility visibility, params UIElement[] elements)
        {
            foreach (var element in elements)
            {
                element.Visibility = visibility;
            }
        }



    }
}
