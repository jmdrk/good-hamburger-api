using GoodHamburger.Application.Orders.Dtos;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Orders.Mappers;

public static class OrderMapper
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto(
            order.Id,
            order.Items.Select(item => new OrderItemDto(
                item.Code.ToString(),
                item.Name,
                item.Type.ToString(),
                item.UnitPrice
            )).ToList(),
            order.Subtotal,
            order.DiscountPercentage,
            order.DiscountAmount,
            order.Total,
            order.CreatedAt,
            order.UpdatedAt
        );
    }
}