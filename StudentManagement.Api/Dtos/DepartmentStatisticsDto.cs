namespace StudentManagement.Api.Dtos
{
    public class DepartmentStatisticsDto
    {
        public string DepartmentName { get; set; }
        public int StudentsCount { get; set; }
        public double AverageAge { get; set; }
        public int OldestAge { get; set; }
        public int YoungestAge { get; set; }
    }
}