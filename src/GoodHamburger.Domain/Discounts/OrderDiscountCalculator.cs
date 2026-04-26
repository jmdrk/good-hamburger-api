using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Discounts;

public sealed class OrderDiscountCalculator
{
    private readonly IReadOnlyCollection<IOrderDiscountStrategy> _strategies;

    public OrderDiscountCalculator()
    {
        _strategies =
        [
            new SandwichFriesSoftDrinkDiscountStrategy(),
            new SandwichSoftDrinkDiscountStrategy(),
            new SandwichFriesDiscountStrategy()
        ];
    }

    public decimal GetDiscountPercentage(Order order)
    {
        var strategy = _strategies.FirstOrDefault(strategy => strategy.IsMatch(order));

        return strategy?.GetDiscountPercentage() ?? 0m;
    }
}