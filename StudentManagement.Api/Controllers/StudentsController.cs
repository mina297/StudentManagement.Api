using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Mina Elkomos Samaan", Age = 20, DepartmentName = "IT" },
            new Student { Id = 2, Name = "Sara Ali", Age = 21, DepartmentName = "HR" },
            new Student { Id = 3, Name = "Mostafa Hassan", Age = 19, DepartmentName = "Finance" },
            new Student { Id = 4, Name = "Nour Ibrahim", Age = 22, DepartmentName = "Sales" },
            new Student { Id = 5, Name = "Youssef Mahmoud", Age = 18, DepartmentName = "Marketing" }
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
                DepartmentName = newStudent.DepartmentName
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
            student.DepartmentName = updatedStudent.DepartmentName;

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