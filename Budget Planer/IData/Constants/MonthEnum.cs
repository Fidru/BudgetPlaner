namespace IData.Constants
{
    public enum MonthEnum
    {
        Jan = 1,
        Feb = 2,
        Mar = 3,
        Apr = 4,
        Mai = 5,
        Jun = 6,
        Jul = 7,
        Aug = 8,
        Sep = 9,
        Oct = 10,
        Nov = 11,
        Dez = 12
    }

    public static class MonthEnumExtension
    {
        public static string ConvertToText(this MonthEnum month)
        {
            switch (month)
            {
                case (MonthEnum.Jan):
                    return "Jan";

                case (MonthEnum.Feb):
                    return "Feb";

                case (MonthEnum.Mar):
                    return "Mar";

                case (MonthEnum.Apr):
                    return "Apr";

                case (MonthEnum.Mai):
                    return "Mai";

                case (MonthEnum.Jun):
                    return "Jun";

                case (MonthEnum.Jul):
                    return "Jul";

                case (MonthEnum.Aug):
                    return "Aug";

                case (MonthEnum.Sep):
                    return "Sep";

                case (MonthEnum.Oct):
                    return "Oct";

                case (MonthEnum.Nov):
                    return "Nov";

                default:
                    return "Dez";
            }
        }
    }
}