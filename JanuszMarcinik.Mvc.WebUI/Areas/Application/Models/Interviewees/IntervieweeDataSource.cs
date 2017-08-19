using JanuszMarcinik.Mvc.Domain.DataSource;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Interviewees
{
    public class IntervieweeDataSource : DataSource<IntervieweeViewModel>
    {
        protected override void Filter()
        {
        }

        protected override void SetEditActions()
        {
            foreach (var row in this.Data)
            {
                //row.ListText = "Pytania";
                //row.ListAction = MVC.Admin.Questions.List(row.PrimaryKeyId);
                //row.EditAction = MVC.Admin.Questionnaires.Edit(row.PrimaryKeyId);
                //row.DeleteAction = MVC.Admin.Questionnaires.Delete(row.PrimaryKeyId);
            }
        }
    }
}