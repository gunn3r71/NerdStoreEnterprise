using FluentValidation;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;
using NerdStoreEnterprise.Services.Identity.API.Models;

namespace NerdStoreEnterprise.Services.Identity.API.Validations
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterViewModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(150);

            RuleFor(x => x.Cpf)
                .Must(HaveAValidCpf)
                .NotNull()
                .NotEmpty();


            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .Length(6, 20);

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .Must(HaveAValidEmail);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8);

            RuleFor(x => x).Custom((x, validationContext) =>
            {
                if (x.Password != x.ConfirmPassword) validationContext.AddFailure(nameof(x.ConfirmPassword), "The passwords entered are not the same.");
            });
        }

        private static bool HaveAValidCpf(string cpf) =>
            Cpf.IsValid(cpf);

        private static bool HaveAValidEmail(string email) =>
            Email.IsValid(email);
    }
}