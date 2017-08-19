using JanuszMarcinik.Mvc.Domain.DataSource;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Questionnaires
{
    public class QuestionnaireDataSource : DataSource<QuestionnaireViewModel>
    {
        protected override void Filter()
        {
        }

        protected override void SetEditActions()
        {
            foreach (var row in this.Rows)
            {
                row.EditAction = MVC.Admin.Questionnaires.Edit(row.PrimaryKey);
            }
        }
    }
}