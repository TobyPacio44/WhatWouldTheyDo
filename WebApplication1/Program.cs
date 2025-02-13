using Database;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Dodaj kontekst bazy danych
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();  // U¿yj UI dla Swaggera
}

// Middleware
app.UseRouting();
app.MapControllers();

//Test if works
//SeedDatabase(app);

app.Run();

void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

    var questionsWithAnswers = context.Questions
                                      .Include(q => q.Answers)
                                      .Select(q => new
                                      {
                                          QuestionTitle = q.Title,
                                          Answers = q.Answers.Select(a => a.answerText).ToList()
                                      })
                                      .ToList();

    foreach (var question in questionsWithAnswers)
    {
        Console.WriteLine($"Pytanie: {question.QuestionTitle}");
        int i = 1;
        foreach (var answer in question.Answers)
        {
            Console.WriteLine($"{i}. {answer}");
            i++;
        }
    }
}