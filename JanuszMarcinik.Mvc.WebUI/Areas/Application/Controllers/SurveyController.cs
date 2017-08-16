using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Interviewees;
using JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Controllers
{
    public partial class SurveyController : Controller
    {
        private const string _intervieweeSessionKey = "IntervieweeSessionKey";
        private const string _resultsSessionKey = "ResultsSessionKey";

        #region SurveyController
        private IQuestionnairesRepository _questionnairesRepository;
        private IIntervieweesRepository _intervieweesRepository;
        private IDictionariesRepository _dictionariesRepository;
        private IResultsRepository _resultsRepository;

        public SurveyController(IQuestionnairesRepository questionnairesRepository, IDictionariesRepository dictionariesRepository,
            IIntervieweesRepository intervieweesRepository, IResultsRepository resultsRepository)
        {
            this._dictionariesRepository = dictionariesRepository;
            this._questionnairesRepository = questionnairesRepository;
            this._intervieweesRepository = intervieweesRepository;
            this._resultsRepository = resultsRepository;
        }
        #endregion

        #region IntervieweeInfo()
        public virtual ActionResult IntervieweeInfo()
        {
            var model = new IntervieweeViewModel();
            model.Age = 22;
            model.SetDictionaries(_dictionariesRepository.GetList());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult IntervieweeInfo(IntervieweeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var interviewee = new Interviewee()
                {
                    InterviewDate = DateTime.Now,
                    PlaceOfResidenceId = model.PlaceOfResidenceId,
                    SeniorityId = model.SeniorityId,
                    SexId = model.SexId,
                    EducationId = model.EducationId,
                    Age = model.Age,
                    MartialStatusId = model.MartialStatusId,
                    MaterialStatusId = model.MaterialStatusId
                };

                Session[_intervieweeSessionKey] = interviewee;
                Session[_resultsSessionKey] = new List<Result>();

                return RedirectToAction(MVC.Application.Survey.FillSurvey(1));
            }

            model.SetDictionaries(_dictionariesRepository.GetList());

            return View(model);
        }
        #endregion

        #region FillSurvey()
        public virtual ActionResult FillSurvey(int questionaireNumber)
        {
            var model = new QuestionnaireViewModel();
            model.SetQuestionnaire(_questionnairesRepository.GetFullModel(questionaireNumber));
            model.SelectedValues = new List<int>();
            model.QuestionnairesCount = _questionnairesRepository.GetOnlyActives().Count();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult FillSurvey(QuestionnaireViewModel model)
        {
            if (model.Questions.All(x => x.AnswerId > 0))
            {
                var results = (List<Result>)Session[_resultsSessionKey];

                foreach (var question in model.Questions)
                {
                    results.Add(new Result()
                    {
                        QuestionnaireId = model.QuestionnaireId,
                        QuestionId = question.QuestionId,
                        AnswerId = question.AnswerId
                    });
                }

                Session[_resultsSessionKey] = results;

                if (model.OrderNumber == model.QuestionnairesCount)
                {
                    var interviewee = (Interviewee)Session[_intervieweeSessionKey];
                    _intervieweesRepository.Create(interviewee);

                    _resultsRepository.CreateMany(results, interviewee.IntervieweeId);

                    return View(MVC.Application.Survey.Views.ThankYou);
                }
                else
                {
                    return RedirectToAction(MVC.Application.Survey.FillSurvey(model.OrderNumber + 1));
                }
            }
            else
            {
                if (model.Questions.Any(x => x.AnswerId == 0))
                {
                    ModelState.AddModelError("", "Należy odpowiedzieć na wszystkie pytnia");
                }

                var selectedAnswers = model.Questions.Where(x => x.AnswerId > 0).Select(x => x.AnswerId).ToList();
                model.SetQuestionnaire(_questionnairesRepository.GetFullModel(model.OrderNumber));
                model.SelectedValues = selectedAnswers;

                return View(model);
            }
        }
        #endregion
    }
}