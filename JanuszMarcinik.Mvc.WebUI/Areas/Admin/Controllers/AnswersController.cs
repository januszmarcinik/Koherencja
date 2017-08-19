using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Answers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public partial class AnswersController : Controller
    {
        #region AnswersController
        private IQuestionsRepository _questionsRepository;
        private IAnswersRepository _answersRepository;

        public AnswersController(IQuestionsRepository questionsRepository, IAnswersRepository answersRepository)
        {
            this._answersRepository = answersRepository;
            this._questionsRepository = questionsRepository;
        }
        #endregion

        #region List()
        public virtual ActionResult List(int questionId, AnswerDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<AnswerViewModel>>(_answersRepository.GetList(questionId));
            datasource.Initialize();

            var question = _questionsRepository.GetById(questionId);
            datasource.QuestionId = questionId;
            datasource.QuestionText = question.Text;
            datasource.QuestionnaireId = question.QuestionnaireId;


            return View(datasource);
        }

        [HttpPost]
        [ActionName("List")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DataSource(AnswerDataSource datasource)
        {
            return List(datasource.QuestionId, datasource);
        }
        #endregion

        #region Create()
        public virtual ActionResult Create(int questionId)
        {
            var model = new AnswerViewModel();
            model.QuestionId = questionId;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(AnswerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var answer = new Answer()
                {
                    QuestionId = model.QuestionId,
                    OrderNumber = model.OrderNumber,
                    Description = model.Description,
                    Value = model.Value
                };

                _answersRepository.Create(answer);

                return RedirectToAction(MVC.Admin.Answers.List(model.QuestionId));
            }

            return View(model);
        }
        #endregion

        #region Edit
        public virtual ActionResult Edit(int id)
        {
            var answer = _answersRepository.GetById(id);
            var model = Mapper.Map<AnswerViewModel>(answer);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(AnswerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var answer = _answersRepository.GetById(model.AnswerId);
                answer.OrderNumber = model.OrderNumber;
                answer.Description = model.Description;
                answer.Value = model.Value;

                _answersRepository.Update(answer);

                return RedirectToAction(MVC.Admin.Answers.List(model.QuestionId));
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
                ConfirmationText = "Czy na pewno usunąć odpowiedź?",
            };

            return PartialView("_DeleteConfirm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(DeleteConfirmViewModel model)
        {
            var questionId = _answersRepository.GetById(model.Id).QuestionId;
            _answersRepository.Delete(model.Id);

            return RedirectToAction(MVC.Admin.Answers.List(questionId));
        }
        #endregion
    }
}