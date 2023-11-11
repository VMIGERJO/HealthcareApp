using BL.DTO;
using BL.Managers;
using BL.Managers.Interfaces;
using EFDal.Entities;
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
    /// Interaction logic for DoctorSearchControl.xaml
    /// </summary>
    public partial class DoctorSearchControl : UserControl
    {
        private MainWindow _mainWindow;
        private IDoctorManager _doctorManager;
        private List<DoctorBasicDTO> allDoctors;

        public DoctorSearchControl(MainWindow mainWindow, IDoctorManager doctorManager)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
            this._doctorManager = doctorManager;
            LoadDoctorsAsync();
        }

        private async Task LoadDoctorsAsync()
        {
            allDoctors = await _doctorManager.GetAllDoctorsAsync();
            DoctorListView.ItemsSource = allDoctors;
        }

        private void DoctorSearchButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorSearchValuesDTO doctorQuery = new();
            doctorQuery.FirstName = DoctorFirstNameBox.Text;
            doctorQuery.LastName = DoctorLastNameBox.Text;
            List<DoctorBasicDTO> matchingDoctors = _doctorManager.DoctorSearch(doctorQuery);
            DoctorListView.ItemsSource = matchingDoctors;
        }
    }
}
