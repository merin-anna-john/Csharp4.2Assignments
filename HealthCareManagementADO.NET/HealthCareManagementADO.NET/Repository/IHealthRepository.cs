using HealthCareManagementADO.NET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementADO.NET.Repository
{
    public interface IHealthRepository
    {
        //Insert
        Task AddPatientAsync(Health patient);

        //Update 
        Task UpdatePatientAsync(Health patient);

        //Search
        Task<Health> GetPatientByCodeAsync(string healthCode);

        //List all Patients
        Task<List<Health>> AllPatientsAsync();

        //Delete Health
        Task DeleteHealthAsync(string healthCode);
    }
}
