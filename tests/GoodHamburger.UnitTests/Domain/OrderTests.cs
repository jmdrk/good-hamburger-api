using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.UnitTests.Domain;

public sealed class OrderTests
{
    [Fact]
    public void Should_apply_20_percent_discount_when_order_has_sandwich_fries_and_soft_drink()
    {
        var items = new[]
        {
            new MenuItem(MenuItemCode.XBurger, "X Burger", MenuItemType.Sandwich, 5.00m),
            new MenuItem(MenuItemCode.Fries, "Batata frita", MenuItemType.Fries, 2.00m),
            new MenuItem(MenuItemCode.SoftDrink, "Refrigerante", MenuItemType.SoftDrink, 2.50m)
        };

        var order = new Order(items);

        order.Subtotal.Should().Be(9.50m);
        order.DiscountPercentage.Should().Be(0.20m);
        order.DiscountAmount.Should().Be(1.90m);
        order.Total.Should().Be(7.60m);
    }

    [Fact]
    public void Should_apply_15_percent_discount_when_order_has_sandwich_and_soft_drink()
    {
        var items = new[]
        {
            new MenuItem(MenuItemCode.XBurger, "X Burger", MenuItemType.Sandwich, 5.00m),
            new MenuItem(MenuItemCode.SoftDrink, "Refrigerante", MenuItemType.SoftDrink, 2.50m)
        };

        var order = new Order(items);

        order.Subtotal.Should().Be(7.50m);
        order.DiscountPercentage.Should().Be(0.15m);
        order.DiscountAmount.Should().Be(1.13m);
        order.Total.Should().Be(6.37m);
    }

    [Fact]
    public void Should_apply_10_percent_discount_when_order_has_sandwich_and_fries()
    {
        var items = new[]
        {
            new MenuItem(MenuItemCode.XBurger, "X Burger", MenuItemType.Sandwich, 5.00m),
            new MenuItem(MenuItemCode.Fries, "Batata frita", MenuItemType.Fries, 2.00m)
        };

        var order = new Order(items);

        order.Subtotal.Should().Be(7.00m);
        order.DiscountPercentage.Should().Be(0.10m);
        order.DiscountAmount.Should().Be(0.70m);
        order.Total.Should().Be(6.30m);
    }

    [Fact]
    public void Should_not_apply_discount_when_order_has_only_sandwich()
    {
        var items = new[]
        {
            new MenuItem(MenuItemCode.XBurger, "X Burger", MenuItemType.Sandwich, 5.00m)
        };

        var order = new Order(items);

        order.Subtotal.Should().Be(5.00m);
        order.DiscountPercentage.Should().Be(0m);
        order.DiscountAmount.Should().Be(0m);
        order.Total.Should().Be(5.00m);
    }

    [Fact]
    public void Should_not_allow_duplicated_sandwich()
    {
        var items = new[]
        {
            new MenuItem(MenuItemCode.XBurger, "X Burger", MenuItemType.Sandwich, 5.00m),
            new MenuItem(MenuItemCode.XEgg, "X Egg", MenuItemType.Sandwich, 4.50m)
        };

        var action = () => new Order(items);

        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("*Duplicated types: Sandwich*");
    }

    [Fact]
    public void Should_not_allow_empty_order()
    {
        var action = () => new Order([]);

        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Order must have at least one item.");
    }
}