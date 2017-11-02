namespace JanuszMarcinik.Mvc.Domain.Application.Models
{
    public enum KeyType
    {
        LOTR = 1,
        IZZ = 2,
        WHOQOL = 3
    }

    public static class KeyTypeExtensions
    {
        public static string GetRange(this KeyType keyType, bool isCategorized)
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
                default:
                    return string.Empty;
            }
        }
    }
}