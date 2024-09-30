using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Net.Http;
using WeddingRegistration.Models;

namespace WeddingRegistration.Controllers
{
    public class RegistrationController : Controller
    {
        private static List<Registration> registrations = [];

        private readonly IHttpClientFactory _httpClientFactory;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        /*
         * [HttpPost]
        public async Task<IActionResult> CreateRegistration(Registration registration)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                HttpResponseMessage response = await client
                    .PostAsJsonAsync($"{Constants.API_URL}/v1/Product/CreateProduct", product);

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
        */
        [HttpPost]
        public IActionResult Index(Registration registration)
        {

            // ModelState will be invalid if any of the constraints are not met
            if (ModelState.IsValid)
            {
                registrations.Add(registration);
                return View(new Registration());
            }
            return View(registration);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
