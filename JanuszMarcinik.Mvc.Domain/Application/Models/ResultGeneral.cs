namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class ResultGeneral
    {
        public int BaseDictionaryId { get; set; }
        public string BaseDictionaryValue { get; set; }
        public string DictionaryTypeName { get; set; }

        public int IntervieweeCount { get; set; }

        public KeyType KeyType { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string FullCategoryName
        {
            get { return $"{CategoryName}<br/>{PointsRange}"; }
        }

        public string PointsRange { get; set; }
        public int AveragePointsEarned { get; set; }
        public int PointsAvailableToGet { get; set; }
        public decimal AverageScoreValue { get; set; }
    }
}