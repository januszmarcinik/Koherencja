namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public enum ScoreValueMark
    {
        Low,
        Medium,
        High
    }

    public static class ScoreValueMarkExtensions
    {
        public static ScoreValueMark GetScoreValueMark(this KeyType keyType, bool isCategorized, decimal value)
        {
            switch (keyType)
            {
                case KeyType.LOTR:
                    return GetBySten((int)value);
                case KeyType.IZZ:
                    {
                        if (isCategorized)
                        {
                            if (value < 2.5M)
                            {
                                return ScoreValueMark.Low;
                            }
                            else if (value > 3.5M)
                            {
                                return ScoreValueMark.High;
                            }
                            else
                            {
                                return ScoreValueMark.Medium;
                            }
                        }
                        else
                        {
                            return GetBySten((int)value);
                        }
                    }
                case KeyType.WHOQOL:
                    {
                        if (value < 10)
                        {
                            return ScoreValueMark.Low;
                        }
                        else if (value > 15)
                        {
                            return ScoreValueMark.High;
                        }
                        else
                        {
                            return ScoreValueMark.Medium;
                        }
                    }
                default:
                    return ScoreValueMark.Medium;
            }
        }

        private static ScoreValueMark GetBySten(int sten)
        {
            if (sten < 5)
            {
                return ScoreValueMark.Low;
            }
            else if (sten > 7)
            {
                return ScoreValueMark.High;
            }
            else
            {
                return ScoreValueMark.Medium;
            }
        }
    }
}