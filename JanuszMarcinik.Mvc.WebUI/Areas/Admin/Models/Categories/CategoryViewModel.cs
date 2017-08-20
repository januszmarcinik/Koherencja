using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.DataSource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Categories
{
    public class CategoryViewModel
    {
        [Grid(DataType = GridDataType.PrimaryKey)]
        public int CategoryId { get; set; }

        [Required]
        [Grid(Order = 2)]
        [Display(Name = "Nazwa")]
        [StringLength(50, ErrorMessage = "Nazwa kategorii musi zawierać od 3 do 50 znaków.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Ankieta")]
        public int QuestionnaireId { get; set; }

        [Display(Name = "Ankieta")]
        [Grid(Order = 1)]
        public string QuestionnaireName
        {
            get { return this.Questionnaire != null ? this.Questionnaire.Name : string.Empty; }
        }

        public Questionnaire Questionnaire { get; set; }
        public IEnumerable<SelectListItem> Questionnaires { get; set; }

        public void SetQuestionnaires(IEnumerable<Questionnaire> questionnaires)
        {
            this.Questionnaires = questionnaires
                .Select(x => new SelectListItem() { Value = x.QuestionnaireId.ToString(), Text = x.Name });
        }

        public bool DeleteDisabled { get; set; }
    }
}