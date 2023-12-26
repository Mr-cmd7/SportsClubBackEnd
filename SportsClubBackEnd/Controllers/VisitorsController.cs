using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClubBackEnd.Models;
using System.Linq;

[Route("api/visitor")]
[ApiController]
public class VisitorsController : ControllerBase
{
    private DataContext _context;

    public VisitorsController(DataContext context)
    {
        _context = context;
    }

    // GET: api/visitor
    [HttpGet]
    public IActionResult GetVisitors()
    {
        var visitors = _context.Visitors.ToList();
        return Ok(visitors);
    }

    // GET: api/visitor/{visitorId}
    [HttpGet("{visitorId}")]
    public IActionResult GetVisitorById(int visitorId)
    {
        var visitor = _context.Visitors.Find(visitorId);
        if (visitor == null)
        {
            return NotFound();
        }
        return Ok(visitor);
    }

    // GET: api/visitor/ByAgeCategory
    [HttpGet("ByAgeCategory")]
    public IActionResult GetVisitorsByAgeCategory()
    {
        var visitorsByAgeCategory = _context.Visitors
            .GroupBy(v => new { AgeCategory = v.Age / 10 * 10 })
            .Select(group => new
            {
                AgeCategory = group.Key.AgeCategory,
                Visitors = group.ToList()
            })
            .OrderBy(result => result.AgeCategory)
            .ToList();

        return Ok(visitorsByAgeCategory);
    }

    // POST: api/visitor
    [HttpPost]
    public IActionResult PostVisitor([FromBody] Visitor visitor)
    {
        if (visitor == null)
        {
            return BadRequest("Visitor object is null");
        }

        _context.Visitors.Add(visitor);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetVisitorById), new { visitorId = visitor.VisitorId }, visitor);
    }

    // PUT: api/visitor/{visitorId}
    [HttpPut("{visitorId}")]
    public IActionResult PutVisitor(int visitorId, [FromBody] Visitor visitor)
    {
        if (visitor == null || visitor.VisitorId != visitorId)
        {
            return BadRequest("Invalid request");
        }

        var existingVisitor = _context.Visitors.Find(visitorId);
        if (existingVisitor == null)
        {
            return NotFound();
        }

        existingVisitor.FirstName = visitor.FirstName;
        existingVisitor.Age = visitor.Age;

        _context.SaveChanges();

        return Ok(existingVisitor);
    }

    // DELETE: api/visitor/{visitorId}
    [HttpDelete("{visitorId}")]
    public IActionResult DeleteVisitor(int visitorId)
    {
        var visitor = _context.Visitors.Find(visitorId);
        if (visitor == null)
        {
            return NotFound();
        }

        _context.Visitors.Remove(visitor);
        _context.SaveChanges();

        return Ok(visitor);
    }
}
