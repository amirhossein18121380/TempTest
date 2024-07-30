using Application.Contracts.Identity;
using Application.Contracts.Persistence;
using Application.Models.Common;
using Mediator;

namespace Application.Features.Product.Commands;

internal class AddProductCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager) : IRequestHandler<AddProductCommand, OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserByIdAsync(request.CreatedByUserId);

        if (user == null)
            return OperationResult<bool>.FailureResult("User Not Found");

        var existingProduct = await unitOfWork.ProductRepository
            .GetProductByEmailAndDateAsync(request.ManufactureEmail, request.ProduceDate, false);

        if (existingProduct != null)
            return OperationResult<bool>.FailureResult("A product with the same manufacturer email and produce date already exists");

        await unitOfWork.ProductRepository.AddProductAsync(new Domain.Entities.Product.Product()
        {
            CreatedByUserId = user.Id,
            Name = request.Name,
            ManufacturePhone = request.ManufacturePhone,
            ManufactureEmail = request.ManufactureEmail,
            IsAvailable = request.IsAvailable,
            ProduceDate = request.ProduceDate
        });

        await unitOfWork.CommitAsync();

        return OperationResult<bool>.SuccessResult(true);
    }
}