using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using WedReg.Models;
using WedReg.Shared.Models;

namespace WedReg.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                IEnumerable<Registration>? response = await client
                    .GetFromJsonAsync<IEnumerable<Registration>>
                    ($"{Constants.API_URL}/v1/Index/GetAllRegistrations");
                if(response == null) 
                {
                    throw new HttpRequestException("Registrations not found");
                }

                return View(response.ToList());
            }
            catch (Exception e)
            {
#if DEBUG
                Debugger.Break();
#endif
                Console.Error.WriteLine(e.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult AddReg()
        {
            return View(new Registration());
        }


        [HttpPost]
        public async Task<IActionResult> AddReg(Registration registration)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                HttpResponseMessage response = await client
                    .PostAsJsonAsync($"{Constants.API_URL}/v1/Index/AddReg", registration);
                
                if (response.IsSuccessStatusCode == false)
                {
                    throw new HttpRequestException("Registration not created");
                }

                return RedirectToAction("Index");
            }
            catch (HttpRequestException e)
            {
#if DEBUG
                Debugger.Break();
#endif
                Console.Error.WriteLine(e.Message);
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
