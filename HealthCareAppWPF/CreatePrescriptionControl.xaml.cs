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
    /// Interaction logic for CreatePrescriptionControl.xaml
    /// </summary>
    public partial class CreatePrescriptionControl : UserControl
    {
        private Patient _patient;
        private IMedicationManager _medicationManager;
        public CreatePrescriptionControl(Patient patient, IMedicationManager medicationManager)
        {
            InitializeComponent();
            this._patient = patient;
            this._medicationManager = medicationManager;
            TitleTextBlock.Text = $"{DateTime.Now:yyyy-MM-dd} - {patient.FirstName} {patient.LastName}";
        }


    }
}
