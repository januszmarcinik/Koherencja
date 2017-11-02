using System;
using System.Collections.Generic;
using System.Linq;

namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class IntervieweeQuestionnaireResult
    {
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }

        public List<IntervieweeResult> IntervieweeResults { get; set; }
    }

    public class IntervieweeResult
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ResultRange { get; set; }

        /// <summary>
        /// N - Ilość wyników
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Śr - Średnia
        /// </summary>
        public decimal Average { get; private set; }

        /// <summary>
        /// Mod - Modalna (najczęśćiej powtarzająca się)
        /// </summary>
        public decimal Mode { get; private set; }

        /// <summary>
        /// Me - Mediana (środkowa)
        /// </summary>
        public decimal Median { get; private set; }

        /// <summary>
        /// Min - Minimalna
        /// </summary>
        public decimal Minimum { get; private set; }

        /// <summary>
        /// Max - Maksymalna
        /// </summary>
        public decimal Maximum { get; private set; }

        /// <summary>
        /// S - Odchylenie standardowe
        /// </summary>
        public decimal Deviation { get; private set; }

        public void SetScores(IEnumerable<decimal> scores)
        {
            Count = scores.Count();

            if (Count == 1)
            {
                var result = scores.First();
                Average = result;
                Mode = result;
                Median = result;
                Minimum = result;
                Maximum = result;
                Deviation = 0;
            }
            else if (Count > 1)
            {
                scores = scores.OrderBy(x => x);

                Average = scores.Average();
                Minimum = scores.Min();
                Maximum = scores.Max();

                int halfIndex = Count / 2;
                if ((Count % 2) == 0)
                {
                    Median = (scores.ElementAt(halfIndex - 1) + scores.ElementAt(halfIndex)) / 2M;
                }
                else
                {
                    Median = scores.ElementAt(halfIndex);
                }

                Mode = scores.GroupBy(n => n)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key).FirstOrDefault();

                double sumOfSquaresOfDifferences = scores.Select(x => Math.Pow((double)(x - Average), 2)).Sum();
                Deviation = (decimal)Math.Round(Math.Sqrt(sumOfSquaresOfDifferences / (double)Count), 2);

                Average = Math.Round(Average, 2);
            }
        }
    }
}