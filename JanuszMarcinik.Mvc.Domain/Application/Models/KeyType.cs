namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public enum KeyType
    {
        LOTR = 1,
        IZZ = 2,
        WHOQOL = 3,
        SOC29 = 4
    }

    public static class KeyTypeExtensions
    {
        public static string GetRange(this KeyType keyType, bool isCategorized, int? questionsInCategoryCount = null)
        {
            switch (keyType)
            {
                case KeyType.LOTR:
                    return "1 - 10 (STEN)";
                case KeyType.IZZ:
                    {
                        if (isCategorized)
                            return "1.00 - 5.00";
                        else
                            return "1 - 10 (STEN)";
                    }
                case KeyType.WHOQOL:
                    return "4 - 20";
                case KeyType.SOC29:
                    {
                        if (isCategorized && questionsInCategoryCount.HasValue)
                        {
                            return $"{questionsInCategoryCount.Value} - {questionsInCategoryCount.Value * 7}";
                        }
                        else
                        {
                            return "29 - 203";
                        }
                    }
                default:
                    return string.Empty;
            }
        }

        public static int GetMinRange(this KeyType keyType)
        {
            switch (keyType)
            {
                case KeyType.LOTR:
                    return 1;
                case KeyType.IZZ:
                    return 1;
                case KeyType.WHOQOL:
                    return 4;
                case KeyType.SOC29:
                    return 29;
                default:
                    return 0;
            }
        }

        public static int GetMaxRange(this KeyType keyType)
        {
            switch (keyType)
            {
                case KeyType.LOTR:
                    return 10;
                case KeyType.IZZ:
                    return 10;
                case KeyType.WHOQOL:
                    return 20;
                case KeyType.SOC29:
                    return 203;
                default:
                    return 0;
            }
        }
    }
}