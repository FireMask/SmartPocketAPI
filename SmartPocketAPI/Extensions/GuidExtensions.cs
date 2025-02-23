namespace SmartPocketAPI.Extensions;

public static class GuidExtensions
{
    public static Guid GetUserId(this HttpContext context)
    {
        if (!context.Items.ContainsKey("UserId"))
            throw new ArgumentException();

        string? userid = context.Items["UserId"] as string;

        if (userid == null)
            throw new ArgumentException();

        return new Guid(userid);
    }
}