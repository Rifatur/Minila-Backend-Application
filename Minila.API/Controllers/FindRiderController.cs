using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minila.API.Dtos;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Model;

namespace Minila.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FindRiderController : ControllerBase
    {
        private readonly IRepository<FindRider> _repository;
        public FindRiderController(IRepository<FindRider> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetByFindRider(long id)
        {
            var findParent = await _repository.GetByIdAsync(id);
            return Ok(findParent);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFindRider()
        {
            var result = await _repository.GetAsQueryableAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddFindRider(CreateFindRiderDtos model)
        {
            DateTime now = DateTime.Now;
            //Using TimeSpan
            
            FindRider AddToFindRider = new FindRider
            {
                ChauffeurId = model.ChauffeurId,
                RoadId = model.RoadId,
                SchholId = model.SchholId,
                remark = model.remark,
                CreateDate = model.CreateDate,
                lastUpdateTime = now,

            };
            await _repository.AddAsync(AddToFindRider);
            return Ok(AddToFindRider);
        }


    }
}
