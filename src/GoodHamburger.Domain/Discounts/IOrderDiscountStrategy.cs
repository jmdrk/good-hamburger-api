using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Discounts;

public interface IOrderDiscountStrategy
{
    bool IsMatch(Order order);
    decimal GetDiscountPercentage();
}