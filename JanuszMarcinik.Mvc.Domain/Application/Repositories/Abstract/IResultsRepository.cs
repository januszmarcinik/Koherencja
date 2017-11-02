using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Models;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IResultsRepository
    {
        void CreateMany(List<Result> entities, int intervieweeId);
        IEnumerable<ResultDetail> GetResultDetails(int questionnaireId);
        IEnumerable<ResultGeneral> GetResultsGeneral(int questionnaireId);
        List<IntervieweeQuestionnaireResult> GetIntervieweeResults(List<int> intervieweesIds);
        List<IntervieweeDetail> GetIntervieweeDetails(int questionnaireId, List<int> intervieweesIds);

        List<Result> GetResultsByDict(int questionnaireId, Dictionary<int, int> questionIdValue);

        void GenerateRandom();
    }
}