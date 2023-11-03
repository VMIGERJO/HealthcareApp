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
    /// Interaction logic for DoctorLandingPage.xaml
    /// </summary>
    public partial class DoctorLandingPage : UserControl
    {
        private Doctor _doctor;
        public DoctorLandingPage(Doctor doctor)
        {
            InitializeComponent();
            this._doctor = doctor;
            TitleTextBlock.Text = $"Welcome Dr. {doctor.FirstName} {doctor.LastName}";
            SpecializationTextBox.Text = $"{doctor.Specialization}";
        }

        private void CreatePrescriptionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
