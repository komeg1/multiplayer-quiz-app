using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public required bool IsActive { get; set; } 
        public required List<QuestionChoice> QuestionChoices { get; set; }
        public List<Quiz> Quizzes { get; set; }

    }
}