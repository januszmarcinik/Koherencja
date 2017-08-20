using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Categories;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public partial class CategoriesController : Controller
    {
        #region CategoriesController
        private IQuestionnairesRepository _questionnairesRepository;
        private ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository, IQuestionnairesRepository questionnairesRepository)
        {
            this._categoriesRepository = categoriesRepository;
            this._questionnairesRepository = questionnairesRepository;
        }
        #endregion

        #region List()
        public virtual ActionResult List(CategoryDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<CategoryViewModel>>(_categoriesRepository.GetList());
            datasource.Initialize();

            return View(datasource);
        }

        [HttpPost]
        [ActionName("List")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DataSource(CategoryDataSource datasource)
        {
            return List(datasource);
        }
        #endregion

        #region Create()
        public virtual ActionResult Create()
        {
            var model = new CategoryViewModel();
            model.SetQuestionnaires(_questionnairesRepository.GetList());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _categoriesRepository.Create(new Category()
                {
                    Name = model.Name,
                    QuestionnaireId = model.QuestionnaireId
                });

                return RedirectToAction(MVC.Admin.Categories.List());
            }

            model.SetQuestionnaires(_questionnairesRepository.GetList());

            return View(model);
        }
        #endregion

        #region Edit
        public virtual ActionResult Edit(int id)
        {
            var category = _categoriesRepository.GetById(id);
            var model = Mapper.Map<CategoryViewModel>(category);
            model.DeleteDisabled = category.Questions.Count > 0;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _categoriesRepository.GetById(model.CategoryId);
                category.Name = model.Name;

                _categoriesRepository.Update(category);

                return RedirectToAction(MVC.Admin.Categories.List());
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
                ConfirmationText = "Czy na pewno usunąć kategorię?",
            };

            return PartialView("_DeleteConfirm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(DeleteConfirmViewModel model)
        {
            _categoriesRepository.Delete(model.Id);

            return RedirectToAction(MVC.Admin.Categories.List());
        }
        #endregion
    }
}