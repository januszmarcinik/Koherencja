using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Questions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public partial class QuestionsController : Controller
    {
        #region QuestionsController
        private IQuestionnairesRepository _questionnairesRepository;
        private IQuestionsRepository _questionsRepository;

        public QuestionsController(IQuestionnairesRepository questionnairesRepository, IQuestionsRepository questionsRepository)
        {
            this._questionnairesRepository = questionnairesRepository;
            this._questionsRepository = questionsRepository;
        }
        #endregion

        #region List()
        public virtual ActionResult List(int questionnaireId, QuestionDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<QuestionViewModel>>(_questionsRepository.GetList(questionnaireId));
            datasource.QuestionnaireId = questionnaireId;
            datasource.QuestionnaireName = _questionnairesRepository.GetById(questionnaireId).Name;
            datasource.Initialize();

            return View(datasource);
        }

        [HttpPost]
        [ActionName("List")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DataSource(QuestionDataSource datasource)
        {
            return List(datasource.QuestionnaireId, datasource);
        }
        #endregion

        #region Create()
        public virtual ActionResult Create(int questionnaireId)
        {
            var model = new QuestionViewModel();
            model.QuestionnaireId = questionnaireId;
            model.SetCategories(_questionnairesRepository.GetById(questionnaireId).Categories);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question()
                {
                    OrderNumber = model.OrderNumber,
                    QuestionnaireId = model.QuestionnaireId,
                    Text = model.Text,
                    CategoryId = model.CategoryId
                };

                _questionsRepository.Create(question);

                return RedirectToAction(MVC.Admin.Questions.List(model.QuestionnaireId));
            }

            model.SetCategories(_questionnairesRepository.GetById(model.QuestionnaireId).Categories);

            return View(model);
        }
        #endregion

        #region Edit
        public virtual ActionResult Edit(int id)
        {
            var question = _questionsRepository.GetById(id);
            var model = Mapper.Map<QuestionViewModel>(question);
            model.SetCategories(_questionnairesRepository.GetById(model.QuestionnaireId).Categories);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = _questionsRepository.GetById(model.QuestionId);
                question.OrderNumber = model.OrderNumber;
                question.Text = model.Text;
                question.CategoryId = model.CategoryId;

                _questionsRepository.Update(question);

                return RedirectToAction(MVC.Admin.Questions.List(model.QuestionnaireId));
            }

            model.SetCategories(_questionnairesRepository.GetById(model.QuestionnaireId).Categories);

            return View(model);
        }
        #endregion

        #region Delete()
        public virtual PartialViewResult Delete(int id)
        {
            var model = new DeleteConfirmViewModel()
            {
                Id = id,
                ConfirmationText = "Czy na pewno usunąć pytanie wraz z odpowiedziami?",
            };

            return PartialView("_DeleteConfirm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(DeleteConfirmViewModel model)
        {
            var questionnaireId = _questionsRepository.GetById(model.Id).QuestionnaireId;
            _questionsRepository.Delete(model.Id);

            return RedirectToAction(MVC.Admin.Questions.List(questionnaireId));
        }
        #endregion
    }
}