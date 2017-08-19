using JanuszMarcinik.Mvc.Domain.DataSource;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Account.Models.Roles
{
    public class RoleDataSource : DataSource<RoleViewModel>
    {
        protected override void Filter()
        {
        }

        protected override void SetEditActions()
        {
            foreach (var row in this.Rows)
            {
                row.EditAction = MVC.Account.Roles.Edit(row.PrimaryKey);
            }
        }
    }
}