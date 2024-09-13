using Microsoft.EntityFrameworkCore;
using School.Interfaces;
using School.Models;

namespace School.Repositories
{
    public class StudentRepository : IStudent
    {
        private readonly SchoolContext schoolContext;

        public StudentRepository(SchoolContext context)
        {
            schoolContext = context;
        }

        public async Task SaveAsync(Student student)
        {
            await schoolContext.Students.AddAsync(student);
            await schoolContext.SaveChangesAsync();
        }

        public async Task AssignCourcesAsync(int id, List<string> cources)
        {
            var ListCources = cources.Aggregate((a, b) => a + "," + b).TrimStart(",".ToCharArray());

            await schoolContext.Students
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(_ => _.Cources, ListCources));
        }

        public Student? GetStudentAsync(int id)
        {
            return schoolContext.Students.Where(s => s.Id == id).FirstOrDefault();
        }

        public List<Student> GetStudentAsync()
        {
            return schoolContext.Students.ToList();
        }
    }
}