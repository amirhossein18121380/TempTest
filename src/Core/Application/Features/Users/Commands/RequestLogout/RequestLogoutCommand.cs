using Application.Models.Common;
using Mediator;

namespace Application.Features.Users.Commands.RequestLogout;

public record RequestLogoutCommand(int UserId):IRequest<OperationResult<bool>>;