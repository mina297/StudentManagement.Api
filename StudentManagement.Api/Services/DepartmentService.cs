using StudentManagement.Api.Dtos;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services
{
    public class DepartmentService : IDepartmentService
    {
        private static List<Department> departments = new List<Department>
        {
            new Department { Id = 1, Name = "IT" },
            new Department { Id = 2, Name = "HR" },
            new Department { Id = 3, Name = "Finance" },
            new Department { Id = 4, Name = "Sales" }
        };

        public List<Department> GetAll()
        {
            return departments;
        }

        public Department? GetById(int id)
        {
            return departments.FirstOrDefault(d => d.Id == id);
        }

        public Department Add(CreateDepartmentDto newDepartment)
        {
            var department = new Department
            {
                Id = departments.Max(d => d.Id) + 1,
                Name = newDepartment.Name
            };

            departments.Add(department);
            return department;
        }

        public Department? Update(int id, UpdateDepartmentDto updatedDepartment)
        {
            var department = departments.FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                return null;
            }

            department.Name = updatedDepartment.Name;
            return department;
        }

        public bool Delete(int id)
        {
            var department = departments.FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                return false;
            }

            departments.Remove(department);
            return true;
        }
    }
}