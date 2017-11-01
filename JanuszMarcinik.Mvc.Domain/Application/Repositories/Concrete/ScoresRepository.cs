using JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires;
using JanuszMarcinik.Mvc.Domain.Application.Repositories.Abstract;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Linq;
using JanuszMarcinik.Mvc.Domain.Application.Models;

namespace JanuszMarcinik.Mvc.Domain.Application.Repositories.Concrete
{
    public class ScoresRepository : IScoresRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void RecalculateScores()
        {
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Questionnaire].[Scores]");

            var intervieweeIds = context.Interviewees.Select(x => x.IntervieweeId).ToList();
            foreach (var id in intervieweeIds)
            {
                Create(id);
            }
        }

        public void Create(int intervieweeId)
        {
            CreateForLOTR(intervieweeId);
            CreateForIZZ(intervieweeId);
            CreateForWHOQOL(intervieweeId);

            context.SaveChanges();
        }

        #region CreateForLOTR()
        private void CreateForLOTR(int intervieweeId)
        {
            var lotr = context.Questionnaires.FirstOrDefault(x => x.KeyType == KeyType.LOTR);
            var results = context.Results
                .Where(x => x.IntervieweeId == intervieweeId)
                .Where(x => x.QuestionnaireId == lotr.QuestionnaireId);

            var score = new Score()
            {
                QuestionnaireId = lotr.QuestionnaireId,
                IntervieweeId = intervieweeId,
                CategoryId = null,
                PointsAvailableToGet = lotr.Questions.Sum(x => x.Answers.Max(p => p.Points)),
                PointsEarned = results.Sum(x => x.Answer.Points)
            };

            if (score.PointsEarned <= 6)
                score.Value = 1;
            else if (score.PointsEarned <= 8)
                score.Value = 2;
            else if (score.PointsEarned <= 10)
                score.Value = 3;
            else if (score.PointsEarned <= 12)
                score.Value = 4;
            else if (score.PointsEarned <= 14)
                score.Value = 5;
            else if (score.PointsEarned <= 16)
                score.Value = 6;
            else if (score.PointsEarned <= 18)
                score.Value = 7;
            else if (score.PointsEarned <= 20)
                score.Value = 8;
            else if (score.PointsEarned <= 22)
                score.Value = 9;
            else if (score.PointsEarned <= 24)
                score.Value = 10;

            context.Scores.Add(score);
        }
        #endregion

        #region CreateForIZZ()
        private void CreateForIZZ(int intervieweeId)
        {
            var izz = context.Questionnaires.FirstOrDefault(x => x.KeyType == KeyType.IZZ);
            var results = context.Results
                .Where(x => x.QuestionnaireId == izz.QuestionnaireId)
                .Where(x => x.IntervieweeId == intervieweeId);

            foreach (var category in izz.Categories)
            {
                var questionsIds = category.Questions.Select(x => x.QuestionId);
                var categoryScore = new Score()
                {
                    QuestionnaireId = izz.QuestionnaireId,
                    IntervieweeId = intervieweeId,
                    CategoryId = category.CategoryId,
                    PointsAvailableToGet = category.Questions.Sum(x => x.Answers.Max(p => p.Points)),
                    PointsEarned = results.Where(x => questionsIds.Contains(x.QuestionId)).Sum(x => x.Answer.Points)
                };
                categoryScore.Value = categoryScore.PointsEarned / 6M;

                context.Scores.Add(categoryScore);
            }

            var score = new Score()
            {
                QuestionnaireId = izz.QuestionnaireId,
                IntervieweeId = intervieweeId,
                CategoryId = null,
                PointsAvailableToGet = izz.Questions.Sum(x => x.Answers.Max(p => p.Points)),
                PointsEarned = results.Sum(x => x.Answer.Points),
                Value = 0
            };

            var intervieweeSexValue = context.Interviewees.Find(intervieweeId).Sex.Value;

            if (intervieweeSexValue == "Mężczyzna")
            {
                if (score.PointsEarned <= 50)
                    score.Value = 1;
                else if (score.PointsEarned <= 58)
                    score.Value = 2;
                else if (score.PointsEarned <= 65)
                    score.Value = 3;
                else if (score.PointsEarned <= 71)
                    score.Value = 4;
                else if (score.PointsEarned <= 78)
                    score.Value = 5;
                else if (score.PointsEarned <= 86)
                    score.Value = 6;
                else if (score.PointsEarned <= 93)
                    score.Value = 7;
                else if (score.PointsEarned <= 101)
                    score.Value = 8;
                else if (score.PointsEarned <= 108)
                    score.Value = 9;
                else if (score.PointsEarned <= 120)
                    score.Value = 10;
            }
            else if (intervieweeSexValue == "Kobieta")
            {
                if (score.PointsEarned <= 53)
                    score.Value = 1;
                else if (score.PointsEarned <= 62)
                    score.Value = 2;
                else if (score.PointsEarned <= 70)
                    score.Value = 3;
                else if (score.PointsEarned <= 77)
                    score.Value = 4;
                else if (score.PointsEarned <= 84)
                    score.Value = 5;
                else if (score.PointsEarned <= 91)
                    score.Value = 6;
                else if (score.PointsEarned <= 98)
                    score.Value = 7;
                else if (score.PointsEarned <= 104)
                    score.Value = 8;
                else if (score.PointsEarned <= 111)
                    score.Value = 9;
                else if (score.PointsEarned <= 120)
                    score.Value = 10;
            }

            context.Scores.Add(score);
        }
        #endregion

        #region CreateForWHOQOL()
        private void CreateForWHOQOL(int intervieweeId)
        {
            var whoqol = context.Questionnaires.FirstOrDefault(x => x.KeyType == KeyType.WHOQOL);
            var results = context.Results
                .Where(x => x.QuestionnaireId == whoqol.QuestionnaireId)
                .Where(x => x.IntervieweeId == intervieweeId);

            foreach (var category in whoqol.Categories)
            {
                var questionsIds = category.Questions.Select(x => x.QuestionId);
                var categoryScore = new Score()
                {
                    QuestionnaireId = whoqol.QuestionnaireId,
                    IntervieweeId = intervieweeId,
                    CategoryId = category.CategoryId,
                    PointsAvailableToGet = category.Questions.Sum(x => x.Answers.Max(p => p.Points)),
                    PointsEarned = results.Where(x => questionsIds.Contains(x.QuestionId)).Sum(x => x.Answer.Points)
                };
                categoryScore.Value = (categoryScore.PointsEarned * 4) / questionsIds.Count();

                context.Scores.Add(categoryScore);
            }

            var score = new Score()
            {
                QuestionnaireId = whoqol.QuestionnaireId,
                IntervieweeId = intervieweeId,
                CategoryId = null,
                PointsAvailableToGet = whoqol.Questions.Sum(x => x.Answers.Max(p => p.Points)),
                PointsEarned = results.Sum(x => x.Answer.Points)
            };
            score.Value = (score.PointsEarned * 4) / whoqol.Questions.Count;

            context.Scores.Add(score);
        }
        #endregion

        #region GetScoreValueMark()
        public ScoreValueMark GetScoreValueMark(KeyType keyType, bool isCategorized, decimal value)
        {
            switch (keyType)
            {
                case KeyType.LOTR:
                    return GetBySten((int)value);
                case KeyType.IZZ:
                    {
                        if (isCategorized)
                        {
                            if (value < 2.5M)
                            {
                                return ScoreValueMark.Low;
                            }
                            else if (value > 3.5M)
                            {
                                return ScoreValueMark.High;
                            }
                            else
                            {
                                return ScoreValueMark.Medium;
                            }
                        }
                        else
                        {
                            return GetBySten((int)value);
                        }
                    }
                case KeyType.WHOQOL:
                    {
                        if (value < 10)
                        {
                            return ScoreValueMark.Low;
                        }
                        else if (value > 15)
                        {
                            return ScoreValueMark.High;
                        }
                        else
                        {
                            return ScoreValueMark.Medium;
                        }
                    }
                default:
                    return ScoreValueMark.Medium;
            }
        }

        private static ScoreValueMark GetBySten(int sten)
        {
            if (sten < 5)
            {
                return ScoreValueMark.Low;
            }
            else if (sten > 7)
            {
                return ScoreValueMark.High;
            }
            else
            {
                return ScoreValueMark.Medium;
            }
        }
        #endregion
    }
}