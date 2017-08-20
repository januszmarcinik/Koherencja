using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires
{
    [Table("Categories", Schema = "Questionnaire")]
    public class Category : IApplicationEntity
    {
        public Category()
        {
            this.Questions = new HashSet<Question>();
        }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public int QuestionnaireId { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}