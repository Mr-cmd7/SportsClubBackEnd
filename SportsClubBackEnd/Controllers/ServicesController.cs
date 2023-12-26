using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClubBackEnd.Models;
using System.Linq;

namespace SportsClubBackEnd.Controllers
{
    [ApiController]
    [Route("api/service")]
    public class ServicesController : ControllerBase
    {
        private DataContext _context;

        public ServicesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/service
        [HttpGet]
        public IActionResult GetServices()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }

        // GET: api/service/{serviceId}
        [HttpGet("{serviceId}")]
        public IActionResult GetServiceById(int serviceId)
        {
            var service = _context.Services.Find(serviceId);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        // POST: api/service
        [HttpPost]
        public IActionResult PostService([FromBody] Service service)
        {
            if (service == null)
            {
                return BadRequest("Service object is null");
            }

            _context.Services.Add(service);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetServiceById), new { serviceId = service.ServiceId }, service);
        }

        // PUT: api/service/{serviceId}
        [HttpPut("{serviceId}")]
        public IActionResult PutService(int serviceId, [FromBody] Service service)
        {
            if (service == null || service.ServiceId != serviceId)
            {
                return BadRequest("Invalid request");
            }

            var existingService = _context.Services.Find(serviceId);
            if (existingService == null)
            {
                return NotFound();
            }

            existingService.ServiceName = service.ServiceName;

            _context.SaveChanges();

            return Ok(existingService);
        }

        // DELETE: api/service/{serviceId}
        [HttpDelete("{serviceId}")]
        public IActionResult DeleteService(int serviceId)
        {
            var service = _context.Services.Find(serviceId);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            _context.SaveChanges();

            return Ok(service);
        }
    }
}
