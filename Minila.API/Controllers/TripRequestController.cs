using Microsoft.AspNetCore.Mvc;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Context;
using MinilaDataAcess.Model;

namespace Minila.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TripRequestController : ControllerBase
    {
        private readonly IRepository<TripRequest> _repository;
        protected readonly MinilaDBContext _dbContext;
        public TripRequestController(IRepository<TripRequest> repository, MinilaDBContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTripRequest(TripRequest model)
        {

            TripRequest AddTotripRequest = new TripRequest
            {
                StudetID = model.StudetID,
                ChauffeurId = model.ChauffeurId,
                lat = model.lat,
                lag = model.lag,
                location = model.location,
                road = model.road,
                status = model.status,
                CreateDate = model.CreateDate,
                UpdateDate = model.UpdateDate,
                RequestTime = model.RequestTime,
            };
            await _repository.AddAsync(AddTotripRequest);
            return Ok(AddTotripRequest);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTripRequest(TripRequest TripRequest)
        {
            try
            {
                await _repository.UpdateAsync(TripRequest);
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrieving Data From Db");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRequestByChauffeur(string ChauffeurID)
        {
            var GetRequestByChauffeur = _dbContext.TripRequest.Where(x => x.ChauffeurId == ChauffeurID).OrderByDescending(c => c.CreateDate).ToList();
            return Ok(GetRequestByChauffeur);
        }
        [HttpGet]
        public async Task<IActionResult> GetRequestByStudent(string StudentID)
        {
            var GetRequestListByStudent = _dbContext.TripRequest.Where(x => x.StudetID == StudentID).OrderByDescending(c => c.CreateDate).ToList();
            return Ok(GetRequestListByStudent);
        }


    }
}
