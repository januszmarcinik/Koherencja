using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Interviewees;
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

        public IntervieweesController(IIntervieweesRepository intervieweesRepository, IDictionariesRepository dictionariesRepository)
        {
            this._intervieweesRepository = intervieweesRepository;
            this._dictionariesRepository = dictionariesRepository;
        }
        #endregion

        #region List()
        public virtual ActionResult List(IntervieweeDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<IntervieweeViewModel>>(_intervieweesRepository.GetList());
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

        #region Edit
        public virtual ActionResult Edit(int id)
        {
            var questionnaire = _intervieweesRepository.GetById(id);
            var model = Mapper.Map<IntervieweeViewModel>(questionnaire);

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