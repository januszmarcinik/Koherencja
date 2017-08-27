using System.Collections.Generic;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class ResultsViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public List<string> Options { get; set; }
        public List<DictionaryGroupViewModel> DictionaryGroups { get; set; }

        public ActionResult Action { get; set; }
    }
}