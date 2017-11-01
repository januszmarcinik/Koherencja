using JanuszMarcinik.Mvc.Domain.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires
{
    [Table("Scores", Schema = "Questionnaire")]
    public class Score : IApplicationEntity
    {
        public int ScoreId { get; set; }

        public int IntervieweeId { get; set; }
        public virtual Interviewee Interviewee { get; set; }

        public int QuestionnaireId { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int PointsAvailableToGet { get; set; }
        public int PointsEarned { get; set; }
        public decimal Value { get; set; }
    }
}