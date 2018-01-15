using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using System.Collections.Generic;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Linq;
using JanuszMarcinik.Mvc.Domain.Application.Models;
using System;
using JanuszMarcinik.Mvc.Domain.Application.Entities.Dictionaries;

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
        public List<ResultDetail> GetResultDetails(int questionnaireId)
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
        public List<ResultGeneral> GetResultsGeneral(int questionnaireId)
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
                        AverageScoreValue = catogryScores.Select(x => x.Value).DefaultIfEmpty(0).Average(),
                        PointsRange = questionnaire.KeyType.GetRange(true, category.Questions.Count)
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
                    AverageScoreValue = scores.Select(x => x.Value).DefaultIfEmpty(0).Average(),
                    PointsRange = questionnaire.KeyType.GetRange(false)
                });
            }

            return model;
        }
        #endregion

        #region GetResultsPearsonCorrelations()
        public List<ResultGeneral> GetResultsPearsonCorrelations()
        {
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

                var correlations = GetIntervieweePearsonCorrelations(intervieweesIds);
                foreach (var correlation in correlations)
                {
                    model.Add(new ResultGeneral()
                    {
                        BaseDictionaryId = dictionary.BaseDictionaryId,
                        BaseDictionaryValue = dictionary.Value,
                        DictionaryTypeName = dictionary.DictionaryType.GetDescription(),
                        CategoryId = correlations.IndexOf(correlation),
                        CategoryName = $"{correlation.XAxisName}<br/>{correlation.YAxisName}",
                        IntervieweeCount = intervieweesIds.Count,
                        AverageScoreValue = correlation.Value.ToDecimal()
                    });
                }
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
                var intervieweeQuestionnaireResult = new IntervieweeQuestionnaireResult
                {
                    QuestionnaireId = questionnaire.QuestionnaireId,
                    QuestionnaireName = questionnaire.Name,
                    IntervieweeResults = new List<IntervieweeResult>()
                };

                foreach (var category in questionnaire.Categories)
                {
                    var categoryValues = context.Scores
                        .Where(x => x.QuestionnaireId == questionnaire.QuestionnaireId)
                        .Where(x => x.CategoryId == category.CategoryId)
                        .Where(x => intervieweesIds.Contains(x.IntervieweeId))
                        .Select(x => x.Value);

                    var categoryResult = new IntervieweeResult
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.Name,
                        ResultRange = questionnaire.KeyType.GetRange(true, category.Questions.Count)
                    };
                    categoryResult.SetScores(categoryValues);

                    intervieweeQuestionnaireResult.IntervieweeResults.Add(categoryResult);
                }

                var values = context.Scores
                    .Where(x => x.QuestionnaireId == questionnaire.QuestionnaireId)
                    .Where(x => !x.CategoryId.HasValue)
                    .Where(x => intervieweesIds.Contains(x.IntervieweeId))
                    .Select(x => x.Value);

                var result = new IntervieweeResult
                {
                    CategoryId = null,
                    CategoryName = "#",
                    ResultRange = questionnaire.KeyType.GetRange(false)
                };
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

        #region GetIntervieweeDetails()
        public List<IntervieweeDetail> GetIntervieweeDetails(int questionnaireId, List<int> intervieweesIds)
        {
            var model = new List<IntervieweeDetail>();

            var questionnaire = context.Questionnaires.Find(questionnaireId);
            var questions = questionnaire.Questions.OrderBy(x => x.OrderNumber);

            foreach (var question in questions)
            {
                var results = question.Results.Where(x => intervieweesIds.Contains(x.IntervieweeId));
                var answers = question.Answers.OrderBy(x => x.OrderNumber);

                var detail = new IntervieweeDetail()
                {
                    QuestionId = question.QuestionId,
                    QuestionText = $"{question.OrderNumber}. {question.Text}",
                    IntervieweeCount = intervieweesIds.Count,
                    Answers = new List<IntervieweeDetailAnswer>()
                };

                foreach (var answer in answers)
                {
                    var answerText = string.IsNullOrEmpty(answer.Description) ? answer.Value.ToString() : answer.Description;
                    detail.Answers.Add(new IntervieweeDetailAnswer(answerText, results.Where(x => x.AnswerId == answer.AnswerId).Count(), detail.IntervieweeCount));
                }

                model.Add(detail);
            }

            return model;
        }
        #endregion

        #region GetIntervieweePearsonCorrelations()
        public List<PearsonCorrelation> GetIntervieweePearsonCorrelations(List<int> intervieweesIds)
        {
            var model = new List<PearsonCorrelation>();

            var combinations = GetQuestionnariesCombinations();
            foreach (var combination in combinations)
            {
                var questionnaireA = context.Questionnaires.Find(combination.Item1);
                var questionnaireB = context.Questionnaires.Find(combination.Item2);
                if (questionnaireA != null && questionnaireB != null)
                {
                    model.Add(new PearsonCorrelation(
                        title: $"{questionnaireA.Name} vs {questionnaireB.Name}",
                        xAxisName: questionnaireA.Name,
                        yAxisName: questionnaireB.Name,
                        xAxisSeries: context.Scores
                            .Where(x => x.QuestionnaireId == combination.Item1)
                            .Where(x => !x.CategoryId.HasValue)
                            .Where(x => intervieweesIds.Contains(x.IntervieweeId))
                            .Select(x => (double)x.Value).ToList(),
                        yAxisSeries: context.Scores
                            .Where(x => x.QuestionnaireId == combination.Item2)
                            .Where(x => !x.CategoryId.HasValue)
                            .Where(x => intervieweesIds.Contains(x.IntervieweeId))
                            .Select(x => (double)x.Value).ToList(),
                        xAxisMin: questionnaireA.KeyType.GetMinRange(),
                        xAxisMax: questionnaireA.KeyType.GetMaxRange(),
                        yAxisMin: questionnaireB.KeyType.GetMinRange(),
                        yAxisMax: questionnaireB.KeyType.GetMaxRange()));
                }
            }

            return model;
        }
        #endregion

        #region GenerateRandom()
        public void GenerateRandom()
        {
            var data = new RandomResults(context.Questionnaires, context.BaseDictionaries);
            IScoresRepository scoresRepository = new ScoresRepository();

            for (int i = 0; i < 25; i++)
            {
                var interviewee = new Interviewee()
                {
                    InterviewDate = DateTime.Now,
                    AgeId = data.RandomInterviewee(DictionaryType.Age),
                    EducationId = data.RandomInterviewee(DictionaryType.Education),
                    MartialStatusId = data.RandomInterviewee(DictionaryType.MartialStatus),
                    MaterialStatusId = data.RandomInterviewee(DictionaryType.MaterialStatus),
                    PlaceOfResidenceId = data.RandomInterviewee(DictionaryType.PlaceOfResidence),
                    SeniorityId = data.RandomInterviewee(DictionaryType.Seniority),
                    SexId = data.RandomInterviewee(DictionaryType.Sex)
                };

                context.Interviewees.Add(interviewee);
                context.SaveChanges();

                var random = new Random();

                foreach (var questionnaire in data.Questionnaires)
                {
                    foreach (var question in questionnaire.Questions)
                    {
                        context.Results.Add(new Result()
                        {
                            IntervieweeId = interviewee.IntervieweeId,
                            QuestionnaireId = questionnaire.QuestionnaireId,
                            QuestionId = question.QuestionId,
                            AnswerId = question.Random()
                        });
                    }
                }

                context.SaveChanges();

                scoresRepository.Create(interviewee.IntervieweeId);
            }
        }
        #endregion


        #region Helpers

        private List<Tuple<int, int>> GetQuestionnariesCombinations()
        {
            var combinations = new List<Tuple<int, int>>();
            var questionnaireIds = context.Questionnaires.Select(x => x.QuestionnaireId).ToList();
            foreach (var questionnaire in context.Questionnaires)
            {
                foreach (var id in questionnaireIds.Where(x => x != questionnaire.QuestionnaireId))
                {
                    var combinationA = new Tuple<int, int>(questionnaire.QuestionnaireId, id);
                    var combinationB = new Tuple<int, int>(id, questionnaire.QuestionnaireId);
                    if (!combinations.Contains(combinationA) && !combinations.Contains(combinationB))
                    {
                        combinations.Add(combinationA);
                    }
                }
            }

            return combinations;
        }

        #endregion
    }
}