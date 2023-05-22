using Application.Persistence;
using Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.GetOrder;

public sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResponse>
{
    private readonly IApplicationDbContext _context;

    public GetOrderQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var orderResponse = await _context
            .Orders
            .Where(o => o.Id == new OrderId(request.orderId))
            .Select(order => new OrderResponse(order.Id.Value,
                order.CustomerId.Value,
                order.LineItems
                    .Select(li => new LineItemResponse(li.Id.Value,li.Price.Amount))
                    .ToList()))
            .SingleAsync(cancellationToken);

        /*var orderSummaries = await _context.Database.SqlQuery<OrderSummary>(
                @$"SELECT o.Id as OrderId, o.CustomerId, li.Id as LineItemId, li.Price_Amount as LineItemPrice" +
                "FROM Orders AS o"
                + "INNER JOIN LineItems AS li ON o.Id = li.OrderId " +
                "WHERE o.Id = {request.orderId}")
            .ToListAsync(cancellationToken);
        
        var orderResponse = orderSummaries
            .GroupBy(o => o.OrderId)
            .Select(o => new OrderResponse(
                o.Key,
                o.First().CustomerId.Value, 
                o.Select(li => new LineItemResponse(li.LineItemId.Value, li.LineItemPrice))
                    .ToList()))
            .Single();*/
        
        return orderResponse;
    }
    
    private sealed record OrderSummary(
        Guid Id,
        Guid CustomerId,
        Guid LineItems,
        decimal LineItemPrice);
}