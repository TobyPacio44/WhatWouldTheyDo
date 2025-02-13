using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace MauiApp1 { 
    public class Answer
    {
        public int Id { get; set; }
        public required string answerText { get; set; }
        public ObservableCollection<Character> Characters { get; set; } = new();
    }
}