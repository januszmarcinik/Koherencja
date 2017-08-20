using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IAnswersRepository
    {
        Answer GetById(int id);
        IEnumerable<Answer> GetList(int questionId);
        Answer Create(Answer entity);
        Answer Update(Answer entity);
        void Delete(int id);
        List<string> GetDescriptions(int questionId);
    }
}