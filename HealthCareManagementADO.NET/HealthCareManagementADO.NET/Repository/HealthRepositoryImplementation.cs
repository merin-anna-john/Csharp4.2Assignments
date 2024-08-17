using HealthCareManagementADO.NET.Model;
using SqlServerConnectionLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HealthCareManagementADO.NET.Repository
{
    public class HealthRepositoryImplementation : IHealthRepository
    {
        // Retrieve Connection String from App.Config
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

        // Insert
        public async Task AddPatientAsync(Health patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "INSERT INTO Patients (PatientCode, PatientName, PatientAge, Diagnosis, TreatmentPlan) " +
                        "VALUES(@PatCode, @PatName, @PatAge, @Diag, @TrPl)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@PatCode", patient.PatientCode);
                        command.Parameters.AddWithValue("@PatName", patient.PatientName);
                        command.Parameters.AddWithValue("@PatAge", patient.PatientAge);
                        command.Parameters.AddWithValue("@Diag", patient.Diagnosis);
                        command.Parameters.AddWithValue("@TrPl", patient.TreatmentPlan);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Retrieve All Patients
        public async Task<List<Health>> AllPatientsAsync()
        {
            List<Health> patients = new List<Health>();

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT PatientCode, PatientName, PatientAge, Diagnosis, TreatmentPlan FROM Patients";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                patients.Add(new Health
                                {
                                    PatientCode = reader["PatientCode"].ToString(),
                                    PatientName = reader["PatientName"].ToString(),
                                    PatientAge = reader["PatientAge"].ToString(),
                                    Diagnosis = reader["Diagnosis"].ToString(),
                                    TreatmentPlan = reader["TreatmentPlan"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return patients;
        }

        // Delete Patient
        public async Task DeleteHealthAsync(string healthCode)
        {
            if (string.IsNullOrEmpty(healthCode)) throw new ArgumentException("Invalid health code", nameof(healthCode));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "DELETE FROM Patients WHERE PatientCode = @PatCode";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@PatCode", healthCode);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Get Patient by Code
        public async Task<Health> GetPatientByCodeAsync(string healthCode)
        {
            if (string.IsNullOrEmpty(healthCode)) throw new ArgumentException("Invalid health code", nameof(healthCode));

            Health patient = null;

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT PatientCode, PatientName, PatientAge, Diagnosis, TreatmentPlan FROM Patients WHERE PatientCode = @PatCode";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@PatCode", healthCode);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                patient = new Health
                                {
                                    PatientCode = reader["PatientCode"].ToString(),
                                    PatientName = reader["PatientName"].ToString(),
                                    PatientAge = reader["PatientAge"].ToString(),
                                    Diagnosis = reader["Diagnosis"].ToString(),
                                    TreatmentPlan = reader["TreatmentPlan"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return patient;
        }

        // Update Patient
        public async Task UpdatePatientAsync(Health patient)
        {
            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    

                    // Retrieve existing patient data
                    Health existingPatient = null;
                    string query = "SELECT PatientName, PatientAge, Diagnosis, TreatmentPlan FROM Patients WHERE PatientCode = @PatCode";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@PatCode", patient.PatientCode);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                existingPatient = new Health
                                {
                                    PatientName = reader["PatientName"].ToString(),
                                    PatientAge = reader["PatientAge"].ToString(),
                                    Diagnosis = reader["Diagnosis"].ToString(),
                                    TreatmentPlan = reader["TreatmentPlan"].ToString()
                                };
                            }
                        }
                    }

                    if (existingPatient == null)
                    {
                        Console.WriteLine("Patient not found.");
                        return;
                    }

                    // Use existing values if no new input is provided
                    string newPatientName = string.IsNullOrWhiteSpace(patient.PatientName) ? existingPatient.PatientName : patient.PatientName;
                    string newPatientAge = string.IsNullOrWhiteSpace(patient.PatientAge) ? existingPatient.PatientAge : patient.PatientAge;
                    string newDiagnosis = string.IsNullOrWhiteSpace(patient.Diagnosis) ? existingPatient.Diagnosis : patient.Diagnosis;
                    string newTreatmentPlan = string.IsNullOrWhiteSpace(patient.TreatmentPlan) ? existingPatient.TreatmentPlan : patient.TreatmentPlan;

                    // Update patient data
                    string newQuery = "UPDATE Patients SET PatientName = @PatName, PatientAge = @PatAge, Diagnosis = @Diagnosis, TreatmentPlan = @TreatmentPlan WHERE PatientCode = @PatCode";
                    using (SqlCommand updateCommand = new SqlCommand(newQuery, conn))
                    {
                        updateCommand.Parameters.AddWithValue("@PatCode", patient.PatientCode);
                        updateCommand.Parameters.AddWithValue("@PatName", newPatientName);
                        updateCommand.Parameters.AddWithValue("@PatAge", newPatientAge);
                        updateCommand.Parameters.AddWithValue("@Diagnosis", newDiagnosis ?? (object)DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@TreatmentPlan", newTreatmentPlan ?? (object)DBNull.Value);

                        int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Patient updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Patient not found.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update patient: {ex.Message}");
            }
        }

    }
}
