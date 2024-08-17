using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareManagementADO.NET.Model
{
    public class Health
    {
        //Auto-generated fields
        private int Id;

        //Properties
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string PatientAge { get; set; }
        public string Diagnosis { get; set; }
        public string TreatmentPlan { get; set; }

        //Default Constructor
        public Health()
        {
            
        }

        //Parameterized Constructor
        public Health(string patientCode, string patientName, string patientAge, string diagnosis, string treatmentPlan)
        {
            this.PatientName = patientName;
            this.PatientCode = patientCode;
            this.PatientAge = patientAge;
            this.Diagnosis = diagnosis;
            this.TreatmentPlan=treatmentPlan;
        }
    }
}
