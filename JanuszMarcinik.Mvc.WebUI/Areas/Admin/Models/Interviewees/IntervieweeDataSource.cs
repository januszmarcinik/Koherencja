using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.DataSource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Interviewees
{
    public class IntervieweeDataSource : DataSource<IntervieweeViewModel>
    {
        #region Filters
        [Display(Name = "Data od")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }
        [Display(Name = "Data do")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateTo { get; set; }

        [Display(Name = "Wiek")]
        public int? AgeId { get; set; }
        public IEnumerable<SelectListItem> Ages { get; set; }

        [Display(Name = "Płeć")]
        public int? SexId { get; set; }
        public IEnumerable<SelectListItem> Sexes { get; set; }

        [Display(Name = "Staż pracy")]
        public int? SeniorityId { get; set; }
        public IEnumerable<SelectListItem> Seniorities { get; set; }

        [Display(Name = "Miejsce zamieszkania")]
        public int? PlaceOfResidenceId { get; set; }
        public IEnumerable<SelectListItem> PlacesOfResidence { get; set; }

        [Display(Name = "Wykształcenie")]
        public int? EducationId { get; set; }
        public IEnumerable<SelectListItem> Educations { get; set; }

        [Display(Name = "Stan cywilny")]
        public int? MartialStatusId { get; set; }
        public IEnumerable<SelectListItem> MartialStatuses { get; set; }

        [Display(Name = "Ocena swojego stanu materialnego")]
        public int? MaterialStatusId { get; set; }
        public IEnumerable<SelectListItem> MaterialStatuses { get; set; }

        [Display(Name = "Miejsce pracy")]
        public int? WorkplaceId { get; set; }
        public IEnumerable<SelectListItem> Workplaces { get; set; }
        #endregion

        protected override void Filter()
        {
        }

        protected override void SetEditActions()
        {
            foreach (var row in this.Rows)
            {
                row.EditAction = MVC.Admin.Interviewees.Edit(row.PrimaryKey);
            }
        }

        #region SetDictionaries()
        public void SetDictionaries(IEnumerable<BaseDictionary> dictionary)
        {
            this.Sexes = dictionary.Where(x => x.DictionaryType == DictionaryType.Sex)
                .Select(x => new SelectListItem() { Text = x.Value, Value = x.BaseDictionaryId.ToString() });

            this.Seniorities = dictionary.Where(x => x.DictionaryType == DictionaryType.Seniority)
                .Select(x => new SelectListItem() { Text = x.Value, Value = x.BaseDictionaryId.ToString() });

            this.PlacesOfResidence = dictionary.Where(x => x.DictionaryType == DictionaryType.PlaceOfResidence)
                .Select(x => new SelectListItem() { Text = x.Value, Value = x.BaseDictionaryId.ToString() });

            this.Educations = dictionary.Where(x => x.DictionaryType == DictionaryType.Education)
                .Select(x => new SelectListItem() { Text = x.Value, Value = x.BaseDictionaryId.ToString() });

            this.MartialStatuses = dictionary.Where(x => x.DictionaryType == DictionaryType.MartialStatus)
                .Select(x => new SelectListItem() { Text = x.Value, Value = x.BaseDictionaryId.ToString() });

            this.MaterialStatuses = dictionary.Where(x => x.DictionaryType == DictionaryType.MaterialStatus)
                .Select(x => new SelectListItem() { Text = x.Value, Value = x.BaseDictionaryId.ToString() });

            this.Ages = dictionary.Where(x => x.DictionaryType == DictionaryType.Age)
                .Select(x => new SelectListItem() { Text = x.Value, Value = x.BaseDictionaryId.ToString() });

            this.Workplaces = dictionary.Where(x => x.DictionaryType == DictionaryType.Workplace)
                .Select(x => new SelectListItem() { Text = x.Value, Value = x.BaseDictionaryId.ToString() });
        }
        #endregion
    }
}