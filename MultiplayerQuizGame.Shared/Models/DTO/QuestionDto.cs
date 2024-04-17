using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerQuizGame.Shared.Models.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public  List<QuestionChoiceDto> QuestionChoices { get; set; }
    }
}
