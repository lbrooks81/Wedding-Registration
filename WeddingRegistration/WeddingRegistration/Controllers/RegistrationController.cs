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
        public IActionResult AddRegistration(Registration registration) 
        {
            // ModelState will be invalid if any of the constraints are not met
            if (ModelState.IsValid) 
            {
                registrations.Add(registration);
            }
            return RedirectToAction("Index");
        }


    }
}
