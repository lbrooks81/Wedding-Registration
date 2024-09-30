using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WedReg.Shared.Models;

namespace WedReg.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly ILogger<RegistrationController> _logger;
        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllRegistrations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Registration> GetAllRegistrations()
        {
            return DB.Registrations;
        }

        [HttpGet("GetRegistrationFromId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Registration? GetRegistrationFromId([Required][FromQuery] int registrationId)
        {
            return DB.Registrations
                .FirstOrDefault(r => r.registrationId = registrationId);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
