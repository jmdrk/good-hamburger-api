using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Discounts;

public sealed class SandwichSoftDrinkDiscountStrategy : IOrderDiscountStrategy
{
    public bool IsMatch(Order order)
    {
        return order.HasSandwich && order.HasSoftDrink && !order.HasFries;
    }

    public decimal GetDiscountPercentage()
    {
        return 0.15m;
    }
}