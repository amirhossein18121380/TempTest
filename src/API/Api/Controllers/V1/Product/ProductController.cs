using Application.Features.Product.Commands;
using Application.Features.Product.Queries.GetAllProducts;
using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFramework.BaseController;

namespace Api.Controllers.V1.Product;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/User")]
public class ProductController(ISender sender) : BaseController
{
    [HttpPost("CreateNewProduct")]
    [Authorize]
    public async Task<IActionResult> CreateNewProduct(AddProductCommand model)
    {
        model.CreatedByUserId = base.UserId;
        var command = await sender.Send(model);
        return base.OperationResult(command);
    }

    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        var query = await sender.Send(new GetAllProductsQuery());
        return base.OperationResult(query);
    }

    [HttpPut("UpdateProduct")]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand model)
    {
        model.UpdatedByUserId = base.UserId;
        var command = await sender.Send(model);
        return base.OperationResult(command);
    }

    [HttpDelete("DeleteProduct")]
    [Authorize]
    public async Task<IActionResult> DeleteProduct(DeleteProductCommand model)
    {
        model.DeletedByUserId = base.UserId;
        var command = await sender.Send(model);
        return base.OperationResult(command);
    }
}