using Domain.Orders;
using MediatR;

namespace Application.Orders.RemoveLineItem;

public record RemoveLineItemCommand(OrderId orderId, LineItemId lineItemId) : IRequest;