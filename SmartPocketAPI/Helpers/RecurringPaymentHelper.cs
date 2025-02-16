namespace SmartPocketAPI.Helpers;

public enum Periodicity
{
    DAILY = 1,
    WEEKLY,
    MONTHLY,
    BIMONTHLY,
    QUARTER,
    YEARLY
}

public class RecurringPaymentHelper
{
    public static DateOnly GetNextDate(DateOnly lastDate, int frequencyId)
    {
        return frequencyId switch
        {
            (int)Periodicity.DAILY => lastDate.AddDays(1),
            (int)Periodicity.WEEKLY => lastDate.AddDays(7),
            (int)Periodicity.MONTHLY => lastDate.AddMonths(1),
            (int)Periodicity.BIMONTHLY => lastDate.AddMonths(2),
            (int)Periodicity.QUARTER => lastDate.AddMonths(3),
            (int)Periodicity.YEARLY => lastDate.AddYears(1),
            _ => lastDate
        };
    }
}