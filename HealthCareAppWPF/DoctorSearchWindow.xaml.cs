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

namespace HealthCareAppWPF {
public partial class DoctorSearchWindow : Window
{
    private IDoctorManager _doctorManager;
    public DoctorSearchWindow(IDoctorManager doctorManager)
    {
        InitializeComponent();
        this._doctorManager = doctorManager;
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
            DoctorSearchValuesDTO doctorQuery = new();
            doctorQuery.FirstName = FirstNameBox.Text;
            doctorQuery.LastName = LastNameBox.Text;

            List<DoctorBasicDTO> matchingDoctors = _doctorManager.DoctorSearch(doctorQuery);

            DoctorListView.ItemsSource = matchingDoctors;
        }
    }

}