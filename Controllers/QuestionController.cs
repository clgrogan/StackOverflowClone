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
  public class QuestionController : ControllerBase
  {
    private readonly DatabaseContext db;

    public QuestionController(DatabaseContext context)
    {
      db = context;
    }

    // GET: api/Question
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
    {
      return await db.Questions.ToListAsync();
    }

    [HttpGet("Search/{searchTerm}")]
    public ActionResult SearchQuestions(string searchTerm)
    {
      var results = db.Questions
        .Where(question =>
            question.Title.ToLower().Contains(searchTerm.ToLower()) ||
            question.QuestionBody.ToLower().Contains(searchTerm.ToLower())
        );
      return Ok(results);
    }

    // GET: api/Question/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> GetQuestion(int id)
    {

      // var student = db.Students.Include(i => i.ProgressReports).FirstOrDefault(st => st.Id == id);
      var question = await db
        .Questions
        .Include(i => i.Answers)
        .FirstOrDefaultAsync(f => f.ID == id);

      if (question == null)
      {
        return NotFound();
      }

      else
      {
        // create json object from model view
        var rv = new QuestionDetails
        {
          ID = question.ID,
          Title = question.Title,
          QuestionBody = question.QuestionBody,
          VoteScore = question.VoteScore,
          TimeStamp = question.TimeStamp,
          Answers = question.Answers.Select(a => new CreatedAnswer
          {
            ID = a.ID,
            AnswerBody = a.AnswerBody,
            VoteScore = a.VoteScore,
            TimeStamp = a.TimeStamp,
            QuestionId = a.QuestionId
          }).ToList()
        };
        return Ok(rv);
      }

    }

    // PUT: api/Question/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.

    // No Update Enabled on Question, commenting out the Put endpoint code
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuestion(int id, Question question)
    {
      if (id != question.ID)
      {
        return BadRequest();
      }

      db.Entry(question).State = EntityState.Modified;

      try
      {
        await db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!QuestionExists(id))
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

    // POST: api/Question
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    // [HttpPost]
    // public async Task<ActionResult<Question>> PostQuestion(Question question)
    // {
    //   db.Questions.Add(question);
    //   await db.SaveChangesAsync();

    //   return CreatedAtAction("GetQuestion", new { id = question.ID }, question);
    // }    
    [HttpPost]
    public async Task<ActionResult<NewQuestion>> PostQuestion(NewQuestion question)
    {
      var qr = new Question
      {
        Title = question.Title,
        QuestionBody = question.QuestionBody,
        TimeStamp = DateTime.UtcNow
      };
      db.Questions.Add(qr);
      await db.SaveChangesAsync();

      return CreatedAtAction("GetQuestion", new { id = question.ID }, question);
    }

    // DELETE: api/Question/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Question>> DeleteQuestion(int id)
    {
      var question = await db.Questions.FindAsync(id);
      if (question == null)
      {
        return NotFound();
      }

      db.Questions.Remove(question);
      await db.SaveChangesAsync();

      return question;
    }

    private bool QuestionExists(int id)
    {
      return db.Questions.Any(e => e.ID == id);
    }
  }
}
