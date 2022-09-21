using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RidingApp.Core.Services.Interfaces;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;
using RidingApp.Presentation.DTOs;

namespace RidingApp.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TripRequestController : ControllerBase
    {
        private readonly IRepository<TripRequest> _repository;
        private readonly ApplicationDbContext _DbContext;
        public TripRequestController(IRepository<TripRequest> repository, ApplicationDbContext DbContext)
        {
            _repository = repository;
            _DbContext = DbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int? top, int? take, string? search)
        {
            var result = await _repository.GetAsQueryableAsync();
            return Ok(result);

        }
        [HttpGet]
        public async Task<IActionResult> GetRequestByToday()
        {
            DateTime now = DateTime.Now;
            DateTime utc = DateTime.UtcNow;
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var result = await _DbContext.TripRequest.Where(x => x.RequestDate >= today && x.RequestDate < tomorrow).ToListAsync();
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> GetRequestByDate(DateTime fromDate, DateTime toDate)
        {
            var GetRequest = await _DbContext.TripRequest.Where(d => d.RequestDate.Date <= fromDate.Date && d.RequestDate.Date >= toDate.Date).ToListAsync(); ;
            return Ok(GetRequest);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(long id)
        {
            var result = await _repository.GetByIdLongAsync(id);
            return Ok(result);
        }
        //Create New 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TripRequestPreDTOs model)
        {
            DateTime now = DateTime.Now;
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }
            var NewTripRequest = new TripRequest
            {
                StudetID = model.StudetID,
                ChauffeurId = model.ChauffeurId,
                lat = model.lat,
                lag = model.lag,
                location = model.location,
                road = model.road,
                status = model.status,
                RequestDate = model.RequestDate,
                UpdateDate = model.UpdateDate,
                RequestTime = now
            };
            await _repository.AddAsync(NewTripRequest);
            return Ok(NewTripRequest);
        }
        //Update New School
        [HttpPut]
        public async Task<IActionResult> Update(TripRequest updatemodel)
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
            TripRequest iSresult = await _repository.GetByIdLongAsync(id);
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
