using Domain.Customers;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Persistence;

public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; set; }
    DbSet<Customer> Customers { get; set; }
    
    DatabaseFacade Database { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}