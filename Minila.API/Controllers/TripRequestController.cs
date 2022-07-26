using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Model;

namespace Minila.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TripRequestController : ControllerBase
    {
        private readonly IRepository<TripRequest> _repository;
        public TripRequestController(IRepository<TripRequest> repository)
        {
            _repository = repository;
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



    }
}
