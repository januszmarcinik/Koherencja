using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.DataSource
{
    public interface IGrid
    {
        List<GridHeader> Headers { get; set; }
        List<GridRow> Rows { get; set; }

        int PageIndex { get; set; }
        PageSize PageSize { get; set; }
        int TotalRows { get; set; }
        string PagerResult { get; }

        string LastOrderBy { get; set; }
        string OrderBy { get; set; }
        SortOrder SortOrder { get; set; }
    }
}