namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class ResultGeneral
    {
        public int BaseDictionaryId { get; set; }
        public string BaseDictionaryValue { get; set; }
        public string DictionaryTypeName { get; set; }

        public int IntervieweeCount { get; set; }

        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int PointsEarned { get; set; }
        public int AveragePointsEarned
        {
            get
            {
                if (this.IntervieweeCount > 1)
                {
                    return this.PointsEarned / this.IntervieweeCount;
                }
                else
                {
                    return this.PointsEarned;
                }
            }
        }

        public int PointsAvailableToGet { get; set; }
        public int TotalPointsAvailableToGet
        {
            get { return this.PointsAvailableToGet * this.IntervieweeCount; }
        }

        public int QuestionsInCategoryCount { get; set; }
        public ResultValueMark ResultValueMark { get; set; }
        public decimal Value { get; set; }
    }
}