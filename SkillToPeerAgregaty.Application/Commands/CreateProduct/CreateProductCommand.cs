using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Commands.CreateProduct;
public sealed record CreateProductCommand(string Name, Quantity StartAmount) : ICommand<Result<ProductId>>;
