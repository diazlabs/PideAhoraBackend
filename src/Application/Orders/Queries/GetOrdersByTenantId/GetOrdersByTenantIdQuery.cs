using Application.Common.Extensions;
using FluentValidation;
using MediatR;

namespace Application.Orders.Queries.GetOrdersByTenantId
{
    public class GetOrdersByTenantIdQuery : IRequest<List<GetOrdersByTenantIdResponse>>
    {
        public Guid TenantId { get; set; }
    }

    public class GetOrdersByTenantIdValidator : AbstractValidator<GetOrdersByTenantIdQuery>
    {
        public GetOrdersByTenantIdValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
