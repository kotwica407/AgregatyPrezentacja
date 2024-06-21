namespace SkillToPeerAgregaty.Domain.Shared;
public record struct ProductId(Guid Value) : IValueObject;

public record struct OrderId(Guid Value) : IValueObject;
public record struct OrderItemId(Guid Value) : IValueObject;

public record struct UserId(Guid Value) : IValueObject;