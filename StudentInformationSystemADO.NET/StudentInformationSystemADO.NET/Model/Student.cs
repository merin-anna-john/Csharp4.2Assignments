using System;

namespace StudentInformationSystemADO.NET.Model
{
    public class Student
    {
        // Auto-generated fields
        private int id;

        //Properties
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public string Course { get; set; }
        public DateTime EnrollmentDate { get; set; }  // DateTime is better suited for date representation
        public string GPA { get; set; }  // GPA should be of type double

        // Default Constructor
        public Student() 
        {

        }

        // Parameterized Constructor
        public Student(string studentCode,string studentName, string course, DateTime enrollmentDate, string gpa)
        {
            this.StudentCode = studentCode;
            this.StudentName = studentName;
            this.Course = course;
            this.EnrollmentDate = enrollmentDate;
            this.GPA = gpa;
        }


        
    }
}
