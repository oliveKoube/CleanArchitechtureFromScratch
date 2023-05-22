using MediatR;

namespace Application.Orders.GetOrder;

public record GetOrderQuery(Guid orderId) : IRequest<OrderResponse>;