using HealthcareApp.Controller;
using HealthcareApp.Presentation;
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
            GenericRepository<Doctor> doctorRepository = new GenericRepository<Doctor>(healthcareDbContext);
            GenericRepository<Patient> patientRepository = new GenericRepository<Patient>(healthcareDbContext);
            GenericRepository<Medication> medicationRepository = new GenericRepository<Medication>(healthcareDbContext);
            GenericRepository<Prescription> prescriptionRepository = new GenericRepository<Prescription>(healthcareDbContext);

            // Initialize DomainController
            DomainController controller = new DomainController(doctorRepository, medicationRepository, patientRepository, prescriptionRepository);

            // Initialize Presentation class
            ConsoleInterface consoleInterface = new ConsoleInterface(controller);


            // Try to add new medication
            consoleInterface.AddMedication();
            consoleInterface.AddDoctor();
            consoleInterface.AddPatient();



        }
    }
}
