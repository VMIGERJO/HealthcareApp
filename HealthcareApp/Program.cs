using BL.Managers;
using BL.Managers.Interfaces;
using EFDal.Repositories;
using EFDal.Repositories.Interfaces;
using HealthcareApp.Presentation;
using Les2.Data;
using Les2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Les2
//{
//    public class Program
//    {
//        static void Main()
//        {
//            // Initialize DbContext
//            HealthcareDbContext healthcareDbContext = new HealthcareDbContext();

//            // Initialize Repos
//            IDoctorRepository doctorRepository = new DoctorRepository(healthcareDbContext);
//            IPatientRepository patientRepository = new PatientRepository(healthcareDbContext);
//            IMedicationRepository medicationRepository = new MedicationRepository(healthcareDbContext);
//            GenericRepository<Prescription> prescriptionRepository = new GenericRepository<Prescription>(healthcareDbContext);

//            // Initialize Managers
//            IDoctorManager doctorManager = new DoctorManager(doctorRepository);
//            IPatientManager patientManager = new PatientManager(patientRepository);
//            IMedicationManager medicationManager = new MedicationManager(medicationRepository);
//            IGenericManager<Prescription> prescriptionManager = new GenericManager<Prescription>(prescriptionRepository);

//            // Initialize Presentation class
//            ConsoleInterface consoleInterface = new ConsoleInterface(medicationManager, prescriptionManager, patientManager, doctorManager);


//            // Try to add new medication
//            consoleInterface.AddPrescription();



//        }
//    }
//}
