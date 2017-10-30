namespace JanuszMarcinik.Mvc.Domain.Application.Keys
{
    internal class LOTStenResult
    {
        internal static int GetResult(int points)
        {
            if (points <= 6)
                return 1;
            else if (points <= 8)
                return 2;
            else if (points <= 10)
                return 3;
            else if (points <= 12)
                return 4;
            else if (points <= 14)
                return 5;
            else if (points <= 16)
                return 6;
            else if (points <= 18)
                return 7;
            else if (points <= 20)
                return 8;
            else if (points <= 22)
                return 9;
            else if (points <= 24)
                return 10;
            else
                return 0;
        }
    }
}