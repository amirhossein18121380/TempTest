using Application.Models.Common;
using Mediator;

namespace Application.Features.Role.Queries.GetAllRolesQuery;

public record GetAllRolesQuery():IRequest<OperationResult<List<GetAllRolesQueryResponse>>>;