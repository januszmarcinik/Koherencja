using JanuszMarcinik.Mvc.Domain.DataSource;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Answers
{
    public class AnswerDataSource : DataSource<AnswerViewModel>
    {
        public int QuestionId { get; set; }
        public int QuestionnaireId { get; set; }
        public string QuestionText { get; set; }

        protected override void Filter()
        {
        }

        protected override void SetEditActions()
        {
            foreach (var row in this.Rows)
            {
                row.EditAction = MVC.Admin.Answers.Edit(row.PrimaryKey);
            }
        }
    }
}