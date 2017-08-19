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

        [Display(Name = "Wiek od")]
        public int? AgeFrom { get; set; }
        [Display(Name = "Wiek do")]
        public int? AgeTo { get; set; }

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
        #endregion

        protected override void Filter()
        {
            if (this.DateFrom.HasValue)
            {
                this.Data = this.Data.Where(x => x.InterviewDate >= this.DateFrom);
            }

            if (this.DateTo.HasValue)
            {
                var dateTo = this.DateTo.Value.AddDays(1);
                this.Data = this.Data.Where(x => x.InterviewDate <= dateTo);
            }

            if (this.AgeFrom.HasValue)
            {
                this.Data = this.Data.Where(x => x.Age >= this.AgeFrom);
            }

            if (this.AgeTo.HasValue)
            {
                this.Data = this.Data.Where(x => x.Age <= this.AgeTo);
            }

            if (this.SexId.HasValue)
            {
                this.Data = this.Data.Where(x => x.SexId == this.SexId);
            }

            if (this.EducationId.HasValue)
            {
                this.Data = this.Data.Where(x => x.EducationId == this.EducationId);
            }

            if (this.MartialStatusId.HasValue)
            {
                this.Data = this.Data.Where(x => x.MartialStatusId == this.MartialStatusId);
            }

            if (this.MaterialStatusId.HasValue)
            {
                this.Data = this.Data.Where(x => x.MaterialStatusId == this.MaterialStatusId);
            }

            if (this.PlaceOfResidenceId.HasValue)
            {
                this.Data = this.Data.Where(x => x.PlaceOfResidenceId == this.PlaceOfResidenceId);
            }

            if (this.SeniorityId.HasValue)
            {
                this.Data = this.Data.Where(x => x.SeniorityId == this.SeniorityId);
            }
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
        }
        #endregion
    }
}