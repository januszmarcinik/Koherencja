using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.DataSource;
using System.ComponentModel.DataAnnotations;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Dictionaries
{
    public class DictionaryViewModel
    {
        [Grid(DataType = GridDataType.PrimaryKey)]
        public int BaseDictionaryId { get; set; }

        [Required]
        [Grid(Order = 1, DataType = GridDataType.Enum)]
        [Display(Name = "Typ słownika")]
        public DictionaryType DictionaryType { get; set; }

        [Required]
        [Grid(Order = 2)]
        [Display(Name = "Wartość")]
        public string Value { get; set; }
    }
}