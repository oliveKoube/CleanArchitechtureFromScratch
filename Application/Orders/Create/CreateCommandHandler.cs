using Application.Persistence;
using Domain.Customers;
using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

public sealed class CreateCommandHandler : IRequestHandler<CreateOrdreCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public CreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _publisher = _publisher;
    }

    public async Task Handle(CreateOrdreCommand request, CancellationToken cancellationToken)
    {
        var customers = await _context.Customers.FindAsync(
            new CustomerId(request.customerId));
        if (customers is null)
        {
            return;
        }
        var order = Order.Create(customers.Id);
        
        _context.Orders.Add(order);
        
        await _context.SaveChangesAsync(cancellationToken);
        await _publisher.Publish(new OrderCreatedEvent(order.Id), cancellationToken);
    }
}