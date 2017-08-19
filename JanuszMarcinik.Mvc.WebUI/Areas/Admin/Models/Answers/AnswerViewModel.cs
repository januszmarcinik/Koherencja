using JanuszMarcinik.Mvc.Domain.DataSource;
using System.ComponentModel.DataAnnotations;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Answers
{
    public class AnswerViewModel
    {
        [Grid(DataType = GridDataType.PrimaryKey)]
        public int AnswerId { get; set; }

        public int QuestionId { get; set; }

        [Required]
        [Grid(Order = 1)]
        [Display(Name = "Kolejność")]
        public int OrderNumber { get; set; }

        [Required]
        [Grid(Order = 2)]
        [Display(Name = "Wartość")]
        public int Value { get; set; }

        [Grid(Order = 3)]
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}