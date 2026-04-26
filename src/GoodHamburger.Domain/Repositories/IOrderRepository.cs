using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Order order, CancellationToken cancellationToken);
    Task DeleteAsync(Order order, CancellationToken cancellationToken);
}