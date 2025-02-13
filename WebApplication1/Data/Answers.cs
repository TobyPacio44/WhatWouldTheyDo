using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string answerText { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

    }
}