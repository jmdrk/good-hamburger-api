using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.Orders.Requests;

public sealed record UpdateOrderRequest(
    IReadOnlyCollection<MenuItemCode> Items
);