namespace SkillToPeerAgregaty.Domain.Shared.Errors;
public static class DomainErrors
{
    public static class Order
    {
        public static Error IsNotAwaitingApproval => new("Order.IsNotAwaitingApproval", "Order is not in awaiting approval status.");
        public static Error IsDelivered => new("Order.IsDelivered", "Order is in delivered status.");
        public static Error IsCancelled => new("Order.IsCancelled", "Order is in cancelled status.");
        public static Error IsNotApproved => new("Order.IsNotApproved", "Order is not in approved status.");
        public static Error SameProducts => new("Order.SameProducts", "Each orderItem in order must have different product.");
    }

    public static class Product
    {
        public static Error NotEnoughAmount => new("Product.NotEnoughAmount", "Not enough pruduct amount to reserve.");
    }
}
