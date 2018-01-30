using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries
{
    [Table("BaseDictionaries", Schema = "Dictionaries")]
    public class BaseDictionary : IApplicationEntity
    {
        public BaseDictionary()
        {
            Sexes = new HashSet<Interviewee>();
            Seniorities = new HashSet<Interviewee>();
            Educations = new HashSet<Interviewee>();
            PlaceOfResidences = new HashSet<Interviewee>();
            MartialStatuses = new HashSet<Interviewee>();
            MaterialStatuses = new HashSet<Interviewee>();
            Ages = new HashSet<Interviewee>();
            Workplaces = new HashSet<Interviewee>();
        }

        public int BaseDictionaryId { get; set; }
        public DictionaryType DictionaryType { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Interviewee> Sexes { get; set; }
        public virtual ICollection<Interviewee> Seniorities { get; set; }
        public virtual ICollection<Interviewee> Educations { get; set; }
        public virtual ICollection<Interviewee> PlaceOfResidences { get; set; }
        public virtual ICollection<Interviewee> MartialStatuses { get; set; }
        public virtual ICollection<Interviewee> MaterialStatuses { get; set; }
        public virtual ICollection<Interviewee> Ages { get; set; }
        public virtual ICollection<Interviewee> Workplaces { get; set; }
    }

    public enum DictionaryType
    {
        [Description("Płeć")]
        Sex = 1,

        [Description("Staż pracy")]
        Seniority = 2,

        [Description("Wykształcenie")]
        Education = 3,

        [Description("Miejsce zamieszkania")]
        PlaceOfResidence = 4,

        [Description("Stan cywilny")]
        MartialStatus = 5,

        [Description("Ocena swojego stanu materialnego")]
        MaterialStatus = 6,

        [Description("Wiek")]
        Age = 7,

        [Description("Miejsce pracy")]
        Workplace = 8
    }
}
