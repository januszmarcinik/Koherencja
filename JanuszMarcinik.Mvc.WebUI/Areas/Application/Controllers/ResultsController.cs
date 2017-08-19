using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Interviewees;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Controllers
{
    [Authorize(Roles = "Ankieter")]
    public partial class ResultsController : Controller
    {
        #region ResultsController
        private IIntervieweesRepository _intervieweesRepository;

        public ResultsController(IIntervieweesRepository intervieweesRepository)
        {
            this._intervieweesRepository = intervieweesRepository;
        }
        #endregion

        #region Interviewees()
        public virtual ActionResult Interviewees(IntervieweeDataSource datasource = null)
        {
            datasource.Data = Mapper.Map<List<IntervieweeViewModel>>(_intervieweesRepository.GetList());
            datasource.Initialize();

            return View(datasource);
        }

        [HttpPost]
        [ActionName("Interviewees")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DataSource(IntervieweeDataSource datasource)
        {
            return Interviewees(datasource);
        }
        #endregion
    }
}