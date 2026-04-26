using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Discounts;

public sealed class SandwichFriesDiscountStrategy : IOrderDiscountStrategy
{
    public bool IsMatch(Order order)
    {
        return order.HasSandwich && order.HasFries && !order.HasSoftDrink;
    }

    public decimal GetDiscountPercentage()
    {
        return 0.10m;
    }
}
