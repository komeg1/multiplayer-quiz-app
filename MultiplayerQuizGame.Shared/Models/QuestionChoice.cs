using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models
{
    public class QuestionChoice
    {
        public int Id { get; set; }
        public required int QuestionId { get; set;}
        public Question Question { get; set;}
        public required bool IsTrue { get; set; }
        public required string ChoiceDescription { get; set; }
    }
}