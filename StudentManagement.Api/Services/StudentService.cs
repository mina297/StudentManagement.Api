using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public class StudentService : IStudentService
    {
        private static List<Department> departments = new List<Department>
        {
            new Department { Id = 1, Name = "IT" },
            new Department { Id = 2, Name = "HR" },
            new Department { Id = 3, Name = "Finance" },
            new Department { Id = 4, Name = "Sales" }
        };

        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Mina Elkomos Samaan", Age = 20, DepartmentId = 1 },
            new Student { Id = 2, Name = "Sara Ali", Age = 21, DepartmentId = 2 },
            new Student { Id = 3, Name = "Mostafa Hassan", Age = 19, DepartmentId = 3 },
            new Student { Id = 4, Name = "Nour Ibrahim", Age = 22, DepartmentId = 4 },
            new Student { Id = 5, Name = "Youssef Mahmoud", Age = 18, DepartmentId = 1 }
        };

        private StudentDetailsDto MapToDto(Student student)
        {
            var department = departments.FirstOrDefault(d => d.Id == student.DepartmentId);

            return new StudentDetailsDto
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentName = department != null ? department.Name : "Unknown"
            };
        }

        public List<StudentDetailsDto> GetAll()
        {
            return students.Select(MapToDto).ToList();
        }

        public StudentDetailsDto? GetById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            return student == null ? null : MapToDto(student);
        }

        public StudentDetailsDto Add(CreateStudentDto newStudent)
        {
            var student = new Student
            {
                Id = students.Max(s => s.Id) + 1,
                Name = newStudent.Name,
                Age = newStudent.Age,
                DepartmentId = newStudent.DepartmentId
            };

            students.Add(student);

            return MapToDto(student);
        }

        public StudentDetailsDto? Update(int id, UpdateStudentDto updatedStudent)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return null;
            }

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.DepartmentId = updatedStudent.DepartmentId;

            return MapToDto(student);
        }

        public bool Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return false;
            }

            students.Remove(student);
            return true;
        }

        public List<StudentDetailsDto> Search(string name)
        {
            return students
                .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Select(MapToDto)
                .ToList();
        }

        public List<StudentDetailsDto> GetStudentsBetween18And22()
        {
            return students
                .Where(s => s.Age >= 18 && s.Age <= 22)
                .OrderBy(s => s.Age)
                .Select(MapToDto)
                .ToList();
        }
    }
}