using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Keys;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Interviewees;
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
                    MaterialStatusId = model.MaterialStatusId,
                    AgeId = model.AgeId
                };

                Session[_intervieweeSessionKey] = interviewee;
                Session[_resultsSessionKey] = new List<Result>();

                return RedirectToAction(MVC.Application.Survey.LOT());
            }

            model.SetDictionaries(_dictionariesRepository.GetList());

            return View(model);
        }
        #endregion

        #region LOT()
        public virtual ActionResult LOT()
        {
            var lot = _questionnairesRepository.GetByType(KeyType.LOTR);
            var model = new LOTViewModel(lot);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LOT(LOTViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionIdValue = model.Questions
                    .ToDictionary(key => key.Id, val => val.Value.Value);

                var results = (List<Result>)Session[_resultsSessionKey];
                if (results == null)
                {
                    return RedirectToAction(MVC.Application.Survey.IntervieweeInfo());
                }
                results.AddRange(_resultsRepository.GetResultsByDict(model.QuestionnaireId, questionIdValue));
                Session[_resultsSessionKey] = results;

                return RedirectToAction(MVC.Application.Survey.IZZ());
            }
            else
            {
                model.ErrorProperties = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => x.Key)
                    .ToArray();

                return View(model);
            }
        }
        #endregion

        #region IZZ()
        public virtual ActionResult IZZ()
        {
            var lot = _questionnairesRepository.GetByType(KeyType.IZZ);
            var model = new IZZViewModel(lot);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult IZZ(IZZViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionIdValue = model.Questions
                    .ToDictionary(key => key.Id, val => val.Value.Value);

                var results = (List<Result>)Session[_resultsSessionKey];
                if (results == null)
                {
                    return RedirectToAction(MVC.Application.Survey.IntervieweeInfo());
                }
                results.AddRange(_resultsRepository.GetResultsByDict(model.QuestionnaireId, questionIdValue));
                Session[_resultsSessionKey] = results;

                return RedirectToAction(MVC.Application.Survey.WHOQOL());
            }
            else
            {
                model.ErrorProperties = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => x.Key)
                    .ToArray();

                return View(model);
            }
        }
        #endregion

        #region WHOQOL()
        public virtual ActionResult WHOQOL()
        {
            var model = new WHOQOLViewModel();
            model.SetQuestionnaire(_questionnairesRepository.GetByType(KeyType.WHOQOL));
            model.SelectedValues = new List<int>();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult WHOQOL(WHOQOLViewModel model)
        {
            if (model.Questions.All(x => x.AnswerId > 0))
            {
                var results = (List<Result>)Session[_resultsSessionKey];
                if (results == null)
                {
                    return RedirectToAction(MVC.Application.Survey.IntervieweeInfo());
                }
                foreach (var question in model.Questions)
                {
                    results.Add(new Result()
                    {
                        QuestionnaireId = model.QuestionnaireId,
                        QuestionId = question.QuestionId,
                        AnswerId = question.AnswerId
                    });
                }

                var interviewee = (Interviewee)Session[_intervieweeSessionKey];
                _intervieweesRepository.Create(interviewee);

                _resultsRepository.CreateMany(results, interviewee.IntervieweeId);

                return RedirectToAction(MVC.Application.Survey.ThankYou());
            }
            else
            {
                if (model.Questions.Any(x => x.AnswerId == 0))
                {
                    ViewBag.WHOQOLModelError = true;
                }

                var selectedAnswers = model.Questions.Where(x => x.AnswerId > 0).Select(x => x.AnswerId).ToList();
                model.SetQuestionnaire(_questionnairesRepository.GetByType(KeyType.WHOQOL));
                model.SelectedValues = selectedAnswers;

                return View(model);
            }
        }
        #endregion

        #region ThankYou()
        public virtual ActionResult ThankYou()
        {
            return View();
        }
        #endregion
    }
}