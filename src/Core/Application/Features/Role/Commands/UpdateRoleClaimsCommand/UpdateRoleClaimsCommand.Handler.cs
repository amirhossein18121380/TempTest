using Application.Contracts.Identity;
using Application.Models.Common;
using Application.Models.Identity;
using Mediator;

namespace Application.Features.Role.Commands.UpdateRoleClaimsCommand
{
    internal class UpdateRoleClaimsCommandHandler:IRequestHandler<UpdateRoleClaimsCommand,OperationResult<bool>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public UpdateRoleClaimsCommandHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public async ValueTask<OperationResult<bool>> Handle(UpdateRoleClaimsCommand request, CancellationToken cancellationToken)
        {
            var updateRoleResult = await _roleManagerService.ChangeRolePermissionsAsync(new EditRolePermissionsDto()
                { RoleId = request.RoleId, Permissions = request.RoleClaimValue });

            return updateRoleResult
                ? OperationResult<bool>.SuccessResult(true)
                : OperationResult<bool>.FailureResult("Could Not Update Claims for given Role");
        }
    }
}
