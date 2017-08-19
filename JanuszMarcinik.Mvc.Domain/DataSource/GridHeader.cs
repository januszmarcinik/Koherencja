namespace JanuszMarcinik.Mvc.Domain.DataSource
{
    public class GridHeader
    {
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }

        public GridDataType DataType { get; set; }
        public int Order { get; set; }
    }
}