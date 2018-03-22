using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class DictionaryChart
    {
        public DictionaryType DictionaryType { get; set; }
        public Dictionary<string, int> Values { get; set; }
    }
}
