using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IIntervieweesRepository
    {
        Interviewee Create(Interviewee entity);
        IEnumerable<Interviewee> GetList();
    }
}