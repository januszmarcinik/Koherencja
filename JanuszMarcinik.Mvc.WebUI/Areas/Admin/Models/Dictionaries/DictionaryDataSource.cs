using JanuszMarcinik.Mvc.Domain.DataSource;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models.Dictionaries
{
    public class DictionaryDataSource : DataSource<DictionaryViewModel>
    {
        protected override void Filter()
        {
        }

        protected override void SetEditActions()
        {
            foreach (var row in this.Rows)
            {
                row.EditAction = MVC.Admin.Dictionaries.Edit(row.PrimaryKey);
            }
        }
    }
}