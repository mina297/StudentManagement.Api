using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        private StudentDetailsDto MapToDto(Student student)
        {
            return new StudentDetailsDto
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentName = student.Department != null ? student.Department.Name : "Unknown"
            };
        }

        public List<StudentDetailsDto> GetAll()
        {
            var studentsList = _context.Students
                .Include(s => s.Department)
                .ToList();

            return studentsList.Select(MapToDto).ToList();
        }

        public StudentDetailsDto? GetById(int id)
        {
            var student = _context.Students
                .Include(s => s.Department)
                .FirstOrDefault(s => s.Id == id);

            return student == null ? null : MapToDto(student);
        }

        public (StudentDetailsDto? Student, string? Error) Add(CreateStudentDto newStudent)
        {
            if (string.IsNullOrWhiteSpace(newStudent.Name))
            {
                return (null, "Name is required.");
            }

            if (newStudent.Age < 18 || newStudent.Age > 60)
            {
                return (null, "Age must be between 18 and 60.");
            }

            var departmentExists = _context.Departments.Any(d => d.Id == newStudent.DepartmentId);

            if (!departmentExists)
            {
                return (null, "Department does not exist.");
            }

            var student = new Student
            {
                Name = newStudent.Name,
                Age = newStudent.Age,
                DepartmentId = newStudent.DepartmentId
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            _context.Entry(student).Reference(s => s.Department).Load();

            return (MapToDto(student), null);
        }

        public (StudentDetailsDto? Student, string? Error) Update(int id, UpdateStudentDto updatedStudent)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return (null, "Student not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedStudent.Name))
            {
                return (null, "Name is required.");
            }

            if (updatedStudent.Age < 18 || updatedStudent.Age > 60)
            {
                return (null, "Age must be between 18 and 60.");
            }

            var departmentExists = _context.Departments.Any(d => d.Id == updatedStudent.DepartmentId);

            if (!departmentExists)
            {
                return (null, "Department does not exist.");
            }

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.DepartmentId = updatedStudent.DepartmentId;

            _context.SaveChanges();

            _context.Entry(student).Reference(s => s.Department).Load();

            return (MapToDto(student), null);
        }

        public bool Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);
            _context.SaveChanges();

            return true;
        }

                public List<StudentDetailsDto> Search(string name)
        {
            var studentsList = _context.Students
                .Include(s => s.Department)
                .Where(s => s.Name.Contains(name) || (s.Department != null && s.Department.Name.Contains(name)))
                .ToList();

            return studentsList.Select(MapToDto).ToList();
        }

        public List<StudentDetailsDto> GetStudentsBetween18And22()
        {
            var studentsList = _context.Students
                .Include(s => s.Department)
                .Where(s => s.Age >= 18 && s.Age <= 22)
                .OrderBy(s => s.Age)
                .ToList();

            return studentsList.Select(MapToDto).ToList();
        }

        public List<DepartmentStatisticsDto> GetDepartmentStatistics()
        {
            var departments = _context.Departments.Include(d => d.Students).ToList();

            return departments.Select(d =>
            {
                var studentsInDept = d.Students ?? new List<Student>();

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