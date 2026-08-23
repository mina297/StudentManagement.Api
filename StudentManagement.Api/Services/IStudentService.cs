using StudentManagement.Api.Dtos;

namespace StudentManagement.Api.Services
{
    public interface IStudentService
    {
        List<StudentDetailsDto> GetAll();
        StudentDetailsDto? GetById(int id);
        StudentDetailsDto Add(CreateStudentDto newStudent);
        StudentDetailsDto? Update(int id, UpdateStudentDto updatedStudent);
        bool Delete(int id);
        List<StudentDetailsDto> Search(string name);
        List<StudentDetailsDto> GetStudentsBetween18And22();
    }
}