using StudentInformationSystemADO.NET.Model;
using StudentInformationSystemADO.NET.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystemADO.NET.Service
{
    public class StudentServiceImplementation : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        //Constructor Injection
        public StudentServiceImplementation(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        //Add student
        public async Task AddStudentAsync(Student student)
        {
            try
            {
                await _studentRepository.AddStudentAsync(student);
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

        //List all students
        public async Task<List<Student>> AllStudentsAsync()
        {
            try
            {
                return await _studentRepository.AllStudentsAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return new List<Student>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Student>();
            }
        }

        //Delete Students
        public async Task DeleteStudentAsync(string studentCode)
        {
            try
            {
                await _studentRepository.DeleteStudentAsync(studentCode);
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

        //Get Student By Code
        public async Task<Student> GetStudentByCodeAsync(string studentCode)
        {
            try
            {
                return await _studentRepository.GetStudentByCodeAsync(studentCode);
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

        //Update Student
        public async Task UpdateStudentAsync(Student student)
        {
            try
            {
                await _studentRepository.UpdateStudentAsync(student);
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
