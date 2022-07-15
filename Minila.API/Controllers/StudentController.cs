using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minila.API.Dtos;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Model;

namespace Minila.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRepository<Student> _repository;
        public StudentController(IRepository<Student> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByStudentID(long id)
        {
            var findParent = await _repository.GetByIdAsync(id);
            return Ok(findParent);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _repository.GetAsQueryableAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(RegisterApiDtos model)
        {

            Student AddToStudent = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                UserCode = model.UserCode,
                Status = model.Status,
                CreateDate = model.CreateDate,
            };
            await _repository.AddAsync(AddToStudent);
            return Ok(AddToStudent);
        }
    }
}
