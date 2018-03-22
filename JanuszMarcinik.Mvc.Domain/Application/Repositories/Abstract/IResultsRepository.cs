using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Models;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IResultsRepository
    {
        void CreateMany(List<Result> entities, int intervieweeId);
        List<ResultDetail> GetResultDetails(int questionnaireId);
        List<ResultGeneral> GetResultsGeneral(int questionnaireId);
        List<ResultGeneral> GetResultsPearsonCorrelations();

        List<IntervieweeQuestionnaireResult> GetIntervieweeResults(List<int> intervieweesIds);
        List<IntervieweeDetail> GetIntervieweeDetails(int questionnaireId, List<int> intervieweesIds);
        List<PearsonCorrelation> GetIntervieweePearsonCorrelations(List<int> intervieweesIds);
        List<DictionaryChart> GetDictionaryCharts(List<int> intervieweesIds);

        List<Result> GetResultsByDict(int questionnaireId, Dictionary<int, int> questionIdValue);
    }
}