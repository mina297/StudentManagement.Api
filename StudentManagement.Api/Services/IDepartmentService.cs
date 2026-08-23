using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department? GetById(int id);
        Department Add(CreateDepartmentDto newDepartment);
        Department? Update(int id, UpdateDepartmentDto updatedDepartment);
        bool Delete(int id);
    }
}