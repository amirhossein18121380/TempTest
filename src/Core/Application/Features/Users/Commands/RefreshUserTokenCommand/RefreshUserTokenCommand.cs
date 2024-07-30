using Application.Models.Common;
using Application.Models.Jwt;
using FluentValidation;
using Mediator;
using SharedKernel.ValidationBase;
using SharedKernel.ValidationBase.Contracts;

namespace Application.Features.Users.Commands.RefreshUserTokenCommand;

public record RefreshUserTokenCommand(Guid RefreshToken) : IRequest<OperationResult<AccessToken>>,
    IValidatableModel<RefreshUserTokenCommand>
{
    public IValidator<RefreshUserTokenCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RefreshUserTokenCommand> validator)
    {
        validator.RuleFor(c => c.RefreshToken)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter valid user refresh token");

        return validator;
    }
};