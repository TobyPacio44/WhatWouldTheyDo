using System.Collections.Generic;

namespace MauiApp1
{
    public class Question
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required List<Answer> Answers { get; set; } = new();
    }

}