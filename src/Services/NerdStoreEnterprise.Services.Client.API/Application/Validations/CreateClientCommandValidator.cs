using System;
using FluentValidation;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;
using NerdStoreEnterprise.Services.Client.API.Application.Commands;

namespace NerdStoreEnterprise.Services.Client.API.Application.Validations
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("The client's {PropertyName} is invalid.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The client's {PropertyName} was not provided.")
                .NotNull()
                .WithMessage("The client's {PropertyName} was not provided.");

            RuleFor(x => x.Email)
                .Must(HaveAValidEmail)
                .WithMessage("The client's {PropertyName} is invalid.");

            RuleFor(x => x.Cpf)
                .Must(HaveAValidCpf)
                .WithMessage("the client's {PropertyName} is invalid.");
        }

        public static bool HaveAValidCpf(string cpf) => 
            Cpf.IsValid(cpf);

        public static bool HaveAValidEmail(string email) => 
            Email.IsValid(email);
    }
}