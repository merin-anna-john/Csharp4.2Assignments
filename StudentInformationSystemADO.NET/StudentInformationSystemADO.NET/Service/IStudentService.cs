using StudentInformationSystemADO.NET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystemADO.NET.Service
{
    public interface IStudentService
    {
        //Insert
        Task AddStudentAsync(Student student);

        //Update 
        Task UpdateStudentAsync(Student student);

        //Search
        Task<Student> GetStudentByCodeAsync(string studentCode);

        //List all Students
        Task<List<Student>> AllStudentsAsync();

        //Delete Student
        Task DeleteStudentAsync(string studentCode);

    }
}
