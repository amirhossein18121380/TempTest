using Application.Models.Common;
using Mediator;

namespace Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<OperationResult<List<GetUsersQueryResponse>>>;