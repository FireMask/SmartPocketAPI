using System.Collections;

namespace SmartPocketAPI.Extensions;

public static class BoolExtensions
{
    public static bool IsNullOrEmpty(this IEnumerable enumerable)
    {
        return enumerable == null || !enumerable.Cast<object>().Any();
    }

}