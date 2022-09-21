using Microsoft.AspNetCore.Mvc;
using RidingApp.Core.Services.Interfaces;
using RidingApp.DataAccess.Entities;
using RidingApp.Presentation.DTOs;

namespace RidingApp.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PersonalDetailsController : ControllerBase
    {
        private readonly IRepository<PersonalDetails> _repository;
        public PersonalDetailsController(IRepository<PersonalDetails> repository)
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
        public async Task<IActionResult> Create([FromBody] PersonalDetailsPreDTOs model)
        {
            DateTime now = DateTime.Now;
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }
            var NewTripRequest = new PersonalDetails
            {
                UserID = model.UserID,
                Address = model.Address,
                SchoolId = model.SchoolId,
                NID = model.NID,
                IdentityCard = model.IdentityCard,
                licenseNo = model.licenseNo,
                CreatedBy = model.CreatedBy,
                Created = model.Created,
                LastModifiedBy = model.LastModifiedBy,
                LastModified = model.LastModified,
            };
            await _repository.AddAsync(NewTripRequest);
            return Ok(NewTripRequest);
        }
        //Update New School
        [HttpPut]
        public async Task<IActionResult> Update(PersonalDetails updatemodel)
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
            PersonalDetails iSresult = await _repository.GetByIdLongAsync(id);
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
