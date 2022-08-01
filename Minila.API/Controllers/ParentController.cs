using Microsoft.AspNetCore.Mvc;
using Minila.API.Dtos;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Model;

namespace Minila.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IRepository<Parent> _repository;
        public ParentController(IRepository<Parent> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByParentID(long id)
        {
            var findParent = await _repository.GetByIdAsync(id);
            return Ok(findParent);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllParent()
        {
            var result = await _repository.GetAsQueryableAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddParent(RegisterApiDtos model)
        {

            Parent AddToParent = new Parent
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                NationalID = model.NationalID,
                UserCode = model.UserCode,
                Status = model.Status,
                CreateDate = model.CreateDate,
            };
            await _repository.AddAsync(AddToParent);
            return Ok(AddToParent);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Parent parent)
        {
            try
            {
                await _repository.UpdateAsync(parent);
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrieving Data From Db");
            }
        }



    }
}
