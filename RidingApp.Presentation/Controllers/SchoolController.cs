using Microsoft.AspNetCore.Mvc;
using RidingApp.Core.Services.Interfaces;
using RidingApp.DataAccess.Entities;
using RidingApp.Presentation.DTOs;

namespace RidingApp.Presentation.Controllers
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
        public async Task<IActionResult> Get(int? top, int? take, string? search)
        {
            var result = await _repository.GetAsQueryableAsync();
            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(b => b.SchoolName.Contains(search));
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBySchoolID(long id)
        {
            var findSchool = await _repository.GetByIdLongAsync(id);
            return Ok(findSchool);
        }
        //Create New School
        [HttpPost]
        public async Task<IActionResult> CreateSchool([FromBody] CreateSchoolDTOs model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }
            var AddNewSchool = new School
            {

                SchoolName = model.SchoolName,
                status = model.status,
                CreatedBy = model.CreatedBy,
                Created = model.Created,
                LastModifiedBy = model.LastModifiedBy,
                LastModified = model.LastModified,

            };
            await _repository.AddAsync(AddNewSchool);
            return Ok(AddNewSchool);
        }
        //Update New School
        [HttpPut]
        public async Task<IActionResult> Update(School updatemodel)
        {
            try
            {
                await _repository.UpdateAsync(updatemodel);
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrieving Data From Db");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            School school = await _repository.GetByIdLongAsync(id);
            if (school != null)
            {
                await _repository.DeleteAsync(school);
            }
            else
            {
                return NoContent();
            }
            return Ok();

        }
    }
}
