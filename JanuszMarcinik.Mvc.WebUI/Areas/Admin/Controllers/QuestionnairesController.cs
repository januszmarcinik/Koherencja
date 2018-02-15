using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Questionnaires;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public partial class QuestionnairesController : Controller
    {
        #region QuestionnairesController
        private IQuestionnairesRepository _questionnairesRepository;

        public QuestionnairesController(IQuestionnairesRepository questionnairesRepository)
        {
            this._questionnairesRepository = questionnairesRepository;
        }
        #endregion

        #region List()
        public virtual ActionResult List(QuestionnaireDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<QuestionnaireViewModel>>(_questionnairesRepository.GetList(activeOnly: false));
            datasource.Initialize();

            return View(datasource);
        }

        [HttpPost]
        [ActionName("List")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DataSource(QuestionnaireDataSource datasource)
        {
            return List(datasource);
        }
        #endregion

        #region Create()
        public virtual ActionResult Create()
        {
            var model = new QuestionnaireViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionnaire = new Questionnaire()
                {
                    OrderNumber = model.OrderNumber,
                    Name = model.Name,
                    IsActive = model.IsActive,
                    KeyType = model.KeyType
                };

                _questionnairesRepository.Create(questionnaire);

                return RedirectToAction(MVC.Admin.Questionnaires.List());
            }

            return View(model);
        }
        #endregion

        #region Edit
        public virtual ActionResult Edit(int id)
        {
            var questionnaire = _questionnairesRepository.GetById(id);
            var model = Mapper.Map<QuestionnaireViewModel>(questionnaire);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(QuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionnaire = _questionnairesRepository.GetById(model.QuestionnaireId);
                questionnaire.OrderNumber = model.OrderNumber;
                questionnaire.Name = model.Name;
                questionnaire.IsActive = model.IsActive;
                questionnaire.KeyType = model.KeyType;

                _questionnairesRepository.Update(questionnaire);

                return RedirectToAction(MVC.Admin.Questionnaires.List());
            }
            return View(model);
        }
        #endregion

        #region Delete()
        public virtual PartialViewResult Delete(int id)
        {
            var model = new DeleteConfirmViewModel()
            {
                Id = id,
                ConfirmationText = "Czy na pewno usunąć ankietę wraz z pytaniami i odpowiedziami?",
            };

            return PartialView("_DeleteConfirm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(DeleteConfirmViewModel model)
        {
            _questionnairesRepository.Delete(model.Id);

            return RedirectToAction(MVC.Admin.Questionnaires.List());
        }
        #endregion
    }
}