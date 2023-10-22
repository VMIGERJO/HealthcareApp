using HealthcareApp.Controller;
using HealthcareApp.Presentation;
using HealthcareApp.Repositories;
using Les2.Data;
using Les2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les2
{
    public class Program
    {
        static void Main()
        {
            // Initialize DbContext
            HealthcareDbContext healthcareDbContext = new HealthcareDbContext();

            // Initialize Repos
            DoctorRepository doctorRepository = new DoctorRepository(healthcareDbContext);
            PatientRepository patientRepository = new PatientRepository(healthcareDbContext);
            MedicationRepository medicationRepository = new MedicationRepository(healthcareDbContext);
            GenericRepository<Prescription> prescriptionRepository = new GenericRepository<Prescription>(healthcareDbContext);

            // Initialize DomainController
            DomainController controller = new DomainController(doctorRepository, medicationRepository, patientRepository, prescriptionRepository);

            // Initialize Presentation class
            ConsoleInterface consoleInterface = new ConsoleInterface(controller);


            // Try to add new medication
            consoleInterface.AddPrescription();



        }
    }
}
