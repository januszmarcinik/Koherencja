using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class QuestionnaireResultsViewModel
    {
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }
        public List<QuestionResultsViewModel> QuestionResults { get; set; }
    }
}