using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.DataSource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Questions
{
    public class QuestionViewModel
    {
        [Grid(DataType = GridDataType.PrimaryKey)]
        public int QuestionId { get; set; }

        public int QuestionnaireId { get; set; }

        [Required]
        [Grid(Order = 1)]
        [Display(Name = "Kolejność")]
        public int OrderNumber { get; set; }

        [Required]
        [Grid(Order = 3)]
        [Display(Name = "Treść")]
        public string Text { get; set; }

        [Display(Name = "Kategoria")]
        public int? CategoryId { get; set; }

        [Display(Name = "Kategoria")]
        [Grid(Order = 2)]
        public string CategoryName
        {
            get { return this.Category != null ? this.Category.Name : string.Empty; }
        }

        public Category Category { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public void SetCategories(IEnumerable<Category> categories)
        {
            this.Categories = categories
                .Select(x => new SelectListItem() { Value = x.CategoryId.ToString(), Text = x.Name });
        }
    }
}