using JanuszMarcinik.Mvc.Domain.Application.Models;
using JanuszMarcinik.Mvc.Domain.DataSource;
using System.ComponentModel.DataAnnotations;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Questionnaires
{
    public class QuestionnaireViewModel
    {
        [Grid(DataType = GridDataType.PrimaryKey)]
        public int QuestionnaireId { get; set; }

        [Required]
        [Grid(Order = 1)]
        [Display(Name = "Kolejność")]
        public int OrderNumber { get; set; }

        [Required]
        [Grid(Order = 2)]
        [Display(Name = "Nazwa")]
        [StringLength(50, ErrorMessage = "Nazwa ankiety musi zawierać od 3 do 50 znaków.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Grid(Order = 3, DataType = GridDataType.Boolean)]
        [Display(Name = "Aktywna")]
        public bool IsActive { get; set; }

        [Required]
        [Grid(Order = 4)]
        [Display(Name = "Typ klucza")]
        public KeyType KeyType { get; set; }
    }
}