using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
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
        private readonly IScoresRepository _scoresRepository;

        public ResultsController(IResultsRepository resultsRepository, IDictionariesRepository dictionariesRepository, IQuestionnairesRepository questionnairesRepository,
            IAnswersRepository answersRepository, IIntervieweesRepository intervieweesRepository, IQuestionsRepository questionsRepository, IScoresRepository scoresRepository)
        {
            this._resultsRepository = resultsRepository;
            this._dictionariesRepository = dictionariesRepository;
            this._questionnairesRepository = questionnairesRepository;
            this._answersRepository = answersRepository;
            this._intervieweesRepository = intervieweesRepository;
            this._questionsRepository = questionsRepository;
            this._scoresRepository = scoresRepository;
        }
        #endregion

        #region General()
        public virtual ActionResult General()
        {
            var model = new ResultsListViewModel()
            {
                Title = "Wyniki wg ankiet",
                Results = new List<ResultsViewModel>(),
                Legend = LegendViewModel.General()
            };

            foreach (var questionnaire in _questionnairesRepository.GetList())
            {
                var results = _resultsRepository.GetResultsGeneral(questionnaire.QuestionnaireId);

                var questionnaireResults = new ResultsViewModel()
                {
                    Id = questionnaire.QuestionnaireId,
                    Text = questionnaire.Name,
                    Options = results.Select(x => x.FullCategoryName).Distinct().ToList(),
                    Action = MVC.Application.Results.Details(questionnaire.QuestionnaireId),
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
                        var resultDictItem = results.First(x => x.BaseDictionaryId == itemId);
                        var dictionaryItem = new DictionaryItemViewModel()
                        {
                            ItemName = resultDictItem.BaseDictionaryValue,
                            Badge = resultDictItem.IntervieweeCount.ToString(),
                            Values = new List<ValueViewModel>()
                        };

                        var categoryIds = results.Select(x => x.CategoryId).Distinct();
                        foreach (var categoryId in categoryIds)
                        {
                            var resultItem = results
                                .Where(x => x.BaseDictionaryId == itemId)
                                .Where(x => x.CategoryId == categoryId)
                                .FirstOrDefault();

                            var value = new ValueViewModel();
                            value.Badge = $"{resultItem.AveragePointsEarned} / {resultItem.PointsAvailableToGet}";
                            value.SetValue(resultItem.AverageScoreValue, _scoresRepository.GetScoreValueMark(resultItem.KeyType, true, resultItem.AverageScoreValue));

                            dictionaryItem.Values.Add(value);
                        }

                        dictionaryGroup.DictionaryItems.Add(dictionaryItem);
                    }

                    questionnaireResults.DictionaryGroups.Add(dictionaryGroup);
                }

                model.Results.Add(questionnaireResults);
            }

            return View(MVC.Application.Results.Views.Results, model);
        }
        #endregion

        #region Details()
        public virtual ActionResult Details(int questionnaireId)
        {
            var results = _resultsRepository.GetResultDetails(questionnaireId);

            var model = new ResultsListViewModel()
            {
                Title = _questionnairesRepository.GetById(questionnaireId).Name,
                Results = new List<ResultsViewModel>(),
                Legend = LegendViewModel.Details()
            };

            var questionsIds = results.Select(x => x.QuestionId).Distinct();
            foreach (var questionId in questionsIds)
            {
                var question = _questionsRepository.GetById(questionId);

                var questionResults = new ResultsViewModel()
                {
                    Id = questionId,
                    Text = $"{question.OrderNumber}. {question.Text}",
                    Options = _answersRepository.GetDescriptions(questionId),
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
                            Badge = results.First(x => x.BaseDictionaryId == itemId).IntervieweeCount.ToString(),
                            Values = new List<ValueViewModel>()
                        };

                        var answersIdList = results.Where(x => x.QuestionId == questionId).Select(x => x.AnswerId).Distinct();
                        foreach (var answerId in answersIdList)
                        {
                            var resultItem = results
                                .Where(x => x.QuestionId == questionId)
                                .Where(x => x.BaseDictionaryId == itemId)
                                .Where(x => x.AnswerId == answerId)
                                .FirstOrDefault();

                            var value = new ValueViewModel()
                            {
                                Badge = resultItem.AnswersCount.ToString(),
                                Count = resultItem.AnswersCount,
                                TotalCount = resultItem.TotalAnswersCount
                            };
                            value.SetValueByPercentage();

                            dictionaryItem.Values.Add(value);
                        }

                        dictionaryGroup.DictionaryItems.Add(dictionaryItem);
                    }

                    questionResults.DictionaryGroups.Add(dictionaryGroup);
                }

                model.Results.Add(questionResults);
            }

            return View(MVC.Application.Results.Views.Results, model);
        }
        #endregion

        #region IntervieweeResults()
        public virtual ActionResult IntervieweeResults(int? ageId, int? sexId, int? educationId, int? martialStatusId, 
            int? materialStatusId, int? placeOfResidenceId, int? seniorityId)
        {
            var intervieweesIds = _intervieweesRepository.GetList(
                ageId: ageId,
                educationId: educationId,
                martialStatusId: martialStatusId,
                materialStatusId: materialStatusId,
                placeOfResidenceId: placeOfResidenceId,
                seniorityId: seniorityId,
                sexId: sexId)
                .Select(x => x.IntervieweeId)
                .ToList();

            var model = GetIntervieweeResultBasicViewModel(
                ageId: ageId,
                educationId: educationId,
                martialStatusId: martialStatusId,
                materialStatusId: materialStatusId,
                placeOfResidenceId: placeOfResidenceId,
                seniorityId: seniorityId,
                sexId: sexId);
            model.IntervieweeQuestionnaireResults = _resultsRepository.GetIntervieweeResults(intervieweesIds);

            return View(model);
        }
        #endregion

        #region IntervieweeDetails()
        public virtual ActionResult IntervieweeDetails(int questionnaireId, int? ageId, int? sexId, int? educationId, int? martialStatusId,
            int? materialStatusId, int? placeOfResidenceId, int? seniorityId)
        {
            var intervieweesIds = _intervieweesRepository.GetList(
                ageId: ageId,
                educationId: educationId,
                martialStatusId: martialStatusId,
                materialStatusId: materialStatusId,
                placeOfResidenceId: placeOfResidenceId,
                seniorityId: seniorityId,
                sexId: sexId)
                .Select(x => x.IntervieweeId)
                .ToList();

            var model = GetIntervieweeResultBasicViewModel(
                ageId: ageId,
                educationId: educationId,
                martialStatusId: martialStatusId,
                materialStatusId: materialStatusId,
                placeOfResidenceId: placeOfResidenceId,
                seniorityId: seniorityId,
                sexId: sexId);
            model.IntervieweeDetails = _resultsRepository.GetIntervieweeDetails(questionnaireId, intervieweesIds);
            model.Legend = LegendViewModel.Details();

            ViewBag.QuestionnaireName = _questionnairesRepository.GetById(questionnaireId).Name;

            return View(model);
        }
        #endregion


        #region Helpers
        private IntervieweeResultsViewModel GetIntervieweeResultBasicViewModel(int? ageId, int? sexId, int? educationId, int? martialStatusId,
            int? materialStatusId, int? placeOfResidenceId, int? seniorityId)
        {
            return new IntervieweeResultsViewModel()
            {
                AgeId = ageId,
                Age = _dictionariesRepository.GetValueOrEmptyIfNull(ageId),

                SexId = sexId,
                Sex = _dictionariesRepository.GetValueOrEmptyIfNull(sexId),

                EducationId = educationId,
                Education = _dictionariesRepository.GetValueOrEmptyIfNull(educationId),

                MartialStatusId = martialStatusId,
                MartialStatus = _dictionariesRepository.GetValueOrEmptyIfNull(martialStatusId),

                MaterialStatusId = materialStatusId,
                MaterialStatus = _dictionariesRepository.GetValueOrEmptyIfNull(materialStatusId),

                PlaceOfResidenceId = placeOfResidenceId,
                PlaceOfResidence = _dictionariesRepository.GetValueOrEmptyIfNull(placeOfResidenceId),

                SeniorityId = seniorityId,
                Seniority = _dictionariesRepository.GetValueOrEmptyIfNull(seniorityId)
            };
        }
        #endregion
    }
}