using School.Models;
namespace School.Interfaces
{
    public interface IStudent
    {
        Task SaveAsync(Student student);
        Task AssignCourcesAsync(int id, List<string> cources);
        Student? GetStudentAsync(int id);
        List<Student> GetStudentAsync();
    }
}
