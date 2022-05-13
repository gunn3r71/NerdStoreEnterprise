using FluentValidation;
using NerdStoreEnterprise.WebApp.Mvc.Models.Users;

namespace NerdStoreEnterprise.WebApp.Mvc.Validations.Users
{
    public class UserLoginValidator : AbstractValidator<UserLoginViewModel>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8);
        }
    }
}
