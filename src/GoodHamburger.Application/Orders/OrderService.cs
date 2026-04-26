using GoodHamburger.Application.Orders.Dtos;
using GoodHamburger.Application.Orders.Mappers;
using GoodHamburger.Application.Orders.Requests;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Menu;
using GoodHamburger.Domain.Repositories;

namespace GoodHamburger.Application.Orders;

public sealed class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var menuItems = GetMenuItems(request.Items);

        var order = new Order(menuItems);

        await _orderRepository.AddAsync(order, cancellationToken);

        return order.ToDto();
    }

    public async Task<IReadOnlyCollection<OrderDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);

        return orders.Select(order => order.ToDto()).ToList();
    }

    public async Task<OrderDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);

        if (order is null)
            throw new KeyNotFoundException("Order not found.");

        return order.ToDto();
    }

    public async Task<OrderDto> UpdateAsync(Guid id, UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);

        if (order is null)
            throw new KeyNotFoundException("Order not found.");

        var menuItems = GetMenuItems(request.Items);

        order.UpdateItems(menuItems);

        await _orderRepository.UpdateAsync(order, cancellationToken);

        return order.ToDto();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);

        if (order is null)
            throw new KeyNotFoundException("Order not found.");

        await _orderRepository.DeleteAsync(order, cancellationToken);
    }

    private static IReadOnlyCollection<MenuItem> GetMenuItems(IReadOnlyCollection<Domain.Enums.MenuItemCode> itemCodes)
    {
        if (itemCodes is null || itemCodes.Count == 0)
            throw new InvalidOperationException("Order must have at least one item.");

        var menuItems = itemCodes
            .Select(code => MenuCatalog.GetByCode(code))
            .ToList();

        if (menuItems.Any(item => item is null))
            throw new InvalidOperationException("One or more menu items are invalid.");

        return menuItems!;
    }
}