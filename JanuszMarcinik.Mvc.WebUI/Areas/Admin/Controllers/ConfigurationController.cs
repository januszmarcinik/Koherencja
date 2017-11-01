using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public partial class ConfigurationController : Controller
    {
        private readonly IScoresRepository _scoresRepository;

        public ConfigurationController(IScoresRepository scoresRepository)
        {
            _scoresRepository = scoresRepository;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult RecalculateScores()
        {
            _scoresRepository.RecalculateScores();
            return RedirectToAction(MVC.Admin.Configuration.Index());
        }
    }
}