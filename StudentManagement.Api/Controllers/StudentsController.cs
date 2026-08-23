using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
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

        [HttpGet("welcome")]
        public IActionResult Welcome()
        {
            return Ok("Welcome to Student Management API");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name)
        {
            var result = students
                .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(result);
        }


        [HttpGet("filter-by-age")]
        public IActionResult FilterByAge()
        {
            var result = students
                .Where(s => s.Age >= 18 && s.Age <= 22)
                .OrderBy(s => s.Age)
                .ToList();

            return Ok(result);
        }




        [HttpPost]
        public IActionResult Create([FromBody] CreateStudentDto newStudent)
        {
            var student = new Student
            {
                Id = students.Max(s => s.Id) + 1,
                Name = newStudent.Name,
                Age = newStudent.Age,
                DepartmentId = newStudent.DepartmentId
            };

            students.Add(student);

            return Ok(student);
        }




        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateStudentDto updatedStudent)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.DepartmentId = updatedStudent.DepartmentId;

            return Ok(student);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            students.Remove(student);

            return Ok();
        }
    }
}