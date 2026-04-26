namespace GoodHamburger.Application.Orders.Dtos;

public sealed record OrderItemDto(
    string Code,
    string Name,
    string Type,
    decimal UnitPrice
);