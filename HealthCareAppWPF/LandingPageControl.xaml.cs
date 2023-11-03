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
    /// Interaction logic for LandingPageControl.xaml
    /// </summary>
    public partial class LandingPageControl : UserControl
    {
        private MainWindow _mainWindow;
        public LandingPageControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
        }

        private void PatientButton_Click(object sender, RoutedEventArgs e)
        {
            PatientSearchControl patientSearchControl = App.ServiceProvider.GetService<PatientSearchControl>();
            _mainWindow.NavigateToView(patientSearchControl);

        }

        private void DoctorButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorLoginControl doctorLoginControl = App.ServiceProvider.GetService<DoctorLoginControl>();
            _mainWindow.NavigateToView(doctorLoginControl);
        }
    }
}
