using HealthCareManagementADO.NET.Model;
using HealthCareManagementADO.NET.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HealthCareManagementADO.NET.Service
{
    public class HealthServiceImplementation : IHealthService
    {
        private readonly IHealthRepository _healthRepository;

        // Constructor injection
        public HealthServiceImplementation(IHealthRepository healthRepository)
        {
            _healthRepository = healthRepository;
        }

        public async Task AddPatientAsync(Health patient)
        {
            try
            {
                await _healthRepository.AddPatientAsync(patient);
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

        public async Task<List<Health>> AllPatientsAsync()
        {
            try
            {
                return await _healthRepository.AllPatientsAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return new List<Health>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Health>();
            }
        }

        public async Task DeleteHealthAsync(string healthCode)
        {
            try
            {
                await _healthRepository.DeleteHealthAsync(healthCode);
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

        public async Task<Health> GetPatientByCodeAsync(string healthCode)
        {
            try
            {
                return await _healthRepository.GetPatientByCodeAsync(healthCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task UpdatePatientAsync(Health patient)
        {
            try
            {
                await _healthRepository.UpdatePatientAsync(patient);
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
    }
}
