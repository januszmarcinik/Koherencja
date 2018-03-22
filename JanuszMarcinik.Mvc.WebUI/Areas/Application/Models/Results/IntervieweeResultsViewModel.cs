using JanuszMarcinik.Mvc.Domain.Application.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class IntervieweeResultsViewModel
    {
        [Display(Name = "Wiek")]
        public string Age { get; set; }
        public int? AgeId { get; set; }

        [Display(Name = "Płeć")]
        public string Sex { get; set; }
        public int? SexId { get; set; }

        [Display(Name = "Staż pracy")]
        public string Seniority { get; set; }
        public int? SeniorityId { get; set; }

        [Display(Name = "Miejsce zamieszkania")]
        public string PlaceOfResidence { get; set; }
        public int? PlaceOfResidenceId { get; set; }

        [Display(Name = "Wykształcenie")]
        public string Education { get; set; }
        public int? EducationId { get; set; }

        [Display(Name = "Stan cywilny")]
        public string MartialStatus { get; set; }
        public int? MartialStatusId { get; set; }

        [Display(Name = "Ocena swojego stanu materialnego")]
        public string MaterialStatus { get; set; }
        public int? MaterialStatusId { get; set; }

        [Display(Name = "Miejsce pracy")]
        public string Workplace { get; set; }
        public int? WorkplaceId { get; set; }

        public List<IntervieweeQuestionnaireResult> IntervieweeQuestionnaireResults { get; set; }
        public List<IntervieweeDetail> IntervieweeDetails { get; set; }
        public List<PearsonCorrelation> PearsonCorrelations { get; set; }
        public List<DictionaryChart> DictionaryCharts { get; set; }

        public LegendViewModel Legend { get; set; }
    }
}