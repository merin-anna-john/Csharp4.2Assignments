using SqlServerConnectionLibrary;
using StudentInformationSystemADO.NET.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystemADO.NET.Repository
{
    public class StudentRepositoryImplementation : IStudentRepository
    {
        // Retrieve Connection String from App.Config
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;


        // Insert
        public async Task AddStudentAsync(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "INSERT INTO Students (StudentCode, StudentName, Course, EnrollmentDate, GPA) " +
                        "VALUES(@StudCode, @StudName, @Course, @EnrollDate, @gpa)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@StudCode", student.StudentCode);
                        command.Parameters.AddWithValue("@StudName", student.StudentName);
                        command.Parameters.AddWithValue("@Course", student.Course);
                        command.Parameters.AddWithValue("@EnrollDate", student.EnrollmentDate);
                        command.Parameters.AddWithValue("@gpa", student.GPA);

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


        //Retrieve all Students
        public async Task<List<Student>> AllStudentsAsync()
        {
            List<Student> students = new List<Student>();

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT StudentCode, StudentName, Course, EnrollmentDate, GPA FROM Students";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                students.Add(new Student
                                {
                                    StudentCode = reader["StudentCode"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),
                                    Course = reader["Course"].ToString(),
                                    EnrollmentDate = Convert.ToDateTime(reader["EnrollmentDate"]),  // Correctly parse DateTime
                                    GPA = reader["GPA"].ToString()
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

            return students;
        }


        //Delete Student
        public async Task DeleteStudentAsync(string studentCode)
        {
            if (string.IsNullOrEmpty(studentCode)) throw new ArgumentException("Invalid health code", nameof(studentCode));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "DELETE FROM Students WHERE StudentCode = @StudCode";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@StudCode", studentCode);
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


        //Get Student by Code
        public async Task<Student> GetStudentByCodeAsync(string studentCode)
        {
            if (string.IsNullOrEmpty(studentCode)) throw new ArgumentException("Invalid student code", nameof(studentCode));

            Student student = null;

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    string query = "SELECT StudentCode, StudentName, Course, EnrollmentDate, GPA FROM Students WHERE StudentCode = @StudCode";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@StudCode", studentCode);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                student = new Student
                                {
                                    StudentCode = reader["StudentCode"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),
                                    Course = reader["Course"].ToString(),
                                    EnrollmentDate = Convert.ToDateTime(reader["EnrollmentDate"]),  // Correctly parse DateTime
                                    GPA = reader["GPA"].ToString()
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

            return student;
        }


        //Update Student
        public async Task UpdateStudentAsync(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));

            try
            {
                using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(_connectionString))
                {
                    // Retrieve existing student data
                    Student existingStudent = null;
                    string query = "SELECT StudentCode, StudentName, Course, EnrollmentDate, GPA FROM Students WHERE StudentCode = @StudCode";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@StudCode", student.StudentCode);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                existingStudent = new Student
                                {
                                    StudentCode = reader["StudentCode"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),
                                    Course = reader["Course"].ToString(),
                                    EnrollmentDate = (DateTime)reader["EnrollmentDate"], // Correctly casting to DateTime
                                    GPA = reader["GPA"].ToString()
                                };
                            }
                        }
                    }

                    if (existingStudent == null)
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }

                    // Use existing values if no new input is provided
                    string newStudentName = string.IsNullOrWhiteSpace(student.StudentName) ? existingStudent.StudentName : student.StudentName;
                    string newCourse = string.IsNullOrWhiteSpace(student.Course) ? existingStudent.Course : student.Course;
                    DateTime newEnrollmentDate = student.EnrollmentDate == default(DateTime) ? existingStudent.EnrollmentDate : student.EnrollmentDate;
                    string newGPA = string.IsNullOrWhiteSpace(student.GPA) ? existingStudent.GPA : student.GPA;

                    // Update student data
                    string updateQuery = "UPDATE Students SET StudentName = @StudName, Course = @Course, EnrollmentDate = @EnrollDate, GPA = @gpa WHERE StudentCode = @StudCode";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                    {
                        updateCommand.Parameters.AddWithValue("@StudCode", student.StudentCode);
                        updateCommand.Parameters.AddWithValue("@StudName", newStudentName);
                        updateCommand.Parameters.AddWithValue("@Course", newCourse);
                        updateCommand.Parameters.AddWithValue("@EnrollDate", newEnrollmentDate);
                        updateCommand.Parameters.AddWithValue("@gpa", newGPA);

                        int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                        
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update student: {ex.Message}");
            }
        }
    }
}
