using AutoMapper;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Questionnaires;
using JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Controllers
{
    [Authorize(Roles = "Ankieter")]
    public partial class ResultsController : Controller
    {
        #region ResultsController
        private readonly IQuestionnairesRepository _questionnairesRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly IResultsRepository _resultsRepository;
        private readonly IDictionariesRepository _dictionariesRepository;
        private readonly IIntervieweesRepository _intervieweesRepository;

        public ResultsController(IResultsRepository resultsRepository, IDictionariesRepository dictionariesRepository, IQuestionnairesRepository questionnairesRepository,
            IAnswersRepository answersRepository, IIntervieweesRepository intervieweesRepository, IQuestionsRepository questionsRepository)
        {
            this._resultsRepository = resultsRepository;
            this._dictionariesRepository = dictionariesRepository;
            this._questionnairesRepository = questionnairesRepository;
            this._answersRepository = answersRepository;
            this._intervieweesRepository = intervieweesRepository;
            this._questionsRepository = questionsRepository;
        }
        #endregion

        #region QuestionnaireSelect()
        public virtual ActionResult QuestionnaireSelect()
        {
            var model = Mapper.Map<IEnumerable<QuestionnaireViewModel>>(_questionnairesRepository.GetList());
            return View(model);
        }
        #endregion

        #region Details()
        public virtual ActionResult Details(int questionnaireId)
        {
            var results = _resultsRepository.GetResultDetails(questionnaireId);

            var model = new QuestionnaireResultsViewModel()
            {
                QuestionnaireId = questionnaireId,
                QuestionnaireName = _questionnairesRepository.GetById(questionnaireId).Name,
                QuestionResults = new List<QuestionResultsViewModel>()
            };

            var questionsIds = results.Select(x => x.QuestionId).Distinct();
            foreach (var questionId in questionsIds)
            {
                var question = _questionsRepository.GetById(questionId);

                var questionResults = new QuestionResultsViewModel()
                {
                    QuestionId = questionId,
                    QuestionText = question.Text,
                    QuestionNumber = question.OrderNumber,
                    Answers = _answersRepository.GetDescriptions(questionId),
                    DictionaryGroups = new List<DictionaryGroupViewModel>()
                };

                var dictionaryGroupNames = results.Select(x => x.DictionaryTypeName).Distinct();
                foreach (var dictionaryType in dictionaryGroupNames)
                {
                    var dictionaryGroup = new DictionaryGroupViewModel()
                    {
                        GroupName = dictionaryType,
                        DictionaryItems = new List<DictionaryItemViewModel>()
                    };

                    var dictionaryItemsIds = results.Where(x => x.DictionaryTypeName == dictionaryType).Select(x => x.BaseDictionaryId).Distinct();
                    foreach (var itemId in dictionaryItemsIds)
                    {
                        var dictionaryItem = new DictionaryItemViewModel()
                        {
                            ItemName = _dictionariesRepository.GetById(itemId).Value,
                            AnswersResults = new List<AnswerResultViewModel>()
                        };

                        var answersIdList = results.Where(x => x.QuestionId == questionId).Select(x => x.AnswerId).Distinct();
                        foreach (var answerId in answersIdList)
                        {
                            var resultItem = results
                                .Where(x => x.QuestionId == questionId)
                                .Where(x => x.BaseDictionaryId == itemId)
                                .Where(x => x.AnswerId == answerId)
                                .FirstOrDefault();

                            dictionaryItem.AnswersResults.Add(new AnswerResultViewModel()
                            {
                                AnswersCount = resultItem.AnswersCount,
                                TotalAnswersCount = resultItem.TotalAnswersCount
                            });
                        }

                        dictionaryGroup.DictionaryItems.Add(dictionaryItem);
                    }

                    questionResults.DictionaryGroups.Add(dictionaryGroup);
                }

                model.QuestionResults.Add(questionResults);
            }

            return View(model);
        }
        #endregion
    }
}