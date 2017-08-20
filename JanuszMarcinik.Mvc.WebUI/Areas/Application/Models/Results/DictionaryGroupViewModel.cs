using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class DictionaryGroupViewModel
    {
        public string GroupName { get; set; }
        public List<DictionaryItemViewModel> DictionaryItems { get; set; }
    }
}