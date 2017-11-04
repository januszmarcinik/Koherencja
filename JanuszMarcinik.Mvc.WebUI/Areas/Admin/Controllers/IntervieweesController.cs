using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Interviewees;
using JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Ankieter")]
    public partial class IntervieweesController : Controller
    {
        #region IntervieweesController
        private IIntervieweesRepository _intervieweesRepository;
        private IDictionariesRepository _dictionariesRepository;
        private IResultsRepository _resultsRepository;
        private IQuestionnairesRepository _questionnairesRepository;

        public IntervieweesController(IIntervieweesRepository intervieweesRepository, IDictionariesRepository dictionariesRepository, IResultsRepository resultsRepository,
            IQuestionnairesRepository questionnairesRepository)
        {
            this._intervieweesRepository = intervieweesRepository;
            this._dictionariesRepository = dictionariesRepository;
            this._resultsRepository = resultsRepository;
            this._questionnairesRepository = questionnairesRepository;
        }
        #endregion

        #region List()
        public virtual ActionResult List(IntervieweeDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<IntervieweeViewModel>>(_intervieweesRepository.GetList(
                dateFrom: datasource.DateFrom,
                dateTo: datasource.DateTo,
                ageId: datasource.AgeId,
                educationId: datasource.EducationId,
                martialStatusId: datasource.MartialStatusId,
                materialStatusId: datasource.MaterialStatusId,
                placeOfResidenceId: datasource.PlaceOfResidenceId,
                seniorityId: datasource.SeniorityId,
                sexId: datasource.SexId));
            datasource.Initialize();
            datasource.SetDictionaries(_dictionariesRepository.GetList());

            return View(datasource);
        }

        [HttpPost]
        [ActionName("List")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DataSource(IntervieweeDataSource datasource)
        {
            return List(datasource);
        }
        #endregion

        #region Edit()
        public virtual ActionResult Edit(int id)
        {
            var interviewee = _intervieweesRepository.GetById(id);
            var model = Mapper.Map<IntervieweeViewModel>(interviewee);
            model.IntervieweeQuestionnaireResults = _resultsRepository.GetIntervieweeResults(new List<int> { id });

            return View(model);
        }
        #endregion

        #region QuestionnaireResultDetails()
        public virtual ActionResult QuestionnaireResultDetails(int intervieweeId, int questionnaireId)
        {
            var interviewee = _intervieweesRepository.GetById(intervieweeId);
            var model = Mapper.Map<IntervieweeViewModel>(interviewee);
            model.IntervieweeDetails = _resultsRepository.GetIntervieweeDetails(questionnaireId, new List<int> { intervieweeId });

            ViewBag.QuestionnaireName = _questionnairesRepository.GetById(questionnaireId).Name;

            return View(model);
        }
        #endregion

        #region Delete()
        public virtual PartialViewResult Delete(int id)
        {
            var model = new DeleteConfirmViewModel()
            {
                Id = id,
                ConfirmationText = "Czy na pewno usunąć respondenta wraz z jego odpowiedziami?",
            };

            return PartialView("_DeleteConfirm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(DeleteConfirmViewModel model)
        {
            _intervieweesRepository.Delete(model.Id);

            return RedirectToAction(MVC.Admin.Interviewees.List());
        }
        #endregion
    }
}