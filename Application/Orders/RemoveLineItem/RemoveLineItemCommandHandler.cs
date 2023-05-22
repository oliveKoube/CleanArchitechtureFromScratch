using Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.RemoveLineItem;

public sealed class RemoveLineItemCommandHandler : IRequestHandler<RemoveLineItemCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveLineItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(o => o.LineItems.Where(li => li.Id == request.lineItemId))
            .SingleOrDefaultAsync(o => o.Id == request.orderId, cancellationToken);
        if (order is null)
        {
            return;
        }
        order.RemoveLineItem(request.lineItemId);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}