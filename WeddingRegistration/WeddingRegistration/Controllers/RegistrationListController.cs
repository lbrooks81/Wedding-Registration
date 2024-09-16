using Microsoft.AspNetCore.Mvc;
using WeddingRegistration.Models;

namespace WeddingRegistration.Controllers
{
    public class RegistrationListController : Controller
    {
        public IActionResult List()
        {
            List<Registration> registrations = RegistrationController.GetRegistrations();
            return View(registrations);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
