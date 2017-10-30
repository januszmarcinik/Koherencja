using JanuszMarcinik.Mvc.Domain.Application.Models;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class ValueViewModel
    {
        private const string CssClassLabelSuccess = "label label-success";
        private const string CssClassLabelDanger = "label label-danger";
        private const string CssClassLabelInfo = "label label-info";

        public int Count { get; set; }
        public int TotalCount { get; set; }

        public string Badge { get; set; }

        public decimal Value { get; private set; }
        public string ResultCssClass { get; private set; }

        public void SetValueByPercentage()
        {
            if (this.TotalCount > 0)
            {
                Value = (decimal)this.Count / (decimal)this.TotalCount * 100;
            }
            else
            {
                Value = 0;
            }

            if (this.Value > 50)
            {
                ResultCssClass = CssClassLabelSuccess;
            }
            else if (this.Value < 10)
            {
                ResultCssClass = CssClassLabelDanger;
            }
            else
            {
                ResultCssClass = CssClassLabelInfo;
            }
        }

        public void SetValue(decimal value, ResultValueMark resultValueMark)
        {
            this.Value = value;

            if (resultValueMark == ResultValueMark.High)
            {
                ResultCssClass = CssClassLabelSuccess;
            }
            else if (resultValueMark == ResultValueMark.Low)
            {
                ResultCssClass = CssClassLabelDanger;
            }
            else
            {
                ResultCssClass = CssClassLabelInfo;
            }
        }
    }
}