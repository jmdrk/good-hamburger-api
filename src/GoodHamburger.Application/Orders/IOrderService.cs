using GoodHamburger.Application.Orders.Dtos;
using GoodHamburger.Application.Orders.Requests;

namespace GoodHamburger.Application.Orders;

public interface IOrderService
{
    Task<OrderDto> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<OrderDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<OrderDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<OrderDto> UpdateAsync(Guid id, UpdateOrderRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}