using System;

namespace JanuszMarcinik.Mvc.Domain.DataSource
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GridAttribute : Attribute
    {
        public GridAttribute()
        {
            this.DataType = GridDataType.Text;
            this.Order = 1;
            this.DisplayName = null;
        }

        public GridDataType DataType { get; set; }
        public int Order { get; set; }
        public string DisplayName { get; set; }
    }

    public enum GridDataType
    {
        Text,
        PhotoPath,
        DateTime,
        Date,
        Time,
        Link,
        PrimaryKey,
        Enum
    }
}