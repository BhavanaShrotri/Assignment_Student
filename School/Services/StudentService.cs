using School.Interfaces;
using School.Models;

namespace School.Services
{
    public class StudentService
    {
        private readonly IStudent _student;
        public StudentService(IStudent student)
        {
            _student = student;
        }

        public async Task SaveStudentAsync(Student student)
        {
            await _student.SaveAsync(student);
        }

        public async Task UpdateCourcesAsync(int id, List<string> Courses)
        {
            var studentData = FetchStudentAsync(id) ?? throw new Exception($"Student with id {id} not available");
            
            List<string> cources = new();
            
            if (string.IsNullOrEmpty(studentData.Cources))
            {
                cources = studentData.Cources.Split(",").ToList();
            }

            cources.AddRange(Courses);

            await _student.AssignCourcesAsync(id, cources.Distinct().ToList());
        }

        public Student? FetchStudentAsync(int id)
        {
            return _student.GetStudentAsync(id);
        }
        
        public List<Student> FetchAllStudentAsync()
        {
            return _student.GetStudentAsync();
        }

    }
}
