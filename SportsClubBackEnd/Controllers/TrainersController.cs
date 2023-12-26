using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClubBackEnd.Models;
using System.Linq;

[Route("api/trainer")]
[ApiController]
public class TrainersController : ControllerBase
{
    private DataContext _context;

    public TrainersController(DataContext context)
    {
        _context = context;
    }

    // GET: api/trainer
    [HttpGet]
    public IActionResult GetTrainers()
    {
        var trainers = _context.Trainers.ToList();
        return Ok(trainers);
    }

    // GET: api/trainer/{trainerId}
    [HttpGet("{trainerId}")]
    public IActionResult GetTrainerById(int trainerId)
    {
        var trainer = _context.Trainers.Find(trainerId);
        if (trainer == null)
        {
            return NotFound();
        }
        return Ok(trainer);
    }

    // GET: api/trainer/WorkSchedule
    [HttpGet("WorkSchedule")]
    public IActionResult GetTrainersWorkSchedule()
    {
        var trainersWorkSchedule = _context.Trainers
            .Select(trainer => new
            {
                TrainerId = trainer.TrainerId,
                FullName = $"{trainer.FirstName} {trainer.LastName}",
                WorkSchedule = trainer.Schedules.Select(schedule => new
                {
                    DayOfWeek = schedule.DayOfWeek,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime
                }).ToList()
            })
            .ToList();

        return Ok(trainersWorkSchedule);
    }

    // POST: api/trainer
    [HttpPost]
    public IActionResult PostTrainer([FromBody] Trainer trainer)
    {
        if (trainer == null)
        {
            return BadRequest("Trainer object is null");
        }

        _context.Trainers.Add(trainer);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetTrainerById), new { trainerId = trainer.TrainerId }, trainer);
    }

    // PUT: api/trainer/{trainerId}
    [HttpPut]
    public IActionResult PutTrainer([FromBody] Trainer trainer)
    {
        if (trainer == null)
        {
            return BadRequest("Invalid request");
        }

        var existingTrainer = _context.Trainers.Find(trainer.TrainerId);
        if (existingTrainer == null)
        {
            return NotFound();
        }

        existingTrainer.FirstName = trainer.FirstName;
        existingTrainer.LastName = trainer.LastName;

        // Update other properties as needed

        _context.SaveChanges();

        return Ok(existingTrainer);
    }

    // DELETE: api/trainer/{trainerId}
    [HttpDelete("{trainerId}")]
    public IActionResult DeleteTrainer(int trainerId)
    {
        var trainer = _context.Trainers.Find(trainerId);
        if (trainer == null)
        {
            return NotFound();
        }

        _context.Trainers.Remove(trainer);
        _context.SaveChanges();

        return Ok(trainer);
    }
}
