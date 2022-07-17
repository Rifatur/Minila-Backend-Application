using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Model;

namespace Minila.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoadwayController : ControllerBase
    {
        private readonly IRepository<RoadWay> _repository;
        public RoadwayController(IRepository<RoadWay> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByRoadwayID(long id)
        {
            var findParent = await _repository.GetByIdAsync(id);
            return Ok(findParent);
        }
        [HttpGet]
        public async Task<IActionResult> GetRoadway()
        {
            var result = await _repository.GetAsQueryableAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddRoadWay(RoadWay model)
        {

            RoadWay AddToRoadWay = new RoadWay
            {
                RoadName = model.RoadName,
                RoadCode = model.RoadCode,
                schoolId = model.schoolId,

            };
            await _repository.AddAsync(AddToRoadWay);
            return Ok(AddToRoadWay);
        }
    }
}
