using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Net.Http;
using WeddingRegistration.Models;
using WeddingRegistration.Shared.Models;

namespace WeddingRegistration.Controllers
{
    public class RegistrationController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegistration(Registration registration)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                HttpResponseMessage response = await client
                    .PostAsJsonAsync($"{Constants.API_URL}/v1/Registration/CreateRegistration", registration);

                if (response.IsSuccessStatusCode == false)
                {
                    throw new HttpRequestException("Registration not created");
                }

                return RedirectToAction("List");
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
        public async Task<IActionResult> List()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            try
            {
                IEnumerable<Registration>? response = await client
                    .GetFromJsonAsync<IEnumerable<Registration>>($"{Constants.API_URL}/v1/Registration/GetAllRegistrations");

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
                    .DeleteAsync($"{Constants.API_URL}/v1/Registration/DeleteRegistration?registrationId={registration.Id}");

                if (response.IsSuccessStatusCode == false)
                {
                    throw new HttpRequestException("Registration not removed");
                }

                return RedirectToAction("List");
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
