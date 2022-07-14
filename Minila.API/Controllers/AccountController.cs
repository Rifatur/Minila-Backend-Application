using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Dtos;
using MinilaDataAcess.Model;

namespace Minila.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepository<AppUsers> _repository ;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IRepository<AppUsers> repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount([FromBody] UserRegisterDaDtos model)
        {

                AppUsers User = new AppUsers
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email= model.Email,
                    PhoneNumber=model.PhoneNumber,
                    UserCode=model.UserCode,
                    loginIdentity=model.loginIdentity,
                    Status=model.Status,
                    CreateDate=model.CreateDate,
                };
                var Result = await _userManager.CreateAsync(User, model.Password);
                if (!Result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });

            
            
        }
    }
}
