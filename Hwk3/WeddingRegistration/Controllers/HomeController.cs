using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Net.Http;
using WeddingRegistration.Models;
using WeddingRegistration.Shared.Models;

namespace WeddingRegistration.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger<HomeController> _logger;

        public HomeController(IHttpClientFactory httpClientFactory, 
            ILogger<HomeController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(String id)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            try
            {
                Registration? response = await client
                .GetFromJsonAsync<Registration>($"{Constants.API_URL}/v1/Home/GetRegistrationFromId?registrationId={id}");

                if (response == null)
                {
                    throw new HttpRequestException("Registration not found");
                }

                return View(response);
            }
            catch (Exception e)
            {
#if DEBUG
                Debugger.Break();
#endif
                Console.Error.WriteLine("Message: " + e.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Registration());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegistration(Registration registration)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                HttpResponseMessage response = await client
                    .PostAsJsonAsync($"{Constants.API_URL}/v1/Home/CreateRegistration", registration);

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
                Console.Error.WriteLine("Message: " + e.Message);
                return View("Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient client = _httpClientFactory.CreateClient();

            try
            {
                IEnumerable<Registration>? response = await client
                    .GetFromJsonAsync<IEnumerable<Registration>>($"{Constants.API_URL}/v1/Home/GetAllRegistrations");

                if (response == null)
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
                Console.Error.WriteLine("Message: " + e.Message);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRegistration(Registration registration)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                HttpResponseMessage response = await client
                    .DeleteAsync($"{Constants.API_URL}/v1/Home/DeleteRegistration?registrationId={registration.Id}");

                if (response.IsSuccessStatusCode == false)
                {
                    throw new HttpRequestException("Registration not removed");
                }

                return RedirectToAction("Index");
            }
            catch (HttpRequestException e)
            {
#if DEBUG
                Debugger.Break();
#endif
                Console.Error.WriteLine("Message: " + e.Message);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRegistration(Registration registration)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                HttpResponseMessage response = await client
                    .PutAsJsonAsync($"{Constants.API_URL}/v1/Home/UpdateRegistration", registration);

                if (response.IsSuccessStatusCode == false)
                {
                    throw new HttpRequestException("Registration not updated");
                }

                return RedirectToAction("Detail", new { id = registration.Id});
            }
            catch (HttpRequestException e)
            {
#if DEBUG
                Debugger.Break();
#endif
                Console.Error.WriteLine("Message: " + e.Message);
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
