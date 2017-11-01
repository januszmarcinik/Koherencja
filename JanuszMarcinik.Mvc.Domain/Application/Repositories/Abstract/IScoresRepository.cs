using JanuszMarcinik.Mvc.Domain.Application.Models;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract
{
    public interface IScoresRepository
    {
        void RecalculateScores();
        void Create(int intervieweeId);
        ScoreValueMark GetScoreValueMark(KeyType keyType, bool isCategorized, decimal value);
    }
}