using GoodHamburger.Domain.Discounts;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public sealed class Order
{
    private readonly List<OrderItem> _items = [];

    public Guid Id { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items;

    public decimal Subtotal { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal Total { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public bool HasSandwich => _items.Any(item => item.Type == MenuItemType.Sandwich);
    public bool HasFries => _items.Any(item => item.Type == MenuItemType.Fries);
    public bool HasSoftDrink => _items.Any(item => item.Type == MenuItemType.SoftDrink);

    private Order()
    {
    }

    public Order(IEnumerable<MenuItem> menuItems)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;

        SetItems(menuItems);
    }

    public void UpdateItems(IEnumerable<MenuItem> menuItems)
    {
        SetItems(menuItems);
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetItems(IEnumerable<MenuItem> menuItems)
    {
        var items = menuItems.ToList();

        if (items.Count == 0)
            throw new InvalidOperationException("Order must have at least one item.");

        ValidateDuplicatedItems(items);

        _items.Clear();
        _items.AddRange(items.Select(item => new OrderItem(item)));

        CalculateTotals();
    }

    private static void ValidateDuplicatedItems(IReadOnlyCollection<MenuItem> items)
    {
        var duplicatedTypes = items
            .GroupBy(item => item.Type)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();

        if (duplicatedTypes.Count == 0)
            return;

        var duplicatedItems = string.Join(", ", duplicatedTypes);

        throw new InvalidOperationException(
            $"Order cannot contain duplicated item types. Duplicated types: {duplicatedItems}.");
    }

    private void CalculateTotals()
    {
        Subtotal = _items.Sum(item => item.UnitPrice);

        var discountCalculator = new OrderDiscountCalculator();

        DiscountPercentage = discountCalculator.GetDiscountPercentage(this);

        DiscountAmount = decimal.Round(
            Subtotal * DiscountPercentage,
            2,
            MidpointRounding.AwayFromZero);

        Total = Subtotal - DiscountAmount;
    }
}