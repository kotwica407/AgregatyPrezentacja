using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.ReserveProduct;
public sealed record ReserveProductCommand(ProductId ProductId, Quantity Amount) : ICommand<Result>;
