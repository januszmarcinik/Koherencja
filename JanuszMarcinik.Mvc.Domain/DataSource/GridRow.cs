using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.Domain.DataSource
{
    public class GridRow
    {
        public GridRow()
        {
            this.Values = new List<GridCell>();
        }

        public int PrimaryKey
        {
            get
            {
                int value = 0;

                var primaryKey = this.Values.FirstOrDefault(x => x.DataType == GridDataType.PrimaryKey);
                if (primaryKey != null)
                {
                    int.TryParse(primaryKey.Value, out value);
                    return value;
                }
                else
                {
                    return value;
                }
            }
        }

        public List<GridCell> Values { get; set; }
        public ActionResult EditAction { get; set; }
    }
}