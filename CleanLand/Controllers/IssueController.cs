using CleanLand.Data.Data;
using CleanLand.Data.Models;

namespace CleanLand.Controllers;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public IssuesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/issues
    [HttpPost]
    public async Task<ActionResult<Issue>> Create([FromBody] IssueCreateDto dto)
    {
        var issue = new Issue
        {
            Date = dto.Date,
            Description = dto.Description,
            IsResolved = dto.IsResolved,
            IsAccepted = dto.IsAccepted,
            ForestId = dto.ForestId,
            PondId = dto.PondId
        };

        _context.Issues.Add(issue);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = issue.Id }, issue);
    }

    // GET: api/issues/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Issue>> GetById(int id)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue == null)
        {
            return NotFound();
        }

        return Ok(issue);
    }
    
    // PUT: api/issues/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] IssueCreateDto dto)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue == null)
        {
            return NotFound();
        }

        issue.Date = dto.Date;
        issue.Description = dto.Description;
        issue.IsResolved = dto.IsResolved;
        issue.IsAccepted = dto.IsAccepted;
        issue.ForestId = dto.ForestId;
        issue.PondId = dto.PondId;

        _context.Entry(issue).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // DELETE: api/issues/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue == null)
        {
            return NotFound();
        }

        _context.Issues.Remove(issue);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    public class IssueCreateDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsResolved { get; set; }
        public bool IsAccepted { get; set; }
        public int? ForestId { get; set; } = null;
        public int? PondId { get; set; } = null;
    }
}