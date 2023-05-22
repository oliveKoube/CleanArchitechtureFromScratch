using MediatR;

namespace Application.Orders.Create;

public record CreateOrdreCommand(Guid customerId) : IRequest;