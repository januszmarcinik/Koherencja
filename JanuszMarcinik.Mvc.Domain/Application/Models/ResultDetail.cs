namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class ResultDetail
    {
        public int BaseDictionaryId { get; set; }
        public string BaseDictionaryValue { get; set; }
        public string DictionaryTypeName { get; set; }

        public int IntervieweeCount { get; set; }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public int AnswerId { get; set; }

        public int AnswersCount { get; set; }
        public int TotalAnswersCount { get; set; }
    }
}