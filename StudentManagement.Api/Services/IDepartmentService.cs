using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department? GetById(int id);
        (Department? Department, string? Error) Add(CreateDepartmentDto newDepartment);
        (Department? Department, string? Error) Update(int id, UpdateDepartmentDto updatedDepartment);
        bool Delete(int id);
    }
}