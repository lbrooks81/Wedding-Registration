using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeddingRegistration.Models;


namespace WeddingRegistration.Controllers
{
    public class RegistrationController : Controller
    {
        private static List<Registration> registrations = [];

        [HttpGet]
        public IActionResult List()
        {
            return View(registrations);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Registration());
        }



        [HttpPost]
        public IActionResult Index(Registration registration) 
        {
            if (ModelState.IsValid) 
            {
                registrations.Add(registration);
                return View(new Registration());
            }
            return View(registration);
        }
    }
}
