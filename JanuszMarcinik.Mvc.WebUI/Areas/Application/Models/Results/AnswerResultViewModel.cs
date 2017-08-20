namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class AnswerResultViewModel
    {
        public int AnswersCount { get; set; }
        public int TotalAnswersCount { get; set; }

        public decimal Percentage
        {
            get
            {
                if (this.TotalAnswersCount > 0)
                {
                    return (decimal)this.AnswersCount / (decimal)this.TotalAnswersCount * 100;
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