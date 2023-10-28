using BL.Managers;
using BL.Managers.Interfaces;
using Les2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Presentation
{
    public class ConsoleInterface
    {
        internal readonly IMedicationManager _medicationManager;
        internal readonly IGenericManager<Prescription> _prescriptionManager;
        internal readonly IPatientManager _patientManager;
        internal readonly IDoctorManager _doctorManager;

        public ConsoleInterface(IMedicationManager medicationManager, IGenericManager<Prescription> prescriptionManager, IPatientManager patientManager, IDoctorManager doctorManager)
        {
            this._medicationManager = medicationManager ?? throw new ArgumentNullException(nameof(medicationManager));
            this._prescriptionManager = prescriptionManager ?? throw new ArgumentNullException(nameof(prescriptionManager));
            this._patientManager = patientManager ?? throw new ArgumentNullException(nameof(patientManager));
            this._doctorManager = doctorManager ?? throw new ArgumentNullException(nameof(doctorManager));
        }

        private static T AddEntity<T>(Func<T> createEntity, Action<T> addEntityAction) where T : class
        {
            T newEntity = createEntity();

            List<Type> propertyTypes = typeof(T).GetProperties().Select(p => p.PropertyType).ToList();

            var scalarProperties = typeof(T)
            .GetProperties()
            .Where(property => property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
            .ToList();

            foreach (var property in scalarProperties)
            {
                if (property.Name != "Id") // Exclude Id property if needed
                {
                    Console.Write($"Enter {property.Name}: ");
                    var value = Console.ReadLine();
                    if (property.PropertyType == typeof(int))
                    {
                        if (int.TryParse(value, out int intValue))
                        {
                            property.SetValue(newEntity, intValue);
                        }
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        if (float.TryParse(value, out float floatValue))
                        {
                            property.SetValue(newEntity, floatValue);
                        }
                    }
                    else
                    {
                        property.SetValue(newEntity, value);
                    }
                }
            }

            addEntityAction(newEntity);
            Console.WriteLine("Entity added successfully.");
            return newEntity;
        }

        public Medication AddMedication()
        {
            return AddEntity<Medication>(() => new Medication(), (medication) => _medicationManager.Add(medication));
        }

        public Patient AddPatient()
        {
            return AddEntity<Patient>(() => new Patient(), (patient) => _patientManager.Add(patient));
        }
        public Doctor AddDoctor()
        {
            return AddEntity<Doctor>(() => new Doctor(), (doctor) => _doctorManager.Add(doctor));
        }

        public Prescription AddPrescription()
        {

            Console.Write($"Select a doctor: ");
            Console.Write($"Doctor first name: ");
            string doctorFirstName = Console.ReadLine();

            Console.Write($"Doctor last name: ");
            string doctorLastName = Console.ReadLine();

            Doctor doctor = _doctorManager.GetByName(doctorFirstName, doctorLastName);

            Console.Write($"Enter patient first name: ");
            String patientFirstName = Console.ReadLine();

            Console.Write($"Enter patient last name: ");
            String patientLastName = Console.ReadLine();

            Patient patient = _patientManager.GetByName(patientFirstName, patientLastName);

            Console.Write($"Select a medication: ");
            Console.Write($"Trade name: ");
            string medicationTradeName = Console.ReadLine();

            Console.Write($"Dosage: ");
            string? medicationDosage = Console.ReadLine();

            Medication medication = _medicationManager.GetByTradeNameAndDosage(medicationTradeName, medicationDosage);

            Prescription prescription = new Prescription
            {
                PatientID = patient.Id,
                Patient = patient,
                DoctorID = doctor.Id,
                Doctor = doctor,
                PrescriptionDate = DateTime.Now

            };
            prescription.Medications.Add(medication);

            _prescriptionManager.Add(prescription);

            return prescription;

        }



    }
}
