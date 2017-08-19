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
        }

        public int QuestionnaireId { get; set; }

        public string Name { get; set; }
        public int OrderNumber { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}