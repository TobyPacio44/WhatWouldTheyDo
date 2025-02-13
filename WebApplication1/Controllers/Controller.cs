using Data;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly DataBaseContext _context;

    public QuizController(DataBaseContext context)
    {
        _context = context;
    }

    // Pobieranie wszystkich pytań
    [HttpGet("questions")]
    public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
    {
        return await _context.Questions.Include(q => q.Answers).ToListAsync();
    }

    // Pobieranie konkretnego pytania
    [HttpGet("questions/{id}")]
    public async Task<ActionResult<Question>> GetQuestion(int id)
    {
        var question = await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == id);
        if (question == null) return NotFound();
        return question;
    }
}