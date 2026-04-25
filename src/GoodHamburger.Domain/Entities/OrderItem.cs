using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities;

public sealed class OrderItem
{
    public Guid Id { get; private set; }
    public MenuItemCode Code { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public MenuItemType Type { get; private set; }
    public decimal UnitPrice { get; private set; }

    private OrderItem()
    {
    }

    public OrderItem(MenuItem menuItem)
    {
        Id = Guid.NewGuid();
        Code = menuItem.Code;
        Name = menuItem.Name;
        Type = menuItem.Type;
        UnitPrice = menuItem.Price;
    }
}