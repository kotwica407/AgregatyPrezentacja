namespace SkillToPeerAgregaty.Domain.Shared;
public record struct ProductReservationRow(ProductId ProductId, Quantity Amount, Price Price) : IValueObject;
