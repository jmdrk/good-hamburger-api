using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Discounts;

public sealed class SandwichFriesSoftDrinkDiscountStrategy : IOrderDiscountStrategy
{
    public bool IsMatch(Order order)
    {
        return order.HasSandwich && order.HasFries && order.HasSoftDrink;
    }

    public decimal GetDiscountPercentage()
    {
        return 0.20m;
    }
}