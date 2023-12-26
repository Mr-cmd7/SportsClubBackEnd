using Microsoft.AspNetCore.Mvc;
using SportsClubBackEnd.Models;

[Route("api/cost")]
[ApiController]
public class CalculateCostController : ControllerBase
{
    private DataContext _context;

    public CalculateCostController(DataContext context)
    {
        _context = context;
    }

    // Рассчитать стоимость услуг за определенный промежуток времени
    [HttpGet("CalculateCost/{startTime}/{endTime}")]
    public IActionResult CalculateCost(DateTime startTime, DateTime endTime)
    {
        var cost = _context.Schedules
            .Where(s => s.StartTime >= startTime && s.EndTime <= endTime && s.Conducted == true)
            .Sum(s => s.Service.Cost);

        return Ok(cost);
    }
}
