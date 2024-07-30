using System.Text.Json.Serialization;
using Application.Models.Common;
using FluentValidation;
using Mediator;
using SharedKernel.ValidationBase;
using SharedKernel.ValidationBase.Contracts;

namespace Application.Features.Product.Commands;

//public record DeleteProductCommand(int ProductId, int UserId) : IRequest<OperationResult<bool>>;


public record DeleteProductCommand(int ProductId) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteProductCommand>
{
    [JsonIgnore]
    public int DeletedByUserId { get; set; }

    public IValidator<DeleteProductCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteProductCommand> validator)
    {
        validator.RuleFor(c => c.ProductId)
            .GreaterThan(0)
            .WithMessage("Invalid product ID");

        return validator;
    }
}
