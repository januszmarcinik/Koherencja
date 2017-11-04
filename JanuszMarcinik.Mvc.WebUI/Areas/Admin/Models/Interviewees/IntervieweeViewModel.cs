using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.Application.Models;
using JanuszMarcinik.Mvc.Domain.DataSource;
using JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Interviewees
{
    public class IntervieweeViewModel
    {
        [Grid(DataType = GridDataType.PrimaryKey)]
        public int IntervieweeId { get; set; }

        [Grid(Order = 1)]
        [Display(Name = "Data")]
        public DateTime InterviewDate { get; set; }

        #region Age
        [Required(ErrorMessage = "Wybierz wiek")]
        [Display(Name = "Wiek")]
        public int AgeId { get; set; }

        [Display(Name = "Wiek")]
        [Grid(Order = 2)]
        public string AgeName
        {
            get { return this.Age != null ? this.Age.Value : string.Empty; }
        }

        public BaseDictionary Age { get; set; }
        public IEnumerable<SelectListItem> Ages { get; set; }
        #endregion

        #region Sex
        [Required(ErrorMessage = "Wybierz płeć")]
        [Display(Name = "Płeć")]
        public int SexId { get; set; }

        [Display(Name = "Płeć")]
        [Grid(Order = 3)]
        public string SexName
        {
            get { return this.Sex != null ? this.Sex.Value : string.Empty; }
        }

        public BaseDictionary Sex { get; set; }
        public IEnumerable<SelectListItem> Sexes { get; set; }
        #endregion

        #region Seniority
        [Required(ErrorMessage = "Wybierz staż pracy")]
        [Display(Name = "Staż pracy")]
        public int SeniorityId { get; set; }

        [Display(Name = "Staż pracy")]
        [Grid(Order = 4)]
        public string SeniorityName
        {
            get { return this.Seniority != null ? this.Seniority.Value : string.Empty; }
        }

        public BaseDictionary Seniority { get; set; }
        public IEnumerable<SelectListItem> Seniorities { get; set; }
        #endregion

        #region PlaceOfResidence
        [Required(ErrorMessage = "Wybierz miejsce zamieszkania")]
        [Display(Name = "Miejsce zamieszkania")]
        public int PlaceOfResidenceId { get; set; }

        [Display(Name = "Miejsce zamieszkania")]
        [Grid(Order = 5)]
        public string PlaceOfResidenceName
        {
            get { return this.PlaceOfResidence != null ? this.PlaceOfResidence.Value : string.Empty; }
        }

        public BaseDictionary PlaceOfResidence { get; set; }
        public IEnumerable<SelectListItem> PlacesOfResidence { get; set; }
        #endregion

        #region Education
        [Required(ErrorMessage = "Wybierz wykształcenie")]
        [Display(Name = "Wykształcenie")]
        public int EducationId { get; set; }

        [Display(Name = "Wykształcenie")]
        [Grid(Order = 6)]
        public string EducationName
        {
            get { return this.Education != null ? this.Education.Value : string.Empty; }
        }

        public BaseDictionary Education { get; set; }
        public IEnumerable<SelectListItem> Educations { get; set; }
        #endregion

        #region MartialStatus
        [Required(ErrorMessage = "Wybierz stan cywilny")]
        [Display(Name = "Stan cywilny")]
        public int MartialStatusId { get; set; }

        [Display(Name = "Stan cywilny")]
        [Grid(Order = 7)]
        public string MartialStatusName
        {
            get { return this.MartialStatus != null ? this.MartialStatus.Value : string.Empty; }
        }

        public BaseDictionary MartialStatus { get; set; }
        public IEnumerable<SelectListItem> MartialStatuses { get; set; }
        #endregion

        #region MaterialStatus
        [Required(ErrorMessage = "Wskaż ocenę swojego stanu materialnego")]
        [Display(Name = "Ocena swojego stanu materialnego")]
        public int MaterialStatusId { get; set; }

        [Display(Name = "Ocena swojego stanu materialnego")]
        [Grid(Order = 8)]
        public string MaterialStatusName
        {
            get { return this.MaterialStatus != null ? this.MaterialStatus.Value : string.Empty; }
        }

        public BaseDictionary MaterialStatus { get; set; }
        public IEnumerable<SelectListItem> MaterialStatuses { get; set; }
        #endregion

        public List<IntervieweeQuestionnaireResult> IntervieweeQuestionnaireResults { get; set; }
        public List<IntervieweeDetail> IntervieweeDetails { get; set; }

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
        }
    }
}