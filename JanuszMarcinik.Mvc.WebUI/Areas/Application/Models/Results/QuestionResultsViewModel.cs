using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class QuestionResultsViewModel
    {
        public int QuestionId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }

        public List<string> Answers { get; set; }
        public List<DictionaryGroupViewModel> DictionaryGroups { get; set; }
    }
}