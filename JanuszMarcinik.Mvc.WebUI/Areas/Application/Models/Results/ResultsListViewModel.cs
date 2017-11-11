using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class ResultsListViewModel
    {
        public string Title { get; set; }
        public List<ResultsViewModel> Results { get; set; }
        public ResultsViewModel PearsonCorrelations { get; set; }
        public LegendViewModel Legend { get; set; }
    }
}