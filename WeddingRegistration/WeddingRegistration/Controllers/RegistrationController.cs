using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeddingRegistration.Models;

namespace WeddingRegistration.Controllers
{
    public class RegistrationController : Controller
    {
        private static List<Registration> registrations;
        
        public static List<Registration> GetRegistrations() => registrations;

        
    }
}
