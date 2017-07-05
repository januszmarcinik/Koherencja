﻿using JanuszMarcinik.Mvc.Domain.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JanuszMarcinik.Mvc.Domain.Application.Entities.Questionnaires
{
    [Table("Questions", Schema = "Questionnaire")]
    public class Question : IApplicationEntity
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.Results = new HashSet<Result>();
        }

        public long QuestionId { get; set; }

        public int OrderNumber { get; set; }
        public string Text { get; set; }

        public long QuestionnaireId { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}