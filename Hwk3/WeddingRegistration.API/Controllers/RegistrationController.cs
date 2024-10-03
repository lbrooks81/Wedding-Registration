using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeddingRegistration.Shared.Models;

namespace WeddingRegistration.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class HomeController : Controller
    {
        private static List<Registration> registrations =
        [
            new Registration("Logan", "Brooks", 4, "Champagne"),
            new Registration("James", "Rangle", 2, "None"),
            new Registration("Shane", "Bola", 5, "Crib")
        ];

        [HttpGet("GetAllRegistrations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Registration> GetAllRegistrations() 
        {
            return registrations;
        }

        [HttpGet("GetRegistrationFromId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Registration? GetRegistrationFromId([Required][FromQuery]int registrationId)
        {
            return registrations.FirstOrDefault(r => r.Id == registrationId);
        }

        [HttpPut("UpdateRegistration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRegistration([Required][FromBody] Registration registration)
        {
            Registration? existingRegistration = registrations
                .FirstOrDefault(r => r.Id == registration.Id);
            if (existingRegistration == null)
            {
                return NotFound();
            }

            existingRegistration.FirstName = registration.FirstName;
            existingRegistration.LastName = registration.LastName;
            existingRegistration.NumberOfGuests = registration.NumberOfGuests;
            existingRegistration.Gifts = registration.Gifts;
            
            return Ok(existingRegistration);
        }

        [HttpPost("CreateRegistration")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateRegistration([Required][FromBody] Registration registration)
        {
            registration.Id = registrations.Max(r => r.Id) + 1;
            registrations.Add(registration);

            return CreatedAtAction(nameof(GetRegistrationFromId), new { Id = registration.Id }, registration);
        }

        [HttpDelete("DeleteRegistration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteRegistration([Required][FromQuery] int registrationId)
        {
            Registration? registration = registrations
                .FirstOrDefault(r => r.Id == registrationId);
            if(registration == null) 
            { 
                return NotFound();
            }

            registrations.Remove(registration);

            return Ok();
        }

    }
}
