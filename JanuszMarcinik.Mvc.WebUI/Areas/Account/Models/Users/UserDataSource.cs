using JanuszMarcinik.Mvc.Domain.DataSource;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Account.Models.Users
{
    public class UserDataSource : DataSource<UserViewModel>
    {
        protected override void Filter()
        {
        }

        protected override void SetEditActions()
        {
            foreach (var row in this.Rows)
            {
                row.EditAction = MVC.Account.Users.Edit(row.PrimaryKey);
            }
        }
    }
}