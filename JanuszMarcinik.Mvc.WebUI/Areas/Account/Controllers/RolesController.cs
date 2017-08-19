using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Data;
using JanuszMarcinik.Mvc.Domain.Identity.Entities;
using JanuszMarcinik.Mvc.Domain.Identity.Managers;
using JanuszMarcinik.Mvc.WebUI.Areas.Account.Models.Roles;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Account.Controllers
{
    public partial class RolesController : Controller
    {
        #region RolesController()
        private ApplicationRoleManager _roleManager;

        public RolesController(ApplicationDbContext context)
        {
            _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
        }
        #endregion

        #region List()
        public virtual ActionResult List(RoleDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<RoleViewModel>>(_roleManager.Roles);
            datasource.Initialize();

            return View(datasource);
        }

        [HttpPost]
        [ActionName("List")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DataSource(RoleDataSource datasource)
        {
            return List(datasource);
        }
        #endregion

        #region Create()
        public virtual ActionResult Create()
        {
            var model = new RoleViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                _roleManager.Create(new Role() { Name = model.Name });
                return RedirectToAction(MVC.Account.Roles.List());
            }

            return View(model);
        }
        #endregion

        #region Edit
        public virtual ActionResult Edit(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var model = Mapper.Map<RoleViewModel>(role);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Id);
                role.Name = model.Name;
                _roleManager.Update(role);

                return RedirectToAction(MVC.Account.Roles.List());
            }
            return View(model);
        }
        #endregion

        #region Delete()
        public virtual ActionResult Delete(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var model = Mapper.Map<RoleViewModel>(role);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(RoleViewModel model)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Id);
            _roleManager.Delete(role);

            return RedirectToAction(MVC.Account.Roles.List());
        }
        #endregion
    }
}