using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for PatientLandingControl.xaml
    /// </summary>
    public partial class PatientLandingControl : UserControl
    {
        private Patient _patient;
        public PatientLandingControl(Patient patient)
        {
            InitializeComponent();
            this._patient = patient;
            TitleTextBlock.Text = $"Welcome {patient.FirstName} {patient.LastName}";
        }
    }
}
