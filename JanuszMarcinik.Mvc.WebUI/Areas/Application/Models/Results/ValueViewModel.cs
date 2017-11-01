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

        public string Value { get; private set; }
        public string ResultCssClass { get; private set; }

        public void SetValueByPercentage()
        {
            decimal value = 0;
            if (this.TotalCount > 0)
            {
                value = (decimal)this.Count / (decimal)this.TotalCount * 100;
            }
            else
            {
                value = 0;
            }

            if (value > 50)
            {
                ResultCssClass = CssClassLabelSuccess;
            }
            else if (value < 10)
            {
                ResultCssClass = CssClassLabelDanger;
            }
            else
            {
                ResultCssClass = CssClassLabelInfo;
            }

            Value = value.TwoOrZeroDecimalPlaces();
        }

        public void SetValue(decimal value, ScoreValueMark resultValueMark)
        {
            this.Value = value.TwoOrZeroDecimalPlaces();

            if (resultValueMark == ScoreValueMark.High)
            {
                ResultCssClass = CssClassLabelSuccess;
            }
            else if (resultValueMark == ScoreValueMark.Low)
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