using JanuszMarcinik.Mvc.Domain.Application.Models;
using System.ComponentModel;
using System;

namespace JanuszMarcinik.Mvc.Domain.Application.Keys
{
    public enum KeyType
    {
        [Description("-- Brak --")]
        None = 0,
        LOT = 1,
        IZZ = 2,
        WHOQOL = 3
    }

    internal static class KeyTypeService
    {
        public static void SetPartialValue(this ResultGeneral resultGeneral, KeyType keyType)
        {
            switch (keyType)
            {
                case KeyType.IZZ:
                    {
                        // Punktacja: 1-5
                        if (resultGeneral.IntervieweeCount > 1)
                        {
                            resultGeneral.Value = Math.Round((decimal)resultGeneral.PointsEarned / (decimal)resultGeneral.IntervieweeCount / 6M, 2);
                        }
                        else
                        {
                            resultGeneral.Value = Math.Round((decimal)resultGeneral.PointsEarned / 6M, 2);
                        }

                        if (resultGeneral.Value < 2.5M)
                        {
                            resultGeneral.ResultValueMark = ResultValueMark.Low;
                        }
                        else if (resultGeneral.Value > 3.5M)
                        {
                            resultGeneral.ResultValueMark = ResultValueMark.High;
                        }
                        else
                        {
                            resultGeneral.ResultValueMark = ResultValueMark.Medium;
                        }
                    }
                    break;
                case KeyType.WHOQOL:
                    {
                        // Punktacja: 4-20
                        WHOQOL(resultGeneral);
                    }
                    break;
            }
        }

        public static void SetSummaryValue(this ResultGeneral resultGeneral, KeyType keyType, IZZGenderType genderType = IZZGenderType.Universal)
        {
            switch (keyType)
            {
                case KeyType.LOT:
                    {
                        resultGeneral.Value = LOTStenResult.GetResult(resultGeneral.AveragePointsEarned);
                        resultGeneral.ResultValueMark = GetBySten((int)resultGeneral.Value);
                    }
                    break;
                case KeyType.IZZ:
                    {
                        resultGeneral.Value = IZZStenResult.GetResult(genderType, resultGeneral.AveragePointsEarned);
                        resultGeneral.ResultValueMark = GetBySten((int)resultGeneral.Value);
                    }
                    break;
                case KeyType.WHOQOL:
                    {
                        WHOQOL(resultGeneral);
                    }
                    break;
            }
        }

        private static void WHOQOL(ResultGeneral resultGeneral)
        {
            resultGeneral.Value = (resultGeneral.AveragePointsEarned * 4) / resultGeneral.QuestionsInCategoryCount;
            if (resultGeneral.Value < 10)
            {
                resultGeneral.ResultValueMark = ResultValueMark.Low;
            }
            else if (resultGeneral.Value > 15)
            {
                resultGeneral.ResultValueMark = ResultValueMark.High;
            }
            else
            {
                resultGeneral.ResultValueMark = ResultValueMark.Medium;
            }
        }

        private static ResultValueMark GetBySten(int sten)
        {
            if (sten < 5)
            {
                return ResultValueMark.Low;
            }
            else if (sten > 7)
            {
                return ResultValueMark.High;
            }
            else
            {
                return ResultValueMark.Medium;
            }
        }
    }
}