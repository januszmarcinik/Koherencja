using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class DictionaryItemViewModel
    {
        public string ItemName { get; set; }
        public string Badge { get; set; }

        public List<ValueViewModel> Values { get; set; }
    }
}