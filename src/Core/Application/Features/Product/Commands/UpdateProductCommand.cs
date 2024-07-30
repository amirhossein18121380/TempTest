using Application.Models.Common;
using FluentValidation;
using Mediator;
using SharedKernel.ValidationBase;
using SharedKernel.ValidationBase.Contracts;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Application.Features.Product.Commands;

public record UpdateProductCommand(int Id, string Name, DateTime ProduceDate, string ManufacturePhone, string ManufactureEmail, bool IsAvailable) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateProductCommand>
{
    [JsonIgnore]
    public int UpdatedByUserId { get; set; }

    public IValidator<UpdateProductCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateProductCommand> validator)
    {
        validator.RuleFor(c => c.Id)
            .GreaterThan(0)
            .WithMessage("Invalid product ID");

        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter the product name");

        validator.RuleFor(c => c.ProduceDate)
            .NotNull()
            .WithMessage("Please enter the produce date");

        validator.RuleFor(c => c.ManufacturePhone).NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")).WithMessage("Phone number is not valid");

        validator.RuleFor(c => c.ManufactureEmail)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage("Please enter a valid manufacturer email");

        return validator;
    }
}
