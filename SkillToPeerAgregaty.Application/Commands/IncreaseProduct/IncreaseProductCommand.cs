using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.IncreaseProduct;
public sealed record IncreaseProductCommand(ProductId ProductId, Quantity Amount) : ICommand<Result>;
