using System.Collections.Generic;

namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class IntervieweeDetail
    {
        public int IntervieweeCount { get; set; }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public List<IntervieweeDetailAnswer> Answers { get; set; }
    }

    public class IntervieweeDetailAnswer
    {
        public string Text { get; private set; }
        public int Count { get; private set; }
        public decimal Percentage { get; private set; }
        public string ResultCssClass { get; private set; }

        public IntervieweeDetailAnswer(string text, int count, int totalCount)
        {
            Text = text;
            Count = count;

            if (totalCount == 0)
                Percentage = 0;
            else
                Percentage = ((decimal)count / (decimal)totalCount) * 100;

            if (Percentage > 30)
            {
                ResultCssClass = "label label-success";
            }
            else if (Percentage < 10)
            {
                ResultCssClass = "label label-danger";
            }
            else
            {
                ResultCssClass = "label label-info";
            }
        }
    }
}