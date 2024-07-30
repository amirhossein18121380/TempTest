﻿using System.ComponentModel.DataAnnotations;
using Application.Features.Role.Commands.AddRoleCommand;
using Application.Features.Role.Commands.UpdateRoleClaimsCommand;
using Application.Features.Role.Queries.GetAllRolesQuery;
using Application.Features.Role.Queries.GetAuthorizableRoutesQuery;
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
    [Route("api/v{version:apiVersion}/RoleManager")]
    [Authorize(ConstantPolicies.DynamicPermission)]
    [Display(Description = "Managing Related Roles for the System")]

    public class RoleManagerController(ISender sender) : BaseController
    {
        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            var queryResult = await sender.Send(new GetAllRolesQuery());

            return base.OperationResult(queryResult);
        }

        [HttpGet("AuthRoutes")]
        public async Task<IActionResult> GetAuthRoutes()
        {
            var queryModel = await sender.Send(new GetAuthorizableRoutesQuery());

            return base.OperationResult(queryModel);
        }

        /// <summary>
        /// Update a role permissions (claims) based on RouteKey received in AuthRoutes API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("UpdateRolePermissions")]
        public async Task<IActionResult> UpdateRolePermissions(UpdateRoleClaimsCommand model)
        {
            var commandResult =
                await sender.Send(new UpdateRoleClaimsCommand(model.RoleId, model.RoleClaimValue));

            return base.OperationResult(commandResult);
        }

        [HttpPost("NewRole")]
        public async Task<IActionResult> AddRole(AddRoleCommand model)
        {
            var commandResult = await sender.Send(model);

            return base.OperationResult(commandResult);
        }

    }
}
