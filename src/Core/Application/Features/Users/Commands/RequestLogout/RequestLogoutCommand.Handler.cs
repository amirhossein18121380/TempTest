﻿using Application.Contracts.Identity;
using Application.Models.Common;
using Mediator;

namespace Application.Features.Users.Commands.RequestLogout
{
    internal class RequestLogoutCommandHandler:IRequestHandler<RequestLogoutCommand,OperationResult<bool>>
    {
        private readonly IAppUserManager _userManager;

        public RequestLogoutCommandHandler(IAppUserManager userManager)
        {
            _userManager = userManager;
        }

        public async ValueTask<OperationResult<bool>> Handle(RequestLogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserByIdAsync(request.UserId);

            if (user == null) 
                return OperationResult<bool>.FailureResult("User not found");

            await _userManager.UpdateSecurityStampAsync(user);

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
