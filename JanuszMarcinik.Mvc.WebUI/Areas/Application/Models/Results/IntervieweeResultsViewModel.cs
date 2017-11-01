using JanuszMarcinik.Mvc.Domain.Application.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class IntervieweeResultsViewModel
    {
        [Display(Name = "Wiek")]
        public string Age { get; set; }

        [Display(Name = "Płeć")]
        public string Sex { get; set; }

        [Display(Name = "Staż pracy")]
        public string Seniority { get; set; }

        [Display(Name = "Miejsce zamieszkania")]
        public string PlaceOfResidence { get; set; }

        [Display(Name = "Wykształcenie")]
        public string Education { get; set; }

        [Display(Name = "Stan cywilny")]
        public string MartialStatus { get; set; }

        [Display(Name = "Ocena swojego stanu materialnego")]
        public string MaterialStatus { get; set; }

        public List<IntervieweeQuestionnaireResult> IntervieweeQuestionnaireResults { get; set; }
    }
}