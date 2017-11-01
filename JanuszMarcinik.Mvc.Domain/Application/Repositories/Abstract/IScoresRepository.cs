using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IScoresRepository
    {
        void RecalculateScores();
        void Create(int intervieweeId);
    }
}