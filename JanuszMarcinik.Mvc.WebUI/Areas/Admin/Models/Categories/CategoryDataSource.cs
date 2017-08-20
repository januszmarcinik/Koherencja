using JanuszMarcinik.Mvc.Domain.DataSource;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Categories
{
    public class CategoryDataSource : DataSource<CategoryViewModel>
    {
        protected override void Filter()
        {
        }

        protected override void SetEditActions()
        {
            foreach (var row in this.Rows)
            {
                row.EditAction = MVC.Admin.Categories.Edit(row.PrimaryKey);
            }
        }
    }
}