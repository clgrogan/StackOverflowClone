using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflowClone.Models;
using StackOverflowClone.ViewModels;

namespace StackOverflowClone.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AnswerController : ControllerBase
  {
    private readonly DatabaseContext db;

    public AnswerController(DatabaseContext context)
    {
      db = context;
    }

    // GET: api/Answer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
    {
      return await db.Answers.ToListAsync();
    }

    // GET: api/Answer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Answer>> GetAnswer(int id)
    {
      var answer = await db.Answers.FindAsync(id);

      if (answer == null)
      {
        return NotFound();
      }

      return answer;
    }

    // PUT: api/Answer/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    // public async Task<IActionResult> PutAnswer(int id, Answer answer)
    public async Task<IActionResult> PutAnswer(int id, UpdateAnswerVote answer)
    {
      if (id != answer.ID)
      {
        return BadRequest();
      }

      db.Entry(answer).State = EntityState.Modified;

      try
      {
        await db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AnswerExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Answer
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<NewAnswer>> PostAnswer(NewAnswer answer)
    {
      var ar = new Answer
      {
        QuestionId = answer.QuestionId,
        AnswerBody = answer.AnswerBody,
        TimeStamp = DateTime.UtcNow
      };

      db.Answers.Add(ar);
      await db.SaveChangesAsync();

      return CreatedAtAction("GetAnswer", new { id = answer.ID }, answer);
    }

    // DELETE: api/Answer/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Answer>> DeleteAnswer(int id)
    {
      var answer = await db.Answers.FindAsync(id);
      if (answer == null)
      {
        return NotFound();
      }

      db.Answers.Remove(answer);
      await db.SaveChangesAsync();

      return answer;
    }

    private bool AnswerExists(int id)
    {
      return db.Answers.Any(e => e.ID == id);
    }
  }
}
