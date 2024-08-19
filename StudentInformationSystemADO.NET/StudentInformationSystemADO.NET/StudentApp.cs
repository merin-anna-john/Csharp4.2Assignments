using StudentInformationSystemADO.NET.Model;
using StudentInformationSystemADO.NET.Repository;
using StudentInformationSystemADO.NET.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentInformationSystemADO.NET
{
    internal class StudentApp
    {
        static async Task Main(string[] args)
        {
            // Create an instance of Service
            IStudentService studentService =new StudentServiceImplementation(new StudentRepositoryImplementation());

            // Menu Driven
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Student Information System");
                Console.WriteLine("1. Add Students");
                Console.WriteLine("2. Update Students");
                Console.WriteLine("3. Search Student by Code");
                Console.WriteLine("4. List All Students");
                Console.WriteLine("5. Delete Students");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await AddStudent(studentService);
                        break;
                    case "2":
                        await UpdateStudent(studentService);
                        break;
                    case "3":
                        await ViewStudentByCode(studentService);
                        break;
                    case "4":
                        await ViewAllStudents(studentService);
                        break;
                    case "5":
                        await DeleteStudent(studentService);
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

        #region AddStudent
        private static async Task AddStudent(IStudentService studentService)
        {
            Student student = new Student();

            //Student Code
            while (true)
            {
                Console.WriteLine("Enter Student Code:");
                student.StudentCode = Console.ReadLine();

                // Validate Patient Code
                if (string.IsNullOrWhiteSpace(student.StudentCode))
                {
                    Console.WriteLine("Invalid input for Student Code. Please enter a valid numeric code.");
                }
                else
                {
                    break;
                }
            }

            //Student Name
            while (true)
            {
                Console.WriteLine("Enter Student Name:");
                student.StudentName = Console.ReadLine();

                // Validation for Student Name
                if (!string.IsNullOrWhiteSpace(student.StudentName) && Regex.IsMatch(student.StudentName, @"^[a-zA-Z\s]+$"))
                {
                    break; // Valid input, exit loop
                }
                else
                {
                    Console.WriteLine("Invalid input for Student Name. Please ensure the name contains only letters and spaces.");
                }
            }

            //Course
            while (true)
            {
                Console.WriteLine("Enter Course:");
                student.Course = Console.ReadLine();

                // Validate Course
                if (!string.IsNullOrWhiteSpace(student.Course) && Regex.IsMatch(student.Course, @"^[a-zA-Z\s]+$"))
                {
                    break; // Valid input, exit loop
                }
                else
                {
                    Console.WriteLine("Invalid input for Course. Please ensure it contains only letters and spaces..");
                }
            }

            
            // Enrollment Date
            DateTime enrollmentDate;
            while (true)
            {
                Console.WriteLine("Enter Enrollment Date (yyyy-mm-dd):");
                string date = Console.ReadLine();

                if (DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out enrollmentDate))
                {
                    student.EnrollmentDate = enrollmentDate;
                    break; // Valid date
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format.");
                }
            }


            // GPA
            while (true)
            {
                Console.WriteLine("Enter GPA:");
                string gpaInput = Console.ReadLine();

                if (double.TryParse(gpaInput, out double gpa) && gpa >= 0 && gpa <= 10.0)
                {
                    student.GPA = gpaInput;
                    break; // Valid GPA
                }
                else
                {
                    Console.WriteLine("Invalid GPA. Please enter a value between 0 and 10.0.");
                }
            }

            try
            {
                await studentService.AddStudentAsync(student);
                Console.WriteLine("Student added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add student: {ex.Message}");
            }
        }
        #endregion


        #region UpdateStudent
        private static async Task UpdateStudent(IStudentService studentService)
        {
            Console.WriteLine("Enter Student Code of the student to update:");
            string studentCode = Console.ReadLine();

            // Fetch the existing student details
            Student existingStudent = await studentService.GetStudentByCodeAsync(studentCode);

            if (existingStudent == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            bool isUpdated = false; // Flag to check if any field was updated

            // Update Student Name
            Console.WriteLine("Enter new Student Name (leave empty to keep current value):");
            string studentName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(studentName))
            {
                if (Regex.IsMatch(studentName, @"^[a-zA-Z\s]+$"))
                {
                    existingStudent.StudentName = studentName;
                    isUpdated = true; // Mark as updated
                }
                else
                {
                    Console.WriteLine("Invalid input for Student Name. Please ensure the name contains only letters and spaces.");
                    return;
                }
            }

            // Update Course
            Console.WriteLine("Enter new Course (leave empty to keep current value):");
            string course = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(course))
            {
                if (Regex.IsMatch(course, @"^[a-zA-Z\s]+$"))
                {
                    existingStudent.Course = course;
                    isUpdated = true; // Mark as updated
                }
                else
                {
                    Console.WriteLine("Invalid input for Course. Please ensure it contains only letters and spaces.");
                    return;
                }
            }

            // Update Enrollment Date
            Console.WriteLine("Enter new Enrollment Date (yyyy-mm-dd) (leave empty to keep current value):");
            string enrollmentDateStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(enrollmentDateStr))
            {
                if (DateTime.TryParseExact(enrollmentDateStr, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime enrollmentDate))
                {
                    existingStudent.EnrollmentDate = enrollmentDate;
                    isUpdated = true; // Mark as updated
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format.");
                    return;
                }
            }

            // Update GPA
            Console.WriteLine("Enter new GPA (leave empty to keep current value):");
            string gpaStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(gpaStr))
            {
                if (double.TryParse(gpaStr, out double gpaValue) && gpaValue >= 0 && gpaValue <= 4)
                {
                    existingStudent.GPA = gpaValue.ToString();
                    isUpdated = true; // Mark as updated
                }
                else
                {
                    Console.WriteLine("Invalid input for GPA. Please enter a value between 0 and 4.");
                    return;
                }
            }

            // Update the student only if something was actually updated
            if (isUpdated)
            {
                await studentService.UpdateStudentAsync(existingStudent);
                Console.WriteLine("Student updated successfully.");
            }
            else
            {
                Console.WriteLine("No changes were made to the student.");
            }
        }



        #endregion

        #region DeleteStudent
        private static async Task DeleteStudent(IStudentService studentService)
        {
            Console.WriteLine("Enter Student Code to Delete:");
            string code = Console.ReadLine();

            try
            {
                await studentService.DeleteStudentAsync(code);
                Console.WriteLine("Student deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete student: {ex.Message}");
            }
        }
        #endregion

        #region ViewAllStudents
        private static async Task ViewAllStudents(IStudentService studentService)
        {
            try
            {
                List<Student> students = await studentService.AllStudentsAsync();
                foreach (var student in students)
                {
                    Console.WriteLine($"Code: {student.StudentCode},Student Name: {student.StudentName}, Course: {student.Course}, Enrollment Date: {student.EnrollmentDate}, GPA: {student.GPA}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve students: {ex.Message}");
            }
        }
        #endregion


        #region ViewSudentByCode
        private static async Task ViewStudentByCode(IStudentService studentService)
        {
            Console.WriteLine("Enter Student Code to View:");
            string code = Console.ReadLine();

            try
            {
                Student student = await studentService.GetStudentByCodeAsync(code);
                if (student != null)
                {
                    Console.WriteLine($"Code: {student.StudentCode},Student Name: {student.StudentName}, Course: {student.Course}, Enrollment Date: {student.EnrollmentDate}, GPA: {student.GPA}");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve student: {ex.Message}");
            }
        }
        #endregion
    }
}

