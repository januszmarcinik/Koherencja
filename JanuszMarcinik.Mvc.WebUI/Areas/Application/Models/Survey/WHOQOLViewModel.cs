using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Survey
{
    public class WHOQOLViewModel
    {
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }
        public List<WHOQOLQuestionViewModel> Questions { get; set; }

        public List<int> SelectedValues { get; set; }
        public List<int> UnselectedQuestions { get; set; }

        public void SetQuestionnaire(Questionnaire questionnaire, List<int> selectedAnswers = null)
        {
            QuestionnaireName = $"{questionnaire.OrderNumber}. {questionnaire.Name}";
            QuestionnaireId = questionnaire.QuestionnaireId;
            Questions = new List<WHOQOLQuestionViewModel>();

            foreach (var question in questionnaire.Questions.OrderBy(x => x.OrderNumber))
            {
                var questionViewModel = new WHOQOLQuestionViewModel()
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
                        Text = answer.Value.ToString(),
                        Value = answer.AnswerId.ToString(),
                        Selected = selectedAnswers != null ? selectedAnswers.Any(x => x == answer.AnswerId) : false,
                        Description = answer.Description
                    });
                }

                Questions.Add(questionViewModel);
            }
        }
    }

    public class WHOQOLQuestionViewModel
    {
        public int QuestionId { get; set; }
        public int OrderNumber { get; set; }
        public string Text { get; set; }

        [Required]
        public int AnswerId { get; set; }
        public List<DescriptionedSelectListItem> Answers { get; set; }
    }

    public class DescriptionedSelectListItem : SelectListItem
    {
        public string Description { get; set; }
    }
}