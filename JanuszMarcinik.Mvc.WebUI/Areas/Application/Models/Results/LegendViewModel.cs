namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Results
{
    public class LegendViewModel
    {
        public string SignOfBadgeMark { get; set; }
        public string DescriptionOfBadgeMark { get; set; }

        public string SignOfResultMark { get; set; }
        public string DescriptionOfResultMark { get; set; }

        public static LegendViewModel General()
        {
            return new LegendViewModel()
            {
                SignOfBadgeMark = "P",
                DescriptionOfBadgeMark = "Średnia ilość punktów",
                SignOfResultMark = "W",
                DescriptionOfResultMark = "Wynik"
            };
        }

        public static LegendViewModel Details()
        {
            return new LegendViewModel()
            {
                SignOfBadgeMark = "N",
                DescriptionOfBadgeMark = "Ilość odpowiedzi",
                SignOfResultMark = "%",
                DescriptionOfResultMark = "Procentowa ilość zaznaczenia"
            };
        }
    }
}