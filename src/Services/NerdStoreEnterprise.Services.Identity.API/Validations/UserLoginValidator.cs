using FluentValidation;
using NerdStoreEnterprise.Services.Identity.API.Models;

namespace NerdStoreEnterprise.Services.Identity.API.Validations
{
    public class UserLoginValidator : AbstractValidator<UserLoginViewModel>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8);
        }
    }
}
