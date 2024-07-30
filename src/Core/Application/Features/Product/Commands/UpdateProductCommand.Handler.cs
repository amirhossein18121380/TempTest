using Application.Contracts.Persistence;
using Application.Models.Common;
using Mediator;

namespace Application.Features.Product.Commands;

public class UpdateUserOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetProductsByIdAsync(request.Id, true);

        if (product is null)
            return OperationResult<bool>.NotFoundResult("Specified Product not found");

        if (product.CreatedByUserId != request.UpdatedByUserId)
            return OperationResult<bool>.FailureResult("This User isn't allowed to update the product.");

        product.Name = request.Name;
        product.IsAvailable = request.IsAvailable;
        product.ManufacturePhone = request.ManufacturePhone;

        var existingProduct = await unitOfWork.ProductRepository
            .GetProductByEmailAndDateAsync(request.ManufactureEmail, request.ProduceDate, false);

        if (existingProduct != null)
            return OperationResult<bool>.FailureResult("A product with the same manufacturer email and produce date already exists");

        product.ProduceDate = request.ProduceDate;
        product.ManufactureEmail = request.ManufactureEmail;

        await unitOfWork.CommitAsync();

        return OperationResult<bool>.SuccessResult(true);
    }
}