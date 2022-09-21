using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RidingApp.DataAccess;
using RidingApp.Web.DTOs;

namespace RidingApp.Web.Controllers
{
    public class SystemImageController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public SystemImageController(

            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext DbContext,
            IWebHostEnvironment hostEnvironment,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _DbContext = DbContext;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,ProfilePicture,CarPicture,")] ImagesDtos ImagesDtos)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(ImagesDtos.ImageFile.FileName);
                string extension = Path.GetExtension(ImagesDtos.ImageFile.FileName);
                ImagesDtos.ProfilePicture = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await ImagesDtos.ImageFile.CopyToAsync(fileStream);
                }
                //Insert record
                _DbContext.Add(ImagesDtos);
                await _DbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ImagesDtos);
        }

    }
}
