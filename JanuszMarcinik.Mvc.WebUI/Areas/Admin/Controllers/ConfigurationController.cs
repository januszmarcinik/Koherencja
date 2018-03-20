using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public partial class ConfigurationController : Controller
    {
        private readonly IScoresRepository _scoresRepository;
        private readonly IResultsRepository _resultsRepository;

        public ConfigurationController(IScoresRepository scoresRepository, IResultsRepository resultsRepository)
        {
            _scoresRepository = scoresRepository;
            _resultsRepository = resultsRepository;
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
