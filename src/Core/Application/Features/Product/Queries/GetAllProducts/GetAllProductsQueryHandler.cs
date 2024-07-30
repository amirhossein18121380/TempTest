using Application.Contracts.Persistence;
using Application.Models.Common;
using Mediator;

namespace Application.Features.Product.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllProductsQuery, OperationResult<List<GetAllProductsQueryResult>>>
    {
        public async ValueTask<OperationResult<List<GetAllProductsQueryResult>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.ProductRepository.GetAllProductsAsync();

            var result = products.Select(c => new GetAllProductsQueryResult(c.Id, c.Name, c.ManufacturePhone,
                c.ManufactureEmail, c.ProduceDate, c.IsAvailable)).ToList();

            return OperationResult<List<GetAllProductsQueryResult>>.SuccessResult(result);
        }
    }
}