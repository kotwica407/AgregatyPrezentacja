namespace SkillToPeerAgregaty.Infrastructure.Services;
public interface IClock
{
    DateTimeOffset UtcNow { get; }

    /// <summary>
    /// Returns current time with specified utc timezone
    /// </summary>
    DateTime UtcNowDateTime { get; }
}

internal class Clock : IClock
{
    public DateTimeOffset UtcNow => DateTime.UtcNow;

    public DateTime UtcNowDateTime => DateTime.UtcNow;
}
