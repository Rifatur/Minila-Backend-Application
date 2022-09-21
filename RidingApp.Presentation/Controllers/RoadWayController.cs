using Microsoft.AspNetCore.Mvc;
using RidingApp.Core.Services.Interfaces;
using RidingApp.DataAccess.Entities;
using RidingApp.Presentation.DTOs;

namespace RidingApp.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoadWayController : ControllerBase
    {
        private readonly IRepository<RoadWay> _repository;
        public RoadWayController(IRepository<RoadWay> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? top, int? take, string? search)
        {
            var result = await _repository.GetAsQueryableAsync();
            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(b => b.RoadName.Contains(search));
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetByID(long id)
        {
            var result = await _repository.GetByIdLongAsync(id);
            return Ok(result);
        }
        //Create New 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoadWayDTOs model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }
            var AddNewSchool = new RoadWay
            {
                RoadName = model.RoadName,
                RoadCode = model.RoadCode,
                schoolId = model.schoolId,
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
        public async Task<IActionResult> Update(RoadWay updatemodel)
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
            RoadWay roadway = await _repository.GetByIdLongAsync(id);
            if (roadway != null)
            {
                await _repository.DeleteAsync(roadway);
            }
            else
            {
                return NoContent();
            }
            return Ok();

        }
    }
}
