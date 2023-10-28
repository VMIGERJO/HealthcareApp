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
        public PatientSearchWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameBox.Text;
            string lastName = LastNameBox.Text;

            //List<Patient> matchingPatients = GetPatientsByFirstNameLastName(firstName, lastName);

            //PatientListView.ItemsSource = matchingPatients;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the DoctorSearchWindow and return to the Main Window.
            this.Close();
        }

    }
}
