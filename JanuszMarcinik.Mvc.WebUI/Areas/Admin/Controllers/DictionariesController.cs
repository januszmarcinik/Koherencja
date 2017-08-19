using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Dictionaries;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public partial class DictionariesController : Controller
    {
        #region DictionariesController
        private IDictionariesRepository _dictionariesRepository;

        public DictionariesController(IDictionariesRepository dictionariesRepository)
        {
            this._dictionariesRepository = dictionariesRepository;
        }
        #endregion

        #region List()
        public virtual ActionResult List(DictionaryDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<DictionaryViewModel>>(_dictionariesRepository.GetList());
            datasource.Initialize();

            return View(datasource);
        }

        [HttpPost]
        [ActionName("List")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DataSource(DictionaryDataSource datasource)
        {
            return List(datasource);
        }
        #endregion

        #region Create()
        public virtual ActionResult Create()
        {
            var model = new DictionaryViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(DictionaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dictionary = new BaseDictionary()
                {
                    DictionaryType = model.DictionaryType,
                    Value = model.Value
                };

                _dictionariesRepository.Create(dictionary);

                return RedirectToAction(MVC.Admin.Dictionaries.List());
            }

            return View(model);
        }
        #endregion

        #region Edit
        public virtual ActionResult Edit(int id)
        {
            var dictionary = _dictionariesRepository.GetById(id);
            var model = Mapper.Map<DictionaryViewModel>(dictionary);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(DictionaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dictionary = _dictionariesRepository.GetById(model.BaseDictionaryId);
                dictionary.DictionaryType = model.DictionaryType;
                dictionary.Value = model.Value;

                _dictionariesRepository.Update(dictionary);

                return RedirectToAction(MVC.Admin.Dictionaries.List());
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
                ConfirmationText = "Czy na pewno usunąć pozycję z metryczki?",
            };

            return PartialView("_DeleteConfirm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(DeleteConfirmViewModel model)
        {
            _dictionariesRepository.Delete(model.Id);

            return RedirectToAction(MVC.Admin.Dictionaries.List());
        }
        #endregion
    }
}