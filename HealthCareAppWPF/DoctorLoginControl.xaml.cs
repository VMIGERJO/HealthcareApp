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
    public partial class DoctorLoginControl : UserControl
    {
        private IDoctorManager _doctorManager;
        private MainWindow _mainWindow;
        public DoctorLoginControl(MainWindow mainWindow, IDoctorManager doctorManager)
        {
            InitializeComponent();
            this._doctorManager = doctorManager;
            this._mainWindow = mainWindow;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorSearchValuesDTO doctorQuery = new();
            doctorQuery.FirstName = DoctorLoginFirstNameBox.Text;
            doctorQuery.LastName = DoctorLoginLastNameBox.Text;
            Doctor loggedInDoctor = _doctorManager.UniqueDoctorSearch(doctorQuery);
            DoctorLandingPage doctorLandingPage = new(loggedInDoctor);
            _mainWindow.NavigateToView(doctorLandingPage);
        }
    }
}
