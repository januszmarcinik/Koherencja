using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Answers;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Categories;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Dictionaries;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Interviewees;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Questionnaires;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Questions;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin
{
    public class AdminProfile : Profile
    {
        #region ProfileName
        public override string ProfileName
        {
            get { return this.GetType().FullName; }
        }
        #endregion

        #region AdminProfile()
        public AdminProfile()
        {
            CreateMap<Questionnaire, QuestionnaireViewModel>();

            CreateMap<Question, QuestionViewModel>()
                .Ignore(x => x.Categories);

            CreateMap<Answer, AnswerViewModel>();

            CreateMap<BaseDictionary, DictionaryViewModel>();

            CreateMap<Interviewee, IntervieweeViewModel>()
                .Ignore(x => x.Educations)
                .Ignore(x => x.MartialStatuses)
                .Ignore(x => x.MaterialStatuses)
                .Ignore(x => x.PlacesOfResidence)
                .Ignore(x => x.Seniorities)
                .Ignore(x => x.Sexes);

            CreateMap<Category, CategoryViewModel>()
                .Ignore(x => x.DeleteDisabled)
                .Ignore(x => x.Questionnaires);
        }
        #endregion
    }
}