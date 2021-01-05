namespace IData.Constants
{
    public enum PaymentIntervalType
    {
        OneTimePayment = 1,
        Monthly = 2,
        EverySecondMonth = 3,
        EverySixthMonth = 4,
        Yearly = 5,
        Custom = 6,
    }

    public static class PaymentIntervalExtension
    {
        public static string ConvertToText(this PaymentIntervalType interval)
        {
            switch (interval)
            {
                case PaymentIntervalType.OneTimePayment:
                    return "One time payment";

                case PaymentIntervalType.Monthly:
                    return "Monthly";

                case PaymentIntervalType.EverySecondMonth:
                    return "Every 2nd month";

                case PaymentIntervalType.EverySixthMonth:
                    return "Every 6th month";

                case PaymentIntervalType.Yearly:
                    return "Once a year";

                default:
                    return "Custom";
            }
        }
    }
}