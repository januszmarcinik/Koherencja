using System;
using System.Collections.Generic;
using System.Linq;

namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class PearsonCorrelation
    {
        public PearsonCorrelation(string questionnaireA, string questionnaireB, IEnumerable<decimal> seriesA, IEnumerable<decimal> seriesB)
        {
            QuestionnaireA = questionnaireA;
            QuestionnaireB = questionnaireB;
            SeriesA = seriesA.Select(x => (double)x).ToList();
            SeriesB = seriesB.Select(x => (double)x).ToList();

            if (SeriesA.Count > 0 && SeriesB.Count > 0 && SeriesA.Count == SeriesB.Count)
            {
                double count = SeriesA.Count;
                double seriesASum = SeriesA.Sum();
                double seriesBSum = SeriesB.Sum();
                double aPowSum = SeriesA.Sum(x => Math.Pow(x, 2));
                double bPowSum = SeriesB.Sum(x => Math.Pow(x, 2));

                double abSum = 0;
                for (int index = 0; index < count; index++)
                {
                    abSum += SeriesA[index] * SeriesB[index];
                }

                Value = 
                    (((count * abSum) - (seriesASum * seriesBSum)) /
                    (Math.Sqrt(((count * aPowSum) - (Math.Pow(seriesASum, 2))) * ((count * bPowSum) - (Math.Pow(seriesBSum, 2))))));
            }
        }

        public string QuestionnaireA { get; private set; }
        public string QuestionnaireB { get; private set; }

        public List<double> SeriesA { get; private set; }
        public List<double> SeriesB { get; private set; }

        public double Value { get; private set; }
    }
}
