using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public sealed class MenuItem
{
    public MenuItemCode Code { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public MenuItemType Type { get; private set; }
    public decimal Price { get; private set; }

    private MenuItem()
    {
    }

    public MenuItem(MenuItemCode code, string name, MenuItemType type, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Menu item name is required.", nameof(name));

        if (price <= 0)
            throw new ArgumentException("Menu item price must be greater than zero.", nameof(price));

        Code = code;
        Name = name;
        Type = type;
        Price = price;
    }
}