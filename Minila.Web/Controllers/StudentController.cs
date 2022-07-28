using Microsoft.AspNetCore.Mvc;
using Minila.Web.Models;
using MinilaDataAcess.Model;
using Newtonsoft.Json;

namespace Minila.Web.Controllers
{
    public class StudentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<StudentWebModel> Studentlist = new List<StudentWebModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage response = await client.GetAsync("Student/GetStudents");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                Studentlist = JsonConvert.DeserializeObject<List<StudentWebModel>>(res);
            }
            ViewBag.TotalStudentlist = Studentlist.Count();

            return View(Studentlist);
        }
        [HttpGet]
        public async Task<IActionResult> StudentDetails(long id)
        {

            Student details = new Student();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            HttpResponseMessage response = await client.GetAsync($"Student/GetByStudentID?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                details = JsonConvert.DeserializeObject<Student>(res);
            }
            //getting All  List Of Road Way ... 
            List<RoadWayWebModel> RWlist = new List<RoadWayWebModel>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7211/Roadway/GetRoadway"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RWlist = JsonConvert.DeserializeObject<List<RoadWayWebModel>>(apiRespose);
                }
            }
            ViewData["Roadlist"] = RWlist;

            //getting School List .. 
            List<SchoolWebModel> Schoollist = new List<SchoolWebModel>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7211/School/GetSchool"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Schoollist = JsonConvert.DeserializeObject<List<SchoolWebModel>>(apiRespose);
                }
            }
            ViewData["school"] = Schoollist;
            //getting Request List
            List<TripRequest> TripRequestlist = new List<TripRequest>();
            var studentStringID = id.ToString();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync($"https://localhost:7211/TripRequest/GetRequestByStudent?StudentID={studentStringID}"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    TripRequestlist = JsonConvert.DeserializeObject<List<TripRequest>>(apiRespose);
                }
            }
            ViewData["RideRequest"] = TripRequestlist;

            return View(details);
        }

        [HttpGet]
        public async Task<IActionResult> CreateStudent()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);
            ViewBag.Code = "STD" + myRandomNo;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentWebModel student)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7211/");
            var response = await client.PostAsJsonAsync<StudentWebModel>("Student/AddStudent", student);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "Student");
            }
            return View();
        }


        //get Rider

        [HttpGet]
        public async Task<IActionResult> FindChauffeur(int roadid, int SchholId, int studentid)
        {
            //getting School List .. 
            List<FindRider> GetMyRiderList = new List<FindRider>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync($"https://localhost:7211/Student/GetRider?roadid={roadid}&schoolid={SchholId}"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    GetMyRiderList = JsonConvert.DeserializeObject<List<FindRider>>(apiRespose);
                }
            }
            ViewBag.TotalRiderListCount = GetMyRiderList.Count();
            //getting All  List Of Road Way ... 
            List<RoadWayWebModel> RWlist = new List<RoadWayWebModel>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7211/Roadway/GetRoadway"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    RWlist = JsonConvert.DeserializeObject<List<RoadWayWebModel>>(apiRespose);
                }
            }
            ViewData["Roadlist"] = RWlist;

            //Getting Chauffeur...
            List<ChauffeurWebModel> ChauffeuList = new List<ChauffeurWebModel>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7211/Chauffeur/GetChauffeurs"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    ChauffeuList = JsonConvert.DeserializeObject<List<ChauffeurWebModel>>(apiRespose);
                }
            }
            ViewData["ChauffeuList"] = ChauffeuList;
            //getting School List .. 
            List<SchoolWebModel> Schoollist = new List<SchoolWebModel>();
            using (var httpClient = new HttpClient())
            {
                using (var getResponse = await httpClient.GetAsync("https://localhost:7211/School/GetSchool"))
                {
                    string apiRespose = await getResponse.Content.ReadAsStringAsync();
                    Schoollist = JsonConvert.DeserializeObject<List<SchoolWebModel>>(apiRespose);
                }
            }
            ViewData["school"] = Schoollist;

            //Static Values 

            ViewData["studentId"] = studentid;
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            ViewBag.Date = currentDate;
            DateTime now = DateTime.Now;
            DateTime utc = DateTime.UtcNow;
            ViewBag.time = now.ToString("T");
            return View(GetMyRiderList);
        }
    }
}
