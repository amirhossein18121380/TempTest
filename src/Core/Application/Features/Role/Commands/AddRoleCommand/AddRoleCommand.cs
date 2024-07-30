using Application.Models.Common;
using FluentValidation;
using Mediator;
using SharedKernel.ValidationBase;
using SharedKernel.ValidationBase.Contracts;

namespace Application.Features.Role.Commands.AddRoleCommand;

public record AddRoleCommand(string RoleName) : IRequest<OperationResult<bool>>,
    IValidatableModel<AddRoleCommand>
{
    public IValidator<AddRoleCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AddRoleCommand> validator)
    {
        validator
            .RuleFor(c => c.RoleName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter role name");

        return validator;
    }
};