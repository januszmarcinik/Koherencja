using JanuszMarcinik.Mvc.Domain.DataSource;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Questions
{
    public class QuestionDataSource : DataSource<QuestionViewModel>
    {
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }

        protected override void Filter()
        {
        }

        protected override void SetEditActions()
        {
            foreach (var row in this.Rows)
            {
                row.EditAction = MVC.Admin.Questions.Edit(row.PrimaryKey);
            }
        }
    }
}