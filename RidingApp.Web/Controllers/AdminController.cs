using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RidingApp.DataAccess;
using RidingApp.DataAccess.Entities;
using RidingApp.Web.DTOs.Requests;
using RidingApp.Web.DTOs.ViewModels;

namespace RidingApp.Web.Controllers
{
    [Authorize(Roles = "Student,User")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _DbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AdminController(
            ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext DbContext,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _DbContext = DbContext;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            DateTime now = DateTime.Now;
            DateTime utc = DateTime.UtcNow;
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            ViewBag.time = now.ToString("T");
            ViewBag.Date = now.ToString("d");
            //getting All  List Of ... 
            List<SchoolWebDTOs> Schoollist = new List<SchoolWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/School/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Schoollist = JsonConvert.DeserializeObject<List<SchoolWebDTOs>>(apiRespose);
                }
            }
            ViewData["school"] = Schoollist;
            //getting All  List Of ... 
            List<RoadWayDTOs> RoadWaylist = new List<RoadWayDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/RoadWay/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RoadWaylist = JsonConvert.DeserializeObject<List<RoadWayDTOs>>(apiRespose);
                }
            }
            ViewData["Road"] = RoadWaylist;
            //Start Geting Total Trip
            List<TripWebDTOs> Triplist = new List<TripWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/Trips/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Triplist = JsonConvert.DeserializeObject<List<TripWebDTOs>>(apiRespose);
                }
            }
            //End Geting Total Trip

            /// <summary>
            /// Gets Total Trip Request for Count
            /// </summary>
            List<TripRequestWebDTOs> TripRequestlist = new List<TripRequestWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/TripRequest/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    TripRequestlist = JsonConvert.DeserializeObject<List<TripRequestWebDTOs>>(apiRespose);
                }
            }
            ViewData["TripRequest"] = TripRequestlist;

            /// <summary>
            /// Geting TripRequest By Today........!
            /// </summary>
            /// 
            List<TripRequestWebDTOs> TripRequestByToday = new List<TripRequestWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/TripRequest/GetRequestByToday"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    TripRequestByToday = JsonConvert.DeserializeObject<List<TripRequestWebDTOs>>(apiRespose);
                }
            }
            ViewData["TripRequestByToday"] = TripRequestByToday;
            /// <summary>
            /// Geting TripRequest By Today........
            /// </summary>



            /// <summary>
            /// User Details 
            /// </summary>
            List<ApplicationUser> UserList = new List<ApplicationUser>();
            var Users = await _DbContext.ApplicationUser.ToListAsync();
            foreach (var item in Users)
            {
                UserList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                });
            }
            ViewData["User"] = UserList;

            return View(Triplist);
        }

        /// <summary>
        /// School Action Method 
        /// </summary>
        public async Task<IActionResult> School(string? search)
        {
            List<SchoolWebDTOs> Schoollist = new List<SchoolWebDTOs>();
            //getting All  List Of ... 
            ViewData["CurrentSearch"] = search;
            if (search != null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var getResponse = await httpClient.GetAsync($"https://localhost:7006/School/Get?search={search}"))
                    {
                        string apiRespose = await getResponse.Content.ReadAsStringAsync();
                        Schoollist = JsonConvert.DeserializeObject<List<SchoolWebDTOs>>(apiRespose);
                    }
                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    using (var getResponse = await httpClient.GetAsync("https://localhost:7006/School/Get"))
                    {
                        string apiRespose = await getResponse.Content.ReadAsStringAsync();
                        Schoollist = JsonConvert.DeserializeObject<List<SchoolWebDTOs>>(apiRespose);
                    }
                }
            }

            //User Details
            List<ApplicationUser> UserList = new List<ApplicationUser>();
            var Users = await _DbContext.ApplicationUser.ToListAsync();
            foreach (var item in Users)
            {
                UserList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                });
            }
            ViewData["User"] = UserList;
            //Getting Time 
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;
            ViewBag.TotalSchoollist = Schoollist.Count();
            return View(Schoollist);
        }

        //Road Way
        [HttpGet]
        public async Task<IActionResult> Roadways(string? search)
        {
            List<RoadWayDTOs> RoadWaylist = new List<RoadWayDTOs>();
            //getting All  List Of ... 
            ViewData["CurrentSearch"] = search;
            if (search != null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var getResponse = await httpClient.GetAsync($"https://localhost:7006/RoadWay/Get?search={search}"))
                    {
                        string apiRespose = await getResponse.Content.ReadAsStringAsync();
                        RoadWaylist = JsonConvert.DeserializeObject<List<RoadWayDTOs>>(apiRespose);
                    }
                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    using (var getResponse = await httpClient.GetAsync("https://localhost:7006/RoadWay/Get"))
                    {
                        string apiRespose = await getResponse.Content.ReadAsStringAsync();
                        RoadWaylist = JsonConvert.DeserializeObject<List<RoadWayDTOs>>(apiRespose);
                    }
                }
            }

            //User Details
            List<ApplicationUser> UserList = new List<ApplicationUser>();
            var Users = await _DbContext.ApplicationUser.ToListAsync();
            foreach (var item in Users)
            {
                UserList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                });
            }
            ViewData["User"] = UserList;
            //Getting Time 
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;
            ViewBag.TotalSchoollist = RoadWaylist.Count();
            //
            List<SchoolWebDTOs> Schoollist = new List<SchoolWebDTOs>();
            //getting All  List Of ... 
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/School/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Schoollist = JsonConvert.DeserializeObject<List<SchoolWebDTOs>>(apiRespose);
                }
            }
            ViewData["school"] = Schoollist;


            return View(RoadWaylist);
        }
        //Start TRIP REQUEST METHOD
        public async Task<IActionResult> TripRequest()
        {
            List<TripRequestWebDTOs> TripRequestlist = new List<TripRequestWebDTOs>();
            //getting All  List Of ... 
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/TripRequest/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    TripRequestlist = JsonConvert.DeserializeObject<List<TripRequestWebDTOs>>(apiRespose);
                }
            }
            //User Details
            List<ApplicationUser> UserList = new List<ApplicationUser>();
            var Users = await _DbContext.ApplicationUser.ToListAsync();
            foreach (var item in Users)
            {
                UserList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                });
            }
            ViewData["User"] = UserList;
            //getting All  List Of School... 
            List<SchoolWebDTOs> Schoollist = new List<SchoolWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/School/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Schoollist = JsonConvert.DeserializeObject<List<SchoolWebDTOs>>(apiRespose);
                }
            }
            ViewData["school"] = Schoollist;
            //getting All  List Of Road... 
            List<RoadWayDTOs> RoadWaylist = new List<RoadWayDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/RoadWay/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RoadWaylist = JsonConvert.DeserializeObject<List<RoadWayDTOs>>(apiRespose);
                }
            }
            ViewData["Road"] = RoadWaylist;
            //getting All  List Of Road... 
            List<TripWebDTOs> Triplist = new List<TripWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/Trips/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Triplist = JsonConvert.DeserializeObject<List<TripWebDTOs>>(apiRespose);
                }
            }
            ViewData["Trip"] = Triplist;


            //Date 
            DateTime now = DateTime.Now;
            DateTime utc = DateTime.UtcNow;
            ViewBag.time = now.ToString("T");
            ViewBag.Date = now.ToString("d");

            return View(TripRequestlist);
        }//END TRIP REQUEST METHOD

        public async Task<IActionResult> Trip(string? search)
        {
            //User Details
            List<ApplicationUser> UserList = new List<ApplicationUser>();
            var Users = await _DbContext.ApplicationUser.ToListAsync();
            foreach (var item in Users)
            {
                UserList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                });
            }
            ViewData["User"] = UserList;
            //getting All  List Of Road... 
            List<TripWebDTOs> Triplist = new List<TripWebDTOs>();
            ViewData["CurrentSearch"] = search;
            if (search != null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var getResponse = await httpClient.GetAsync($"https://localhost:7006/Trips/Get?search={search}"))
                    {
                        string apiRespose = await getResponse.Content.ReadAsStringAsync();
                        Triplist = JsonConvert.DeserializeObject<List<TripWebDTOs>>(apiRespose);
                    }
                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    using (var getResponse = await httpClient.GetAsync("https://localhost:7006/Trips/Get"))
                    {
                        string apiRespose = await getResponse.Content.ReadAsStringAsync();
                        Triplist = JsonConvert.DeserializeObject<List<TripWebDTOs>>(apiRespose);
                    }
                }
            }


            return View(Triplist);
        }
        public async Task<IActionResult> EnableRoad()
        {


            //getting List Of Ride Request .. 
            List<RiderHubDTOs> RideRequestlist = new List<RiderHubDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync($"https://localhost:7006/RiderHub/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RideRequestlist = JsonConvert.DeserializeObject<List<RiderHubDTOs>>(apiRespose);
                }
            }

            return View(RideRequestlist);
        }

        //Find Rider
        [HttpGet]
        public async Task<IActionResult> GetRider(int roadid, int SchholId, int studentid)
        {
            //getting School List .. 
            List<RiderHubDTOs> GetMyRiderList = new List<RiderHubDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync($"https://localhost:7006/RiderHub/GetRider?roadid={roadid}&schoolid={SchholId}"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    GetMyRiderList = JsonConvert.DeserializeObject<List<RiderHubDTOs>>(apiRespose);
                }
            }
            //Contains
            ViewBag.TotalRiderListCount = GetMyRiderList.Count();
            DateTime now = DateTime.Now;
            DateTime utc = DateTime.UtcNow;
            ViewBag.time = now.ToString("T");
            ViewBag.Date = now.ToString("d");

            // Get School 
            List<SchoolWebDTOs> Schoollist = new List<SchoolWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/School/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Schoollist = JsonConvert.DeserializeObject<List<SchoolWebDTOs>>(apiRespose);
                }
            }
            ViewData["school"] = Schoollist;
            //getting All  List Of ... 
            List<RoadWayDTOs> RoadWaylist = new List<RoadWayDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/RoadWay/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RoadWaylist = JsonConvert.DeserializeObject<List<RoadWayDTOs>>(apiRespose);
                }
            }
            ViewData["Road"] = RoadWaylist;

            List<ApplicationUser> UserList = new List<ApplicationUser>();
            var Users = await _DbContext.ApplicationUser.ToListAsync();
            foreach (var item in Users)
            {
                UserList.Add(new ApplicationUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                });
            }
            ViewData["User"] = UserList;

            return View(GetMyRiderList);
        }

        [HttpGet]
        [Route("profile/{username}")]
        public async Task<ActionResult> Profile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return NotFound("User not found");

            ViewData["UserInfo"] = await _DbContext.PersonalDetails.ToListAsync();
            //Date 
            DateTime now = DateTime.Now;
            DateTime utc = DateTime.UtcNow;
            ViewBag.time = now.ToString("T");
            ViewBag.Date = now.ToString("d");

            //getting All  List Of School... 
            List<SchoolWebDTOs> Schoollist = new List<SchoolWebDTOs>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7006/School/Get"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Schoollist = JsonConvert.DeserializeObject<List<SchoolWebDTOs>>(apiRespose);
                }
            }
            ViewData["school"] = Schoollist;

            return View(new UserViewWebDTOs
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            });



        }



    }
}
