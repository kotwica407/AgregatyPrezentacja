using SkillToPeerAgregaty.Domain.Shared;

namespace SkillToPeerAgregaty.Application.Validation;
public class ValidationErrors
{
    public static class Product
    {
        public static Error NotFound => new("ValidationErrors.Product.NotFound", "Product not found.");
    }

    public static class Order
    {
        public static Error NotFound => new("ValidationErrors.Order.NotFound", "Order not found.");
    }
}
