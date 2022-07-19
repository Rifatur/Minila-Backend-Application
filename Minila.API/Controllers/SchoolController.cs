using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Model;

namespace Minila.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IRepository<School> _repository;
        public SchoolController(IRepository<School> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBySchoolID(long id)
        {
            var findParent = await _repository.GetByIdAsync(id);
            return Ok(findParent);
        }
        [HttpGet]
        public async Task<IActionResult> GetSchool()
        {
            var result = await _repository.GetAsQueryableAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddSchool(School model)
        {

            School AddToSchool = new School
            {
                SchoolName = model.SchoolName,
                status = model.status

            };
            await _repository.AddAsync(AddToSchool);
            return Ok(AddToSchool);
        }
    }
}
