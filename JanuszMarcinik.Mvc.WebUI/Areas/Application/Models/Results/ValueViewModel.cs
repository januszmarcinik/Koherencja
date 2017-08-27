namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class ValueViewModel
    {
        public int Count { get; set; }
        public int TotalCount { get; set; }

        public string Badge { get; set; }

        public decimal Percentage
        {
            get
            {
                if (this.TotalCount > 0)
                {
                    return (decimal)this.Count / (decimal)this.TotalCount * 100;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string ResultValue
        {
            get { return $"{this.Percentage.ToString("N2")} %"; }
        }

        public string ResultCssClass
        {
            get
            {
                if (this.Percentage > 50)
                {
                    return "label label-success";
                }
                else if (this.Percentage < 10)
                {
                    return "label label-danger";
                }
                else
                {
                    return "label label-info";
                }
            }
        }
    }
}