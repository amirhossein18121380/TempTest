using Application.Models.Common;
using Application.Models.Jwt;
using FluentValidation;
using Mediator;
using SharedKernel.ValidationBase;
using SharedKernel.ValidationBase.Contracts;

namespace Application.Features.Admin.Queries.GetToken;

public record AdminGetTokenQuery(string UserName, string Password) : IRequest<OperationResult<AccessToken>>,
    IValidatableModel<AdminGetTokenQuery>
{
    public IValidator<AdminGetTokenQuery> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AdminGetTokenQuery> validator)
    {
        validator.RuleFor(c => c.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter admin username");

        validator.RuleFor(c => c.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter admin password");

        return validator;
    }
};