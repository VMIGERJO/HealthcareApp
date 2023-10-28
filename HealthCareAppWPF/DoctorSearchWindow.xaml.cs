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

namespace HealthCareAppWPF {
public partial class DoctorSearchWindow : Window
{
    public DoctorSearchWindow()
    {
        InitializeComponent();

        // Populate the ListView with doctor data (you can fetch this from your database).
        //List<DoctorBasicDTO> doctors = GetAllDoctorBasicDTO(); // Replace with your data retrieval logic.
        //DoctorListView.ItemsSource = doctors;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        // Close the DoctorSearchWindow and return to the Main Window.
        this.Close();
    }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve first name and last name from textboxes.
            string firstName = FirstNameBox.Text;
            string lastName = LastNameBox.Text;

            // Call a method to retrieve doctors based on the entered first name and last name.
            //List<DoctorBasicDTO> matchingDoctors = GetDoctorsByFirstNameLastName(firstName, lastName);

            // Populate the ListView with matching doctors.
            //DoctorListView.ItemsSource = matchingDoctors;
        }
    }

}