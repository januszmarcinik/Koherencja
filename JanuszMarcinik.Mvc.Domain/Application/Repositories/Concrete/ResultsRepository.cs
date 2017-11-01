using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using System.Collections.Generic;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Linq;
using JanuszMarcinik.Mvc.Domain.Application.Models;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Concrete
{
    public class ResultsRepository : IResultsRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void CreateMany(List<Result> entities, int intervieweeId)
        {
            foreach (var item in entities)
            {
                item.IntervieweeId = intervieweeId;
            }

            context.Results.AddRange(entities);
            context.SaveChanges();
        }

        #region GetResultDetails()
        public IEnumerable<ResultDetail> GetResultDetails(int questionnaireId)
        {
            var questionnaire = context.Questionnaires.Find(questionnaireId);

            var model = new List<ResultDetail>();

            foreach (var question in questionnaire.Questions.OrderBy(x => x.OrderNumber))
            {
                foreach (var dictionary in context.BaseDictionaries)
                {
                    var intervieweesIds = context.Interviewees
                        .Where(x =>
                            x.SexId == dictionary.BaseDictionaryId ||
                            x.SeniorityId == dictionary.BaseDictionaryId ||
                            x.EducationId == dictionary.BaseDictionaryId ||
                            x.PlaceOfResidenceId == dictionary.BaseDictionaryId ||
                            x.MartialStatusId == dictionary.BaseDictionaryId ||
                            x.AgeId == dictionary.BaseDictionaryId ||
                            x.MaterialStatusId == dictionary.BaseDictionaryId)
                        .Select(x => x.IntervieweeId).ToList();

                    var results = question.Results.Where(x => intervieweesIds.Contains(x.IntervieweeId));
                    var totalAnswersCount = results.Count();

                    foreach (var answer in question.Answers.OrderBy(x => x.OrderNumber))
                    {
                        model.Add(new ResultDetail()
                        {
                            QuestionId = question.QuestionId,
                            QuestionText = question.Text,
                            BaseDictionaryId = dictionary.BaseDictionaryId,
                            BaseDictionaryValue = dictionary.Value,
                            DictionaryTypeName = dictionary.DictionaryType.GetDescription(),
                            AnswerId = answer.AnswerId,
                            AnswersCount = results.Where(x => x.AnswerId == answer.AnswerId).Count(),
                            TotalAnswersCount = totalAnswersCount,
                            IntervieweeCount = intervieweesIds.Count
                        });
                    }
                }
            }

            return model;
        }
        #endregion

        #region GetResultsGeneral()
        public IEnumerable<ResultGeneral> GetResultsGeneral(int questionnaireId)
        {
            var questionnaire = context.Questionnaires.Find(questionnaireId);

            var model = new List<ResultGeneral>();

            foreach (var dictionary in context.BaseDictionaries)
            {
                var intervieweesIds = context.Interviewees
                    .Where(x =>
                        x.SexId == dictionary.BaseDictionaryId ||
                        x.SeniorityId == dictionary.BaseDictionaryId ||
                        x.EducationId == dictionary.BaseDictionaryId ||
                        x.PlaceOfResidenceId == dictionary.BaseDictionaryId ||
                        x.MartialStatusId == dictionary.BaseDictionaryId ||
                        x.AgeId == dictionary.BaseDictionaryId ||
                        x.MaterialStatusId == dictionary.BaseDictionaryId)
                    .Select(x => x.IntervieweeId).ToList();

                foreach (var category in questionnaire.Categories)
                {
                    var questionsIds = category.Questions.Select(x => x.QuestionId);
                    var catogryScores = context.Scores
                        .Where(x => x.QuestionnaireId == questionnaireId)
                        .Where(x => x.CategoryId == category.CategoryId)
                        .Where(x => intervieweesIds.Contains(x.IntervieweeId));

                    model.Add(new ResultGeneral()
                    {
                        KeyType = questionnaire.KeyType,
                        BaseDictionaryId = dictionary.BaseDictionaryId,
                        BaseDictionaryValue = dictionary.Value,
                        DictionaryTypeName = dictionary.DictionaryType.GetDescription(),
                        CategoryId = category.CategoryId,
                        CategoryName = category.Name,
                        IntervieweeCount = intervieweesIds.Count,
                        PointsAvailableToGet = category.Questions.Sum(x => x.Answers.Max(p => p.Points)),
                        AveragePointsEarned = (int)catogryScores.Select(x => x.PointsEarned).DefaultIfEmpty(0).Average(),
                        AverageScoreValue = catogryScores.Select(x => x.Value).DefaultIfEmpty(0).Average()
                    });
                }

                var scores = context.Scores
                        .Where(x => x.QuestionnaireId == questionnaireId)
                        .Where(x => !x.CategoryId.HasValue)
                        .Where(x => intervieweesIds.Contains(x.IntervieweeId));

                model.Add(new ResultGeneral()
                {
                    KeyType = questionnaire.KeyType,
                    BaseDictionaryId = dictionary.BaseDictionaryId,
                    BaseDictionaryValue = dictionary.Value,
                    DictionaryTypeName = dictionary.DictionaryType.GetDescription(),
                    CategoryId = null,
                    CategoryName = "#",
                    IntervieweeCount = intervieweesIds.Count,
                    PointsAvailableToGet = questionnaire.Questions.Sum(x => x.Answers.Max(p => p.Points)),
                    AveragePointsEarned = (int)scores.Select(x => x.PointsEarned).DefaultIfEmpty(0).Average(),
                    AverageScoreValue = scores.Select(x => x.Value).DefaultIfEmpty(0).Average()
                });
            }

            return model;
        }
        #endregion

        #region GetIntervieweeResults()
        public List<IntervieweeQuestionnaireResult> GetIntervieweeResults(List<int> intervieweesIds)
        {
            var intervieweeQuestionnaireResults = new List<IntervieweeQuestionnaireResult>();

            foreach (var questionnaire in context.Questionnaires)
            {
                var intervieweeQuestionnaireResult = new IntervieweeQuestionnaireResult();
                intervieweeQuestionnaireResult.QuestionnaireId = questionnaire.QuestionnaireId;
                intervieweeQuestionnaireResult.QuestionnaireName = questionnaire.Name;
                intervieweeQuestionnaireResult.IntervieweeResults = new List<IntervieweeResult>();

                foreach (var category in questionnaire.Categories)
                {
                    var categoryValues = context.Scores
                        .Where(x => x.QuestionnaireId == questionnaire.QuestionnaireId)
                        .Where(x => x.CategoryId == category.CategoryId)
                        .Where(x => intervieweesIds.Contains(x.IntervieweeId))
                        .Select(x => x.Value);

                    var categoryResult = new IntervieweeResult();
                    categoryResult.CategoryId = category.CategoryId;
                    categoryResult.CategoryName = category.Name;
                    categoryResult.SetScores(categoryValues);

                    intervieweeQuestionnaireResult.IntervieweeResults.Add(categoryResult);
                }

                var values = context.Scores
                    .Where(x => x.QuestionnaireId == questionnaire.QuestionnaireId)
                    .Where(x => !x.CategoryId.HasValue)
                    .Where(x => intervieweesIds.Contains(x.IntervieweeId))
                    .Select(x => x.Value);

                var result = new IntervieweeResult();
                result.CategoryId = null;
                result.CategoryName = "#";
                result.SetScores(values);

                intervieweeQuestionnaireResult.IntervieweeResults.Add(result);
                intervieweeQuestionnaireResults.Add(intervieweeQuestionnaireResult);
            }

            return intervieweeQuestionnaireResults;
        }

        public List<Result> GetResultsByDict(int questionnaireId, Dictionary<int, int> questionIdValue)
        {
            var questionnaire = context.Questionnaires.Find(questionnaireId);
            var results = new List<Result>();
            foreach (var item in questionIdValue)
            {
                var question = questionnaire.Questions.FirstOrDefault(x => x.QuestionId == item.Key);
                results.Add(new Result()
                {
                    QuestionnaireId = questionnaire.QuestionnaireId,
                    QuestionId = question.QuestionId,
                    AnswerId = question.Answers.FirstOrDefault(x => x.Value == item.Value).AnswerId
                });
            }

            return results;
        }
        #endregion
    }
}