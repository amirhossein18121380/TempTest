using System.ComponentModel.DataAnnotations;
using Application.Features.Users.Queries.GetUsers;
using Asp.Versioning;
using Infrastructure.Identity.Identity.PermissionManager;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFramework.BaseController;

namespace Api.Controllers.V1.Admin
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/UserManagement")]
    [Display(Description = "Managing API Users")]
    [Authorize(ConstantPolicies.DynamicPermission)]
    public class UserManagementController : BaseController
    {
        private readonly ISender _sender;

        public UserManagementController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("CurrentUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var queryResult = await _sender.Send(new GetUsersQuery());

            return base.OperationResult(queryResult);
        }
    }
}
