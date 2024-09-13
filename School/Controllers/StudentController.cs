using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using School.Services;

namespace School.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private StudentService StudentService { get; set; }
        public StudentController(StudentService studentService)
        {
            StudentService = studentService;
        }

        
        [HttpPost("/save-student")]
        [ProducesResponseType(statusCode:401)]
        public async Task SaveStudentAsync(string name, string emailId, long phoneNo)
        {
            await StudentService.SaveStudentAsync(new Student(name, emailId, phoneNo));
        }

        [HttpGet("/get-student{id}")]
        public ActionResult<Student> FetchStudentAsync(int id)
        {
            var student = StudentService.FetchStudentAsync(id);

            if (student is not null) { return student; }

            return NotFound();
        }

        [HttpGet("/get-all-students")]
        public ActionResult<List<Student>> FetchStudentAsync()
        {
            return StudentService.FetchAllStudentAsync();
        }

        [HttpPatch("/assign-cources")]
        public async Task AssignCources(int id, List<string> cources)
        {
            await StudentService.UpdateCourcesAsync(id, cources);
        }
    }
}
