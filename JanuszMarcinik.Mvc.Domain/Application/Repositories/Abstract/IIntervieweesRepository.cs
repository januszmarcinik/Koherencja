using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IIntervieweesRepository
    {
        Interviewee GetById(int id);
        Interviewee Create(Interviewee entity);
        IEnumerable<Interviewee> GetList();
        void Delete(int id);
    }
}