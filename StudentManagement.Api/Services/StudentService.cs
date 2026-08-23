using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IDepartmentService _departmentService;

        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Mina Elkomos Samaan", Age = 20, DepartmentId = 1 },
            new Student { Id = 2, Name = "Sara Ali", Age = 21, DepartmentId = 2 },
            new Student { Id = 3, Name = "Mostafa Hassan", Age = 19, DepartmentId = 3 },
            new Student { Id = 4, Name = "Nour Ibrahim", Age = 22, DepartmentId = 4 },
            new Student { Id = 5, Name = "Youssef Mahmoud", Age = 18, DepartmentId = 1 }
        };

        public StudentService(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        private StudentDetailsDto MapToDto(Student student)
        {
            var department = _departmentService.GetById(student.DepartmentId);

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

        public (StudentDetailsDto? Student, string? Error) Add(CreateStudentDto newStudent)
        {
            var department = _departmentService.GetById(newStudent.DepartmentId);

            if (department == null)
            {
                return (null, "Department does not exist.");
            }

            var student = new Student
            {
                Id = students.Max(s => s.Id) + 1,
                Name = newStudent.Name,
                Age = newStudent.Age,
                DepartmentId = newStudent.DepartmentId
            };

            students.Add(student);

            return (MapToDto(student), null);
        }

        public (StudentDetailsDto? Student, string? Error) Update(int id, UpdateStudentDto updatedStudent)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return (null, "Student not found.");
            }

            var department = _departmentService.GetById(updatedStudent.DepartmentId);

            if (department == null)
            {
                return (null, "Department does not exist.");
            }

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.DepartmentId = updatedStudent.DepartmentId;

            return (MapToDto(student), null);
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
        public List<DepartmentStatisticsDto> GetDepartmentStatistics()
        {
            var departments = _departmentService.GetAll();

            return departments.Select(d =>
            {
                var studentsInDept = students.Where(s => s.DepartmentId == d.Id).ToList();

                return new DepartmentStatisticsDto
                {
                    DepartmentName = d.Name,
                    StudentsCount = studentsInDept.Count,
                    AverageAge = studentsInDept.Any() ? studentsInDept.Average(s => s.Age) : 0,
                    OldestAge = studentsInDept.Any() ? studentsInDept.Max(s => s.Age) : 0,
                    YoungestAge = studentsInDept.Any() ? studentsInDept.Min(s => s.Age) : 0
                };
            }).ToList();
        }

        public List<DepartmentStatisticsDto> GetHighestAndLowestDepartments()
        {
            var stats = GetDepartmentStatistics();

            if (!stats.Any())
            {
                return new List<DepartmentStatisticsDto>();
            }

            var maxCount = stats.Max(s => s.StudentsCount);
            var minCount = stats.Min(s => s.StudentsCount);

            return stats
                .Where(s => s.StudentsCount == maxCount || s.StudentsCount == minCount)
                .ToList();
        }
    }
}