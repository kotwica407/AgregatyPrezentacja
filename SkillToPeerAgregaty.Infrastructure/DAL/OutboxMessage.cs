using Newtonsoft.Json;
using SkillToPeerAgregaty.Infrastructure.Services;

namespace SkillToPeerAgregaty.Infrastructure.DAL;
public class OutboxMessage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedUtcDate { get; private set; }
    public DateTime? ProcessedUtcDate { get; private set; }
    public byte TriesLeft { get; private set; }
    public string? Error { get; private set; }

    private OutboxMessage()
    { }

    public static OutboxMessage Create(object message, IClock clock)
    {
        return new OutboxMessage
        {
            Content = JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }),
            Type = message.GetType().Name,
            CreatedUtcDate = clock.UtcNowDateTime,
            TriesLeft = 1
        };
    }

    public void MarkAsProcessed(IClock clock)
    {
        ProcessedUtcDate = clock.UtcNowDateTime;
        TriesLeft--;
    }

    public void MarkAsFailed()
    {
        TriesLeft--;
    }
}
