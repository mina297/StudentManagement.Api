using StudentManagement.Api.Dtos;

namespace StudentManagement.Api.Services
{
    public interface IStudentService
    {
        List<StudentDetailsDto> GetAll();
        StudentDetailsDto? GetById(int id);
        (StudentDetailsDto? Student, string? Error) Add(CreateStudentDto newStudent);
        (StudentDetailsDto? Student, string? Error) Update(int id, UpdateStudentDto updatedStudent);
        bool Delete(int id);
        List<StudentDetailsDto> Search(string name);
        List<StudentDetailsDto> GetStudentsBetween18And22();
        List<DepartmentStatisticsDto> GetDepartmentStatistics();
    }
}