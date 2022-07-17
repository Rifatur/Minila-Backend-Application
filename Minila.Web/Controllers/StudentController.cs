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

    }
}
