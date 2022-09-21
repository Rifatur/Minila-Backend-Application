using Microsoft.AspNetCore.Mvc;
using RidingApp.Core.Services.Interfaces;
using RidingApp.DataAccess.Entities;
using RidingApp.Presentation.DTOs;

namespace RidingApp.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IRepository<Vehicle> _repository;
        public VehicleController(IRepository<Vehicle> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetByID(long id)
        {
            var result = await _repository.GetByIdLongAsync(id);
            return Ok(result);
        }
        //Create New 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehiclePreDTOs model)
        {
            DateTime now = DateTime.Now;
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }
            var NewVehicle = new Vehicle
            {
                VehicleName = model.VehicleName,
                VehicleNumber = model.VehicleNumber,
                SeatCapacity = model.SeatCapacity,
                CreatedBy = model.CreatedBy,
                Created = model.Created,
                LastModifiedBy = model.LastModifiedBy,
                LastModified = model.LastModified,

            };
            await _repository.AddAsync(NewVehicle);
            return Ok(NewVehicle);
        }
        //Update New School
        [HttpPut]
        public async Task<IActionResult> Update(Vehicle updatemodel)
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
            Vehicle iSresult = await _repository.GetByIdLongAsync(id);
            if (iSresult != null)
            {
                await _repository.DeleteAsync(iSresult);
            }
            else
            {
                return NoContent();
            }
            return Ok();

        }
    }
}
