using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Interviewees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public virtual ActionResult Interviewees()
        {
            var interviewees = _intervieweesRepository.GetList();
            var model = new IntervieweeDataSource();
            model.Interviewees = Mapper.Map<List<IntervieweeViewModel>>(interviewees);
            model.SetActions();

            return View(MVC.Shared.Views._Grid, model.GetGridModel());
        }
        #endregion
    }
}