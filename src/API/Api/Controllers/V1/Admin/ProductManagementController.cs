using System.ComponentModel.DataAnnotations;
using Application.Features.Product.Queries.GetAllProducts;
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
    [Route("api/v{version:apiVersion}/ProductManagement")]
    [Display(Description = "Managing Products")]
    [Authorize(ConstantPolicies.DynamicPermission)]
    public class ProductManagementController(ISender sender) : BaseController
    {

        [HttpGet("ProductList")]
        public async Task<IActionResult> GetProducts()
        {
            var queryResult = await sender.Send(new GetAllProductsQuery());

            return base.OperationResult(queryResult);
        }
    }
}
