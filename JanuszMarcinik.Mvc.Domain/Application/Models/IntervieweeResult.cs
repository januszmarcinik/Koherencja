using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class IntervieweeResultsByFilters
    {
        [Display(Name = "Wiek")]
        public string Age { get; set; }

        [Display(Name = "Płeć")]
        public string Sex { get; set; }

        [Display(Name = "Staż pracy")]
        public string Seniority { get; set; }

        [Display(Name = "Miejsce zamieszkania")]
        public string PlaceOfResidence { get; set; }

        [Display(Name = "Wykształcenie")]
        public string Education { get; set; }

        [Display(Name = "Stan cywilny")]
        public string MartialStatus { get; set; }

        [Display(Name = "Ocena swojego stanu materialnego")]
        public string MaterialStatus { get; set; }

        public List<IntervieweeQuestionnaireResult> IntervieweeQuestionnaireResults { get; set; }
    }

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

        public void SetResults(IEnumerable<decimal> results)
        {
            Count = results.Count();

            if (Count == 1)
            {
                var result = results.First();
                Average = result;
                Mode = result;
                Median = result;
                Minimum = result;
                Maximum = result;
                Deviation = 0;
            }
            else if (Count > 1)
            {
                results = results.OrderBy(x => x);

                Average = results.Average();
                Minimum = results.Min();
                Maximum = results.Max();

                int halfIndex = Count / 2;
                if ((Count % 2) == 0)
                {
                    Median = (results.ElementAt(halfIndex - 1) + results.ElementAt(halfIndex)) / 2M;
                }
                else
                {
                    Median = results.ElementAt(halfIndex);
                }

                Mode = results.GroupBy(n => n)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key).FirstOrDefault();

                double sumOfSquaresOfDifferences = results.Select(x => Math.Pow((double)(x - Average), 2)).Sum();
                Deviation = (decimal)Math.Round(Math.Sqrt(sumOfSquaresOfDifferences / (double)Count), 2);

                Average = Math.Round(Average, 2);
            }
        }
    }
}