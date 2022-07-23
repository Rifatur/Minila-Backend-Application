using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minila.API.Dtos;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Context;
using MinilaDataAcess.Model;

namespace Minila.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ChauffeurController : ControllerBase
    {
        private readonly IRepository<Chauffeur> _repository;
        protected readonly MinilaDBContext _dbContext;
        public ChauffeurController(
            IRepository<Chauffeur> repository,
            MinilaDBContext dbContext
         )
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetByChauffeurID(long id)
        {
            var findParent = await _repository.GetByIdAsync(id);
            return Ok(findParent);
        }
        [HttpGet]
        public async Task<IActionResult> GetChauffeurs()
        {
            var result = await _repository.GetAsQueryableAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddChauffeur(RegisterApiDtos model)
        {

            Chauffeur AddToChauffeur = new Chauffeur
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                CRCode = model.UserCode,
                Status = model.Status,
                CreateDate = model.CreateDate,
            };
            await _repository.AddAsync(AddToChauffeur);
            return Ok(AddToChauffeur);
        }

        [HttpGet]
        public async Task<IActionResult> GetChauffeurRoad(int ChauffeurId)
        {
            var GetRoad = _dbContext.findRiders.Where(x => x.ChauffeurId == ChauffeurId).ToList();

            return Ok(GetRoad);
        }

    }
}
