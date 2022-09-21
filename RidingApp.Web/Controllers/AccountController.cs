using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RidingApp.Core.DTOs.Requests;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;
using RidingApp.Web.DTOs.Requests;

namespace RidingApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _DbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(

            UserManager<IdentityUser> userManager,
            ApplicationDbContext DbContext,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)

        {
            _userManager = userManager;
            _DbContext = DbContext;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Register(string returnurl = null)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                //Create Roles
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Student"));
                await _roleManager.CreateAsync(new IdentityRole("Parent"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Student",
                Text = "Student"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Parent",
                Text = "Parent"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "User",
                Text = "User"
            });
            UserRegistrationWebDTOs Newregistration = new UserRegistrationWebDTOs()
            {
                RoleList = listItems
            };
            return View(Newregistration);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationWebDTOs request)
        {

            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Status = request.Status,
                    Created = request.Created,
                    LastModified = request.LastModified
                };
                var isCreated = await _userManager.CreateAsync(newUser, request.Password);
                if (isCreated.Succeeded)
                {
                    if (request.RoleSelected != null && request.RoleSelected.Length > 0 && request.RoleSelected == "Admin")
                    {
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                    else if (request.RoleSelected != null && request.RoleSelected.Length > 0 && request.RoleSelected == "Student")
                    {
                        await _userManager.AddToRoleAsync(newUser, "Student");
                    }
                    else if (request.RoleSelected != null && request.RoleSelected.Length > 0 && request.RoleSelected == "Parent")
                    {
                        await _userManager.AddToRoleAsync(newUser, "Parent");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, "User");

                    }

                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    TempData["alertMessage"] = "Registration Successfully Done ..!";
                    return RedirectToAction(nameof(AccountController.Login), "Account"); ;
                }


            }
            //If request fail then Show The Role List
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Student",
                Text = "Student"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Parent",
                Text = "Parent"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "User",
                Text = "User"
            });
            request.RoleList = listItems;
            UserRegistrationWebDTOs registration = new UserRegistrationWebDTOs();
            return View(registration);

        }
        private void AddError(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }
        [HttpGet]
        public IActionResult Login(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserSignInWebDTOs model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    return LocalRedirect(returnurl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserSignInWebDTOs model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var Users = await _DbContext.ApplicationUser.FirstOrDefaultAsync(x => x.Email == model.Email);
                    var UserInStudentRole = await (from user in _DbContext.Users
                                                   join userRole in _DbContext.UserRoles
                                                   on user.Id equals userRole.UserId
                                                   join role in _DbContext.Roles
                                                   on userRole.RoleId equals role.Id
                                                   where role.Name == "Student"
                                                   select user).FirstOrDefaultAsync();
                    if (UserInStudentRole != null)
                    {
                        return RedirectToAction("index", "Admin");
                    }

                    return LocalRedirect(returnurl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }
    }
}
