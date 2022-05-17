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
                .WithMessage("The product Id is invalid.");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage(product => $"The quantity of {product.Name} must be greater than 0.")
                .LessThan(5)
                .WithMessage(product => $"The quantity of {product.Name} must be less than 5.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage(product => $"The {product.Name} value must be greater than 0.");
        }
    }
}