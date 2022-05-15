using System;
using FluentValidation;
using NerdStoreEnterprise.Services.Cart.API.Models;

namespace NerdStoreEnterprise.Services.Cart.API.Validations
{
    public class CartItemValidator : AbstractValidator<CartItem>
    {
        public CartItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product Id is invalid.");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .LessThan(5);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}