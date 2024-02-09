using DataAdapterSTUDENT.Model;
using DataAdapterSTUDENT.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAdapterSTUDENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class STDController : ControllerBase
    {
        private readonly IStudents _student;
        public STDController(IStudents student) 
        {
            _student = student;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_student.GetAllStudents());
        }

        [HttpGet("ID")]
        public IActionResult Get(int id)
        {
            return Ok(_student.GetStudents(id));
        }
        [HttpPut]
        public IActionResult Put(int id, [FromBody] Students students) 
        {
            _student.UpdateStudents(id, students);
            return Ok();
        }
        [HttpPost]
        public IActionResult POST([FromBody] Students students)
        {
            _student.AddStudents(students);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _student.DeleteStudents(id);
            return Ok();
        }
    }
}
