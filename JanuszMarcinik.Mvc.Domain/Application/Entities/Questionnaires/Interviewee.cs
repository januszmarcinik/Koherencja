using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires
{
    [Table("Interviewees", Schema = "Questionnaire")]
    public class Interviewee : IApplicationEntity
    {
        public Interviewee()
        {
            this.Results = new HashSet<Result>();
        }

        public int IntervieweeId { get; set; }
        public DateTime InterviewDate { get; set; }

        public int AgeId { get; set; }
        [ForeignKey("AgeId")]
        public virtual BaseDictionary Age { get; set; }

        public int SexId { get; set; }
        [ForeignKey("SexId")]
        public virtual BaseDictionary Sex { get; set; }

        public int SeniorityId { get; set; }
        [ForeignKey("SeniorityId")]
        public virtual BaseDictionary Seniority { get; set; }

        public int EducationId { get; set; }
        [ForeignKey("EducationId")]
        public virtual BaseDictionary Education { get; set; }

        public int PlaceOfResidenceId { get; set; }
        [ForeignKey("PlaceOfResidenceId")]
        public virtual BaseDictionary PlaceOfResidence { get; set; }

        public int MartialStatusId { get; set; }
        [ForeignKey("MartialStatusId")]
        public virtual BaseDictionary MartialStatus { get; set; }

        public int MaterialStatusId { get; set; }
        [ForeignKey("MaterialStatusId")]
        public virtual BaseDictionary MaterialStatus { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}