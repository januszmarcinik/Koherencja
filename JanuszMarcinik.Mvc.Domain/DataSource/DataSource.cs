using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace JanuszMarcinik.Mvc.Domain.DataSource
{
    public abstract class DataSource<TModel> : IGrid where TModel : class
    {
        public IEnumerable<TModel> Data { get; set; }

        public List<GridHeader> Headers { get; set; }
        public List<GridRow> Rows { get; set; }

        public int PageIndex { get; set; }
        public PageSize PageSize { get; set; }
        public int TotalRows { get; set; }
        public string PagerResult
        {
            get
            {
                var startIndex = this.PageIndex * (int)this.PageSize - (int)this.PageSize + 1;
                var endIndex = startIndex + (int)this.PageSize - 1;

                if (startIndex >= this.TotalRows)
                {
                    startIndex = this.TotalRows;
                }

                if (endIndex >= this.TotalRows)
                {
                    endIndex = this.TotalRows;
                }

                return $"Wyniki: {startIndex}-{endIndex} z {this.TotalRows}";
            }
        }

        public string LastOrderBy { get; set; }
        public string OrderBy { get; set; }
        public SortOrder SortOrder { get; set; }

        #region Initialize()
        public virtual void Initialize()
        {
            if (this.Data == null)
            {
                this.Data = new List<TModel>();
            }

            Filter();

            this.Headers = new List<GridHeader>();
            this.TotalRows = this.Data.Count();
            if (this.TotalRows > 0)
            {
                SetHeaders(this.Data.First().GetType().GetProperties());
            }

            Pager();
            Sort();
            SetRows();
            SetEditActions();
        }
        #endregion

        #region SetHeaders()
        private void SetHeaders(PropertyInfo[] properties)
        {
            this.Headers = new List<GridHeader>();


            foreach (var prop in properties)
            {
                var header = new GridHeader()
                {
                    DisplayName = prop.Name,
                    PropertyName = prop.Name
                };

                var gridAttribute = prop.GetCustomAttribute<GridAttribute>();
                if (gridAttribute != null)
                {
                    header.Order = gridAttribute.Order;
                    header.DataType = gridAttribute.DataType;

                    if (gridAttribute.DisplayName != null)
                    {
                        header.DisplayName = gridAttribute.DisplayName;
                    }
                    else
                    {
                        var displayAttribute = prop.GetCustomAttribute<DisplayAttribute>();
                        if (displayAttribute != null)
                        {
                            header.DisplayName = displayAttribute.Name;
                        }
                    }

                    this.Headers.Add(header);
                }
            }

            if (properties.Count() > 1)
            {
                this.Headers = this.Headers.OrderBy(x => x.Order).ToList();
            }
        }
        #endregion

        #region SetRows()
        private void SetRows()
        {
            this.Rows = new List<GridRow>();

            foreach (var item in this.Data)
            {
                var row = new GridRow();

                foreach (var prop in this.Headers)
                {
                    var cell = new GridCell();
                    cell.DataType = prop.DataType;

                    try
                    {
                        if (prop.DataType == GridDataType.Enum)
                        {
                            var enumValue = (Enum)item.GetType().GetProperty(prop.PropertyName).GetValue(item);
                            cell.Value = enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttribute<DescriptionAttribute>(false).Description;
                        }
                        else
                        {
                            cell.Value = item.GetType().GetProperty(prop.PropertyName).GetValue(item).ToString();
                        }
                    }
                    catch
                    {
                        cell.Value = string.Empty;
                    }

                    row.Values.Add(cell);
                }

                this.Rows.Add(row);
            }
        }
        #endregion

        #region Pager()
        private void Pager()
        {
            if (this.PageSize == PageSize.Unset)
            {
                this.PageSize = PageSize.Ten;
            }
            if (this.PageIndex == 0)
            {
                this.PageIndex = 1;
            }

            this.Data = this.Data
                .Skip((this.PageIndex * (int)this.PageSize) - (int)this.PageSize)
                .Take((int)this.PageSize);
        }
        #endregion

        #region Sort()
        private void Sort()
        {
            if (!string.IsNullOrEmpty(this.OrderBy))
            {
                if (this.SortOrder == SortOrder.Unset)
                {
                    this.SortOrder = SortOrder.Ascending;
                }
                else if (this.SortOrder == SortOrder.Ascending && this.LastOrderBy == this.OrderBy)
                {
                    this.SortOrder = SortOrder.Descending;
                }
                else if (this.SortOrder == SortOrder.Descending)
                {
                    this.SortOrder = SortOrder.Ascending;
                }

                if (this.SortOrder == SortOrder.Ascending)
                {
                    this.Data = this.Data.OrderBy(x => x.GetType().GetProperty(this.OrderBy).GetValue(x, null));
                }
                else if (this.SortOrder == SortOrder.Descending)
                {
                    this.Data = this.Data.OrderByDescending(x => x.GetType().GetProperty(this.OrderBy).GetValue(x, null));
                }
            }

            this.LastOrderBy = this.OrderBy;
        }
        #endregion

        protected abstract void SetEditActions();

        protected abstract void Filter();
    }
}