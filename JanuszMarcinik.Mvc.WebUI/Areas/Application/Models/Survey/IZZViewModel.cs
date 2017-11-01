using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Survey
{
    public class IZZViewModel
    {
        public IZZViewModel() { }

        public IZZViewModel(Questionnaire questionnaire)
        {
            QuestionnaireId = questionnaire.QuestionnaireId;
            QuestionnaireName = $"{questionnaire.OrderNumber}. {questionnaire.Name}";

            var questions = questionnaire.Questions.OrderBy(x => x.OrderNumber).ToList();

            Questions = new List<IZZQuestionViewModel>();
            foreach (var question in questions)
            {
                Questions.Add(new IZZQuestionViewModel()
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
        public List<IZZQuestionViewModel> Questions { get; set; }
    }

    public class IZZQuestionViewModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Text { get; set; }

        [Required(ErrorMessage = "Należy wpisać wartość")]
        [Range(minimum: 1, maximum: 5, ErrorMessage = "Prawidłowa wartość mieści się w przedziale od 0 do 4.")]
        public int? Value { get; set; }
    }
}