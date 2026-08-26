using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public (Department? Department, string? Error) Add(CreateDepartmentDto newDepartment)
        {
            if (string.IsNullOrWhiteSpace(newDepartment.Name))
            {
                return (null, "Department name is required.");
            }

            var nameExists = _context.Departments.Any(d => d.Name.ToLower() == newDepartment.Name.ToLower());

            if (nameExists)
            {
                return (null, "Department name already exists.");
            }

            var department = new Department
            {
                Name = newDepartment.Name
            };

            _context.Departments.Add(department);
            _context.SaveChanges();

            return (department, null);
        }

        public (Department? Department, string? Error) Update(int id, UpdateDepartmentDto updatedDepartment)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                return (null, "Department not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedDepartment.Name))
            {
                return (null, "Department name is required.");
            }

            var nameExists = _context.Departments.Any(d => d.Id != id && d.Name.ToLower() == updatedDepartment.Name.ToLower());

            if (nameExists)
            {
                return (null, "Department name already exists.");
            }

            department.Name = updatedDepartment.Name;
            _context.SaveChanges();

            return (department, null);
        }

        public bool Delete(int id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                return false;
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();

            return true;
        }
    }
}