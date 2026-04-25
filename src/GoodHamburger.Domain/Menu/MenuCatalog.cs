using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Menu;

public static class MenuCatalog
{
    private static readonly IReadOnlyCollection<MenuItem> Items =
    [
        new(MenuItemCode.XBurger, "X Burger", MenuItemType.Sandwich, 5.00m),
        new(MenuItemCode.XEgg, "X Egg", MenuItemType.Sandwich, 4.50m),
        new(MenuItemCode.XBacon, "X Bacon", MenuItemType.Sandwich, 7.00m),
        new(MenuItemCode.Fries, "Batata frita", MenuItemType.Fries, 2.00m),
        new(MenuItemCode.SoftDrink, "Refrigerante", MenuItemType.SoftDrink, 2.50m)
    ];

    public static IReadOnlyCollection<MenuItem> GetAll()
    {
        return Items;
    }

    public static MenuItem? GetByCode(MenuItemCode code)
    {
        return Items.FirstOrDefault(item => item.Code == code);
    }
}