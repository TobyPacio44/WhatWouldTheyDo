using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}