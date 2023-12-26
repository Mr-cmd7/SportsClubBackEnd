using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClubBackEnd.Models;
using System.Linq;

[Route("api/schedule")]
[ApiController]
public class SchedulesController : ControllerBase
{
    private DataContext _context;

    public SchedulesController(DataContext context)
    {
        _context = context;
    }

    // GET: api/schedule
    [HttpGet]
    public IActionResult GetSchedules()
    {
        var schedules = _context.Schedules.Include(s => s.Service).Include(s => s.Trainer).Include(s => s.Visitor).ToList();
        return Ok(schedules);
    }

    // GET: api/schedule/{scheduleId}
    [HttpGet("{scheduleId}")]
    public IActionResult GetScheduleById(int scheduleId)
    {
        var schedule = _context.Schedules.Include(s => s.Service).Include(s => s.Trainer).Include(s => s.Visitor).FirstOrDefault(s => s.ScheduleId == scheduleId);
        if (schedule == null)
        {
            return NotFound();
        }
        return Ok(schedule);
    }

    // GET: api/schedule/GroupSessions
    [HttpGet("GroupSessions")]
    public IActionResult GetGroupSessions()
    {
        var groupSessions = _context.Schedules
            .Where(s => s.Service != null && s.Service.ServiceName.ToLower().Contains("групповое"))
            .Select(s => new
            {
                ScheduleId = s.ScheduleId,
                ServiceName = s.Service.ServiceName,
                TrainerName = $"{s.Trainer.FirstName} {s.Trainer.LastName}",
                DayOfWeek = s.DayOfWeek,
                StartTime = s.StartTime,
                EndTime = s.EndTime
            })
            .ToList();

        return Ok(groupSessions);
    }

    // GET: api/schedule/GroupSessionLists
    [HttpGet("GroupSessionLists")]
    public IActionResult GetGroupSessionLists()
    {
        var groupSessionLists = _context.Schedules
            .Where(s => s.Service != null && s.Service.ServiceName.ToLower().Contains("групповое"))
            .GroupBy(s => s.Service.ServiceName)
            .Select(group => new
            {
                ServiceName = group.Key,
                GroupSessionLists = group.Select(s => new
                {
                    ScheduleId = s.ScheduleId,
                    TrainerName = $"{s.Trainer.FirstName} {s.Trainer.LastName}",
                    DayOfWeek = s.DayOfWeek,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime
                }).ToList()
            })
            .ToList();

        return Ok(groupSessionLists);
    }

    // POST: api/schedule
    [HttpPost]
    public IActionResult PostSchedule([FromBody] Schedule schedule)
    {
        if (schedule == null)
        {
            return BadRequest("Schedule object is null");
        }

        _context.Schedules.Add(schedule);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetScheduleById), new { scheduleId = schedule.ScheduleId }, schedule);
    }

    // PUT: api/schedule/{scheduleId}
    [HttpPut("{scheduleId}")]
    public IActionResult PutSchedule(int scheduleId, [FromBody] Schedule schedule)
    {
        if (schedule == null || schedule.ScheduleId != scheduleId)
        {
            return BadRequest("Invalid request");
        }

        var existingSchedule = _context.Schedules.Find(scheduleId);
        if (existingSchedule == null)
        {
            return NotFound();
        }

        existingSchedule.DayOfWeek = schedule.DayOfWeek;
        existingSchedule.StartTime = schedule.StartTime;
        existingSchedule.EndTime = schedule.EndTime;
        // Update other properties as needed

        _context.SaveChanges();

        return Ok(existingSchedule);
    }

    // DELETE: api/schedule/{scheduleId}
    [HttpDelete("{scheduleId}")]
    public IActionResult DeleteSchedule(int scheduleId)
    {
        var schedule = _context.Schedules.Find(scheduleId);
        if (schedule == null)
        {
            return NotFound();
        }

        _context.Schedules.Remove(schedule);
        _context.SaveChanges();

        return Ok(schedule);
    }
}
