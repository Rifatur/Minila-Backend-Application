using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RidingApp.Core.Configuration;
using RidingApp.Core.DTOs.Responses;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;
using RidingApp.Presentation.DTOs;

namespace RidingApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly ILogger<AuthManagementController> _logger;
        private readonly ApplicationDbContext _DbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthManagementController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            ILogger<AuthManagementController> logger,
            ApplicationDbContext DbContext,
            SignInManager<IdentityUser> signInManager)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _userManager = userManager;
            _logger = logger;
            _DbContext = DbContext;
            _signInManager = signInManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTOs user)
        {
            if (ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "Email already in use"
                            },
                        Success = false
                    });
                }

                var newUser = new ApplicationUser()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Status = user.Status,
                    Created = user.Created,
                    LastModified = user.LastModified
                };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return Ok();
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }





    }
}
