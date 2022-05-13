using System;
using FluentValidation;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;
using NerdStoreEnterprise.Services.Customer.API.Application.Commands;

namespace NerdStoreEnterprise.Services.Customer.API.Application.Validations
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("The customer's {PropertyName} is invalid.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The customer's {PropertyName} was not provided.")
                .NotNull()
                .WithMessage("The customer's {PropertyName} was not provided.");

            RuleFor(x => x.Email)
                .Must(HaveAValidEmail)
                .WithMessage("The customer's {PropertyName} is invalid.");

            RuleFor(x => x.Cpf)
                .Must(HaveAValidCpf)
                .WithMessage("the customer's {PropertyName} is invalid.");
        }

        private static bool HaveAValidCpf(string cpf)
        {
            return Cpf.IsValid(cpf);
        }

        private static bool HaveAValidEmail(string email)
        {
            return Email.IsValid(email);
        }
    }
}