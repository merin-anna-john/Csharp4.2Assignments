using HealthCareManagementADO.NET.Model;
using HealthCareManagementADO.NET.Repository;
using HealthCareManagementADO.NET.Service;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HealthCareManagementADO.NET
{
    internal class HealthApp
    {
        static async Task Main(string[] args)
        {
            // Create an instance of Service
            IHealthService patientService = new HealthServiceImplementation(new HealthRepositoryImplementation());

            // Menu Driven
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("HealthCare Management System");
                Console.WriteLine("1. Add Patients");
                Console.WriteLine("2. Update Patients");
                Console.WriteLine("3. Search Patient by Code");
                Console.WriteLine("4. List All Patients");
                Console.WriteLine("5. Delete Patient");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await AddPatient(patientService);
                        break;
                    case "2":
                        await UpdatePatient(patientService);
                        break;
                    case "3":
                        await ViewPatientByCode(patientService);
                        break;
                    case "4":
                        await ViewAllPatients(patientService);
                        break;
                    case "5":
                        await DeletePatient(patientService);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong choice! Please enter again...");
                        break;
                }
            }
            Console.ReadKey();
        }

        #region AddPatient
        private static async Task AddPatient(IHealthService patientService)
        {
            Health patient = new Health();

            //Patient Code
            while (true)
            {
                Console.WriteLine("Enter Patient Code:");
                patient.PatientCode = Console.ReadLine();

                // Validate Patient Code
                if (string.IsNullOrWhiteSpace(patient.PatientCode))
                {
                    Console.WriteLine("Invalid input for Patient Code. Please enter a valid numeric code.");
                }
                else
                {
                    break;
                }
            }

            //Patient Name
            while (true)
            {
                Console.WriteLine("Enter Patient Name:");
                patient.PatientName = Console.ReadLine();

                // Validation for Patient Name
                if (!string.IsNullOrWhiteSpace(patient.PatientName) && Regex.IsMatch(patient.PatientName, @"^[a-zA-Z\s]+$"))
                {
                    break; // Valid input, exit loop
                }
                else
                {
                    Console.WriteLine("Invalid input for Patient Name. Please ensure the name contains only letters and spaces.");
                }
            }

            //Patient Age
            while (true)
            {
                Console.WriteLine("Enter Patient Age:");
                patient.PatientAge = Console.ReadLine();

                // Validate Patient Age
                if (string.IsNullOrWhiteSpace(patient.PatientAge) || !int.TryParse(patient.PatientAge, out _))
                {
                    Console.WriteLine("Invalid input for Patient Age. Please enter a valid number.");
                }
                else
                {
                    break; // Valid input, exit loop
                }
            }

            //Diagnosis
            Console.WriteLine("Enter Diagnosis:");
            patient.Diagnosis = Console.ReadLine();

            //Treatment Plan
            Console.WriteLine("Enter Treatment Plan:");
            patient.TreatmentPlan = Console.ReadLine();

            try
            {
                await patientService.AddPatientAsync(patient);
                Console.WriteLine("Patient added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add patient: {ex.Message}");
            }
        }
        #endregion


        #region UpdatePatient
        private static async Task UpdatePatient(IHealthService patientService)
        {
            Health patient = new Health();

            // Ensure PatientCode is provided
             Console.WriteLine("Enter Patient Code:");
                patient.PatientCode = Console.ReadLine();

                if (string.IsNullOrEmpty(patient.PatientCode))
                {
                    Console.WriteLine("Patient Code is required.");
                }

                else
                {
                    //Patient name
                    while (true)
                    {
                        Console.WriteLine("Enter Patient Name (leave empty to keep current):");
                        patient.PatientName = Console.ReadLine();

                        // If the input is empty, keep the current value
                        if (string.IsNullOrWhiteSpace(patient.PatientName))
                        {
                            break; // Exit loop without changing the current name
                        }

                        // Validation for Patient Name
                        if (Regex.IsMatch(patient.PatientName, @"^[a-zA-Z\s]+$"))
                        {
                            patient.PatientName = patient.PatientName; // Update the patient name
                            break; // Valid input, exit loop
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for Patient Name. Please ensure the name contains only letters and spaces.");
                        }
                    }

                    //Patient Age
                    while (true)
                    {
                        Console.WriteLine("Enter Patient Age (leave empty to keep current):");
                        patient.PatientAge = Console.ReadLine();

                        // Validate Patient Age
                        // If the input is empty, keep the current value
                        if (string.IsNullOrWhiteSpace(patient.PatientAge))
                        {
                            break; // Exit loop without changing the current name
                        }

                        // Validation for Patient Name
                        if (!int.TryParse(patient.PatientAge, out _))
                        {
                            Console.WriteLine("Invalid input for Patient Age. Please enter a valid number.");
                        }
                        else
                        {
                            patient.PatientAge = patient.PatientAge; // Update the patient age
                            break; // Valid input, exit loop
                        }


                    }

                    Console.WriteLine("Enter Diagnosis (leave empty to keep current):");
                    patient.Diagnosis = Console.ReadLine();

                    Console.WriteLine("Enter Treatment Plan (leave empty to keep current):");
                    patient.TreatmentPlan = Console.ReadLine();

                    try
                    {
                        await patientService.UpdatePatientAsync(patient);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to update patient: {ex.Message}");
                    }
                }
            
        }

        #endregion

        #region DeletePatient
        private static async Task DeletePatient(IHealthService patientService)
        {
            Console.WriteLine("Enter Patient Code to Delete:");
            string code = Console.ReadLine();

            try
            {
                await patientService.DeleteHealthAsync(code);
                Console.WriteLine("Patient deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete patient: {ex.Message}");
            }
        }
        #endregion

        #region ViewAllPatients
        private static async Task ViewAllPatients(IHealthService patientService)
        {
            try
            {
                List<Health> patients = await patientService.AllPatientsAsync();
                foreach (var patient in patients)
                {
                    Console.WriteLine($"Code: {patient.PatientCode}, Name: {patient.PatientName}, Age: {patient.PatientAge}, Diagnosis: {patient.Diagnosis}, Treatment Plan: {patient.TreatmentPlan}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve patients: {ex.Message}");
            }
        }
        #endregion


        #region ViewPatientByCode
        private static async Task ViewPatientByCode(IHealthService patientService)
        {
            Console.WriteLine("Enter Patient Code to View:");
            string code = Console.ReadLine();

            try
            {
                Health patient = await patientService.GetPatientByCodeAsync(code);
                if (patient != null)
                {
                    Console.WriteLine($"Code: {patient.PatientCode}, Name: {patient.PatientName}, Age: {patient.PatientAge}, Diagnosis: {patient.Diagnosis}, Treatment Plan: {patient.TreatmentPlan}");
                }
                else
                {
                    Console.WriteLine("Patient not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve patient: {ex.Message}");
            }
        }
        #endregion
    }
}
