using Application.Models.Common;
using Mediator;

namespace Application.Features.Role.Queries.GetAuthorizableRoutesQuery;

public record GetAuthorizableRoutesQuery():IRequest<OperationResult<List<GetAuthorizableRoutesQueryResponse>>>;