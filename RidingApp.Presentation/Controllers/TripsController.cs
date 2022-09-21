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
    public class TripsController : ControllerBase
    {
        private readonly IRepository<Trip> _repository;

        private readonly ApplicationDbContext _DbContext;
        public TripsController(IRepository<Trip> repository, ApplicationDbContext dbContext)
        {
            _repository = repository;
            _DbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int? top, int? take, string? search)
        {
            var result = await _repository.GetAsQueryableAsync();
            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(b => b.CreatedBy.Contains(search));
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
        public async Task<IActionResult> Create([FromBody] CreateTripPreDTOs model)
        {
            DateTime now = DateTime.Now;
            Random rnd = new Random();
            int myRandomNo = rnd.Next(100000, 999999);
            string TripCode = "tp" + myRandomNo;
            string Tripstatus = "1";

            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }
            var NewTrip = new Trip
            {

                tripCode = TripCode,
                tripStatus = Tripstatus,
                tripStart = model.tripStart,
                tripEnd = model.tripEnd,
                StudetID = model.StudetID,
                DriverId = model.DriverId,
                cancelReason = model.cancelReason,
                startLocation = model.startLocation,
                endLocation = model.endLocation,
                TripRequestId = model.TripRequestId,
                CreatedBy = model.CreatedBy,
                Created = model.Created,
                LastModifiedBy = model.LastModifiedBy,
                LastModified = model.LastModified,

            };
            await _repository.AddAsync(NewTrip);
            return Ok(NewTrip);
        }
        //Update New School
        [HttpPut]
        public async Task<IActionResult> Update(Trip updatemodel)
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
            Trip iSresult = await _repository.GetByIdLongAsync(id);
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
        [HttpGet]
        public async Task<IActionResult> GetTriptByToday()
        {
            DateTime now = DateTime.Now;
            DateTime utc = DateTime.UtcNow;
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var result = await _DbContext.Trip.Where(x => x.Created >= today && x.Created < tomorrow).ToListAsync();
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> GetRequestByDate(DateTime fromDate, DateTime toDate)
        {
            var GetRequest = await _DbContext.Trip.Where(d => d.Created.Date <= fromDate.Date && d.Created.Date >= toDate.Date).ToListAsync();
            return Ok(GetRequest);
        }

    }
}
