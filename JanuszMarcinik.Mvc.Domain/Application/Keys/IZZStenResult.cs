namespace JanuszMarcinik.Mvc.Domain.Application.Keys
{
    internal class IZZStenResult
    {
        internal static int GetResult(IZZGenderType genderType, int points)
        {
            if (genderType == IZZGenderType.Men)
            {
                return IZZStenForMen(points);
            }
            else if (genderType == IZZGenderType.Women)
            {
                return IZZStenForWomen(points);
            }
            else
            {
                return (IZZStenForMen(points) + IZZStenForWomen(points)) / 2;
            }
        }

        #region IZZStenForWomen()
        private static int IZZStenForWomen(int points)
        {
            if (points <= 53)
                return 1;
            else if (points <= 62)
                return 2;
            else if (points <= 70)
                return 3;
            else if (points <= 77)
                return 4;
            else if (points <= 84)
                return 5;
            else if (points <= 91)
                return 6;
            else if (points <= 98)
                return 7;
            else if (points <= 104)
                return 8;
            else if (points <= 111)
                return 9;
            else if (points <= 120)
                return 10;
            else
                return 0;
        }
        #endregion

        #region IZZStenForMen()
        private static int IZZStenForMen(int points)
        {
            if (points <= 50)
                return 1;
            else if (points <= 58)
                return 2;
            else if (points <= 65)
                return 3;
            else if (points <= 71)
                return 4;
            else if (points <= 78)
                return 5;
            else if (points <= 86)
                return 6;
            else if (points <= 93)
                return 7;
            else if (points <= 101)
                return 8;
            else if (points <= 108)
                return 9;
            else if (points <= 120)
                return 10;
            else
                return 0;
        }
        #endregion
    }

    public enum IZZGenderType
    {
        Universal,
        Women,
        Men
    }
}