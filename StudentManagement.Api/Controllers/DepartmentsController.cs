using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Dtos;
using StudentManagement.Api.Services;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;

        public DepartmentsController(IDepartmentService departmentService, IStudentService studentService)
        {
            _departmentService = departmentService;
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_departmentService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var department = _departmentService.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpGet("statistics")]
        public IActionResult GetStatistics()
        {
            return Ok(_studentService.GetDepartmentStatistics());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDepartmentDto newDepartment)
        {
            var department = _departmentService.Add(newDepartment);
            return Ok(department);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateDepartmentDto updatedDepartment)
        {
            var department = _departmentService.Update(id, updatedDepartment);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _departmentService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}