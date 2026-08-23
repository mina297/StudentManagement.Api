using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Dtos;
using StudentManagement.Api.Services;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("welcome")]
        public IActionResult Welcome()
        {
            return Ok("Welcome to Student Management API");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_studentService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name)
        {
            return Ok(_studentService.Search(name));
        }

        [HttpGet("filter-by-age")]
        public IActionResult FilterByAge()
        {
            return Ok(_studentService.GetStudentsBetween18And22());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStudentDto newStudent)
        {
            var student = _studentService.Add(newStudent);
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateStudentDto updatedStudent)
        {
            var student = _studentService.Update(id, updatedStudent);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _studentService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}