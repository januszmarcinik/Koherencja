using System;
using System.Collections.Generic;
using System.Linq;

namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public class PearsonCorrelation
    {
        public PearsonCorrelation(string title, string xAxisName, string yAxisName, List<double> xAxisSeries, List<double> yAxisSeries,
            int xAxisMin, int xAxisMax, int yAxisMin, int yAxisMax)
        {
            Title = title;
            XAxisName = xAxisName;
            YAxisName = yAxisName;
            XAxisMin = xAxisMin;
            XAxisMax = xAxisMax;
            YAxisMin = yAxisMin;
            YAxisMax = yAxisMax;

            if (xAxisSeries.Count > 0 && yAxisSeries.Count > 0 && xAxisSeries.Count == yAxisSeries.Count)
            {
                var valuesCoordinates = new List<List<int>>();
                double count = xAxisSeries.Count;
                double seriesXSum = xAxisSeries.Sum();
                double seriesYSum = yAxisSeries.Sum();
                double xPowSum = xAxisSeries.Sum(x => Math.Pow(x, 2));
                double yPowSum = yAxisSeries.Sum(x => Math.Pow(x, 2));

                double xySum = 0;
                for (int index = 0; index < count; index++)
                {
                    valuesCoordinates.Add(new List<int>() { (int)xAxisSeries[index], (int)yAxisSeries[index] });
                    xySum += xAxisSeries[index] * yAxisSeries[index];
                }

                ValuesCoordinates = valuesCoordinates.Select(x => x.ToArray()).ToArray();

                Value =
                    (((count * xySum) - (seriesXSum * seriesYSum)) /
                    (Math.Sqrt(((count * xPowSum) - (Math.Pow(seriesXSum, 2))) * ((count * yPowSum) - (Math.Pow(seriesYSum, 2))))));

                double b =
                   (((count * xySum) - (seriesXSum * seriesYSum)) /
                   ((count * xPowSum) - (Math.Pow(seriesXSum, 2))));

                double a = (seriesYSum / count) - (b * (seriesXSum / count));

                var regressionLineCoordinates = new List<List<int>>();
                regressionLineCoordinates.Add(new List<int>() { 0, (int)(a) });
                regressionLineCoordinates.Add(new List<int>() { xAxisMax, (int)(a + (b * xAxisMax)) });
                RegressionLineCoordinates = regressionLineCoordinates.Select(x => x.ToArray()).ToArray();
            }
        }

        public string Title { get; }
        public string XAxisName { get; }
        public string YAxisName { get; }

        public double Value { get; }
        public int[][] ValuesCoordinates { get; }
        public int[][] RegressionLineCoordinates { get; }

        public int XAxisMin { get; }
        public int XAxisMax { get; }
        public int YAxisMin { get; }
        public int YAxisMax { get; }
    }
}
