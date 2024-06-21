using SkillToPeerAgregaty.Domain.Shared;
using SkillToPeerAgregaty.Domain.Shared.Errors;

namespace SkillToPeerAgregaty.Domain.Product.Entities;
public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; }
    public Quantity AvailableAmount { get; private set; }

    private Product()//EF Core constructor
    {

    }

    public Product(string name, Quantity startAmount)
    {
        Id = new ProductId(Guid.NewGuid());
        Name = name;
        AvailableAmount = startAmount;
    }
    public static Result<Product> Create(string name, Quantity startAmount) => Result.Success<Product>(new(name, startAmount));

    public Result Reserve(Quantity Amount)
    {
        if (Amount > AvailableAmount)
            return Result.Failure(DomainErrors.Product.NotEnoughAmount);

        AvailableAmount = AvailableAmount.GetReduced(Amount);
        IncrementVersion();
        return Result.Success();
    }

    public Result Increase(Quantity Amount)
    {
        AvailableAmount = AvailableAmount.GetIncreased(Amount);
        IncrementVersion();
        return Result.Success();
    }
}
