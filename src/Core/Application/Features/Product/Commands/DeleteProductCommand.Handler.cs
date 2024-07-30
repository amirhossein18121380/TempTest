using Application.Contracts.Persistence;
using Application.Models.Common;
using Mediator;

namespace Application.Features.Product.Commands;

public class DeleteUserOrdersCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetProductsByIdAsync(request.ProductId, true);

        if (product.CreatedByUserId != request.DeletedByUserId)
            return OperationResult<bool>.FailureResult("This User isn't allowed to remove the product.");

        await unitOfWork.ProductRepository.HardDeleteProductAsync(request.ProductId);

        return OperationResult<bool>.SuccessResult(true);
    }
}