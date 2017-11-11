using JanuszMarcinik.Mvc.Domain.Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class ResultsViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public List<string> Options { get; set; }
        public List<DictionaryGroupViewModel> DictionaryGroups { get; set; }

        public ActionResult Action { get; set; }

        public static ResultsViewModel Initialize(int questionnaireId, string questionnaireName, List<ResultGeneral> results)
        {
            var questionnaireResults = new ResultsViewModel()
            {
                Id = questionnaireId,
                Text = questionnaireName,
                Options = results.Select(x => x.FullCategoryName).Distinct().ToList(),
                Action = MVC.Application.Results.Details(questionnaireId),
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
                        value.SetValue(resultItem.AverageScoreValue, resultItem.KeyType.GetScoreValueMark(categoryId.HasValue, resultItem.AverageScoreValue));

                        dictionaryItem.Values.Add(value);
                    }

                    dictionaryGroup.DictionaryItems.Add(dictionaryItem);
                }

                questionnaireResults.DictionaryGroups.Add(dictionaryGroup);
            }

           return questionnaireResults;
        }
    }
}