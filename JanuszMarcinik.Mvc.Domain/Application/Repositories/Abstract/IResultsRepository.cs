using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Models;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IResultsRepository
    {
        void CreateMany(List<Result> entities, int intervieweeId);
        IEnumerable<Result> GetList(int questionId);
        IEnumerable<ResultDetail> GetResultDetails(int questionnaireId);
    }
}