using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.Orders.Requests;

public sealed record CreateOrderRequest(
    IReadOnlyCollection<MenuItemCode> Items
);