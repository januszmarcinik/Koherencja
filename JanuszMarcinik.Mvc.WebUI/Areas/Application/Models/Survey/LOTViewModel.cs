using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Survey
{
    public class LOTViewModel
    {
        public LOTViewModel() { }

        public LOTViewModel(Questionnaire questionnaire)
        {
            QuestionnaireId = questionnaire.QuestionnaireId;
            QuestionnaireName = $"{questionnaire.OrderNumber}. {questionnaire.Name}";

            var questions = questionnaire.Questions.OrderBy(x => x.OrderNumber).ToList();

            Questions = new List<LOTQuestionViewModel>();
            foreach (var question in questions)
            {
                Questions.Add(new LOTQuestionViewModel()
                {
                    Id = question.QuestionId,
                    Text = question.Text,
                    OrderNumber = question.OrderNumber
                });
            }
        }

        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }

        public string[] ErrorProperties { get; set; }

        [Required]
        public List<LOTQuestionViewModel> Questions { get; set; }
    }

    public class LOTQuestionViewModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Text { get; set; }

        [Required(ErrorMessage = "Należy wpisać wartość")]
        [Range(minimum: 0, maximum: 4, ErrorMessage = "Prawidłowa wartość mieści się w przedziale od 0 do 4.")]
        public int? Value { get; set; }
    }
}