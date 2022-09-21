using Microsoft.AspNetCore.Mvc;
using RidingApp.Core.Services.Interfaces;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;
using RidingApp.Presentation.DTOs;

namespace RidingApp.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RiderHubController : ControllerBase
    {
        private readonly IRepository<RiderHub> _repository;
        private readonly ApplicationDbContext _DbContext;
        public RiderHubController(IRepository<RiderHub> repository, ApplicationDbContext DbContext)
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
        public async Task<IActionResult> GetByID(long id)
        {
            var result = await _repository.GetByIdLongAsync(id);
            return Ok(result);
        }

        //Create New 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RiderHubDTOs model)
        {
            DateTime now = DateTime.Now;
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }
            var AddNew = new RiderHub
            {
                RiderId = model.RiderId,
                RoadWayId = model.RoadWayId,
                SchholId = model.SchholId,
                CarId = model.CarId,
                remark = model.remark,
                lastUpdateTime = now,
                CreatedBy = model.CreatedBy,
                Created = model.Created,
                LastModifiedBy = model.LastModifiedBy,
                LastModified = model.LastModified,

            };
            await _repository.AddAsync(AddNew);
            return Ok(AddNew);
        }
        //Update New School
        [HttpPut]
        public async Task<IActionResult> Update(RiderHub updatemodel)
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
            RiderHub iSresult = await _repository.GetByIdLongAsync(id);
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
        public async Task<IActionResult> GetRider(int roadid, int schoolid)
        {
            var findRider = _DbContext.RiderHub.Where(r => r.RoadWayId == roadid).Where(s => s.SchholId == schoolid).ToList();

            return Ok(findRider);
        }

    }
}
