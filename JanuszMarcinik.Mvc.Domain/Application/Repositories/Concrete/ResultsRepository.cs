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

        public IEnumerable<Result> GetList(int questionId)
        {
            return context.Results.Where(x => x.QuestionId == questionId);
        }

        public IEnumerable<ResultDetail> GetResultDetails(int questionnaireId)
        {
            var questionnaire = context.Questionnaires.Find(questionnaireId);

            var model = new List<ResultDetail>();

            foreach (var question in questionnaire.Questions)
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

                    foreach (var answer in question.Answers)
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
                            TotalAnswersCount = totalAnswersCount
                        });
                    }
                }
            }

            return model
                .OrderBy(x => x.QuestionId)
                .ThenBy(x => x.AnswerId)
                .ThenBy(x => x.DictionaryTypeName)
                .ThenBy(x => x.BaseDictionaryValue);
        }
    }
}