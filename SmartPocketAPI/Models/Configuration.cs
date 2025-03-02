namespace SmartPocketAPI.Models;

public class Configuration
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public UserConfig ToDto()
    {
        return new UserConfig(this.Key, this.Value);
    }
}

public record UserConfig(string Key, string Value);