using Application.Models.Common;
using Mediator;

namespace Application.Features.Product.Queries.GetAllProducts;

public record GetAllProductsQuery():IRequest<OperationResult<List<GetAllProductsQueryResult>>>;