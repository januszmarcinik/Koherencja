using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Survey
{
    public class SOC29ViewModel
    {
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }
        public List<SOC29QuestionViewModel> Questions { get; set; }
        public List<int> SelectedValues { get; set; }

        public void SetQuestionnaire(Questionnaire questionnaire, List<int> selectedAnswers = null)
        {
            QuestionnaireId = questionnaire.QuestionnaireId;
            QuestionnaireName = questionnaire.Name;
            Questions = new List<SOC29QuestionViewModel>();

            foreach (var question in questionnaire.Questions.OrderBy(x => x.OrderNumber))
            {
                var questionViewModel = new SOC29QuestionViewModel()
                {
                    QuestionId = question.QuestionId,
                    OrderNumber = question.OrderNumber,
                    Text = question.Text,
                    Answers = new List<DescriptionedSelectListItem>()
                };

                foreach (var answer in question.Answers.OrderBy(x => x.OrderNumber))
                {
                    questionViewModel.Answers.Add(new DescriptionedSelectListItem()
                    {
                        Text = $"{answer.Value}",
                        Value = answer.AnswerId.ToString(),
                        Selected = selectedAnswers != null ? selectedAnswers.Any(x => x == answer.AnswerId) : false,
                        Description = answer.Description
                    });
                }

                this.Questions.Add(questionViewModel);
            }
        }
    }

    public class SOC29QuestionViewModel
    {
        public int QuestionId { get; set; }
        public int OrderNumber { get; set; }
        public string Text { get; set; }

        [Required]
        public int AnswerId { get; set; }
        public List<DescriptionedSelectListItem> Answers { get; set; }
    }

    public class SOC29DescriptionedSelectListItem : SelectListItem
    {
        public string Description { get; set; }
    }
}
