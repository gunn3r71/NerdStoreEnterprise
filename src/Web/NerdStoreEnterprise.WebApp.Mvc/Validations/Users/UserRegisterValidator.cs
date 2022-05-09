using FluentValidation;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;
using NerdStoreEnterprise.WebApp.Mvc.Models.Users;

namespace NerdStoreEnterprise.WebApp.Mvc.Validations.Users
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterViewModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(150);

            RuleFor(x => x.Cpf)
                .NotEmpty()
                .NotNull()
                .Must(HaveAValidCpf)
                .WithMessage("CPF with invalid format.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .Length(6, 20);

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

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
    }
}