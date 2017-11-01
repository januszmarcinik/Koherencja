using JanuszMarcinik.Mvc.Domain.Application.Keys;
using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires
{
    [Table("Questionnaires", Schema = "Questionnaire")]
    public class Questionnaire : IApplicationEntity
    {
        public Questionnaire()
        {
            this.Questions = new HashSet<Question>();
            this.Results = new HashSet<Result>();
            this.Categories = new HashSet<Category>();
            this.Scores = new HashSet<Score>();
        }

        public int QuestionnaireId { get; set; }

        public string Name { get; set; }
        public int OrderNumber { get; set; }
        public string Description { get; set; }
        public KeyType KeyType { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}