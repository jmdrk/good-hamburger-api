namespace GoodHamburger.Application.Orders.Dtos;

public sealed record OrderDto(
    Guid Id,
    IReadOnlyCollection<OrderItemDto> Items,
    decimal Subtotal,
    decimal DiscountPercentage,
    decimal DiscountAmount,
    decimal Total,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);