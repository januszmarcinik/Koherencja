using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Data;
using JanuszMarcinik.Mvc.Domain.Identity.Entities;
using JanuszMarcinik.Mvc.Domain.Identity.Managers;
using JanuszMarcinik.Mvc.WebUI.Areas.Account.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Account.Controllers
{
    [Authorize(Roles = "Administrator")]
    public partial class UsersController : Controller
    {
        #region UsersController()
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UsersController(ApplicationDbContext context)
        {
            _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
        }

        public UsersController(ApplicationUserManager userManager, ApplicationDbContext context)
        {
            UserManager = userManager;
            _roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
        #endregion

        #region Index()
        public virtual ActionResult Index()
        {
            return View();
        }
        #endregion

        #region List()
        public virtual ActionResult List(UserDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<UserViewModel>>(UserManager.Users);
            datasource.Initialize();

            return View(datasource);
        }

        [HttpPost]
        [ActionName("List")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DataSource(UserDataSource datasource)
        {
            return List(datasource);
        }
        #endregion

        #region Edit
        public virtual ActionResult Edit(int id)
        {
            var user = UserManager.Users.FirstOrDefault(x => x.Id == id);
            var model = Mapper.Map<UserViewModel>(user);

            foreach (var role in _roleManager.Roles)
            {
                model.AllRoles.Add(new SelectListItem()
                {
                    Value = role.Id.ToString(),
                    Text = role.Name,
                    Selected = user.Roles.Any(x => x.RoleId == role.Id)
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Users.FirstOrDefault(x => x.Id == model.Id);
                user.UserName = model.UserName;
                user.Email = model.Email;

                user.Roles.Clear();
                foreach (var selectedRole in model.SelectedRoles)
                {
                    user.Roles.Add(new UserRole()
                    {
                        RoleId = selectedRole,
                        UserId = user.Id
                    });
                }

                UserManager.Update(user);

                return RedirectToAction(MVC.Account.Users.List());
            }

            return View(model);
        }
        #endregion

        #region Delete()
        public virtual ActionResult Delete(int id)
        {
            var user = UserManager.Users.FirstOrDefault(x => x.Id == id);
            var model = Mapper.Map<UserViewModel>(user);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(UserViewModel model)
        {
            var user = UserManager.Users.FirstOrDefault(x => x.Id == model.Id);
            UserManager.DeleteAsync(user);

            return RedirectToAction(MVC.Account.Users.List());
        }
        #endregion
    }
}