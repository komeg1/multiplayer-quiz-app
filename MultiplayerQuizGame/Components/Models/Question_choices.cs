using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Components.Models
{
    public class Question_choices
    {
        public int Id { get; set; }
        public required int QuestionId { get; set;}
        public required Question Question { get; set;}
        public required bool IsTrue { get; set; }
        public required string ChoiceDescription { get; set; }
    }
}