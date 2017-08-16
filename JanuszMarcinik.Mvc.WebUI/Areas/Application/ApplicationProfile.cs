using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Interviewees;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application
{
    public class ApplicationProfile : Profile
    {
        #region ProfileName
        public override string ProfileName
        {
            get { return this.GetType().FullName; }
        }
        #endregion

        #region ApplicationProfile()
        public ApplicationProfile()
        {
            CreateMap<Interviewee, IntervieweeViewModel>()
                .Ignore(x => x.Educations)
                .Ignore(x => x.MartialStatuses)
                .Ignore(x => x.MaterialStatuses)
                .Ignore(x => x.PlacesOfResidence)
                .Ignore(x => x.Seniorities)
                .Ignore(x => x.Sexes);
        }
        #endregion
    }
}