using System.Text.RegularExpressions;
using Application.Models.Common;
using Application.Profiles;
using Domain.Entities.User;
using FluentValidation;
using Mediator;
using SharedKernel.ValidationBase;
using SharedKernel.ValidationBase.Contracts;

namespace Application.Features.Users.Commands.Create;

public record UserCreateCommand
    (string UserName, string Name, string FamilyName, string PhoneNumber) 
    : IRequest<OperationResult<UserCreateCommandResult>>
        ,IValidatableModel<UserCreateCommand>
,ICreateMapper<User>
{

    public IValidator<UserCreateCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UserCreateCommand> validator)
    {

        validator
            .RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("User must have first name");

        validator.RuleFor(c => c.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter your username");

        validator
            .RuleFor(c => c.FamilyName)
            .NotEmpty()
            .NotNull()
            .WithMessage("User must have last name");


        validator.RuleFor(c => c.PhoneNumber).NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")).WithMessage("Phone number is not valid");

        return validator;
    }
}