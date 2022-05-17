using System;
using FluentValidation;
using NerdStoreEnterprise.Services.Cart.API.Models;

namespace NerdStoreEnterprise.Services.Cart.API.Validations
{
    public class CustomerCartValidator : AbstractValidator<CustomerCart>
    {
        public CustomerCartValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("The customer Id is invalid.");

            RuleFor(x => x.Items.Count)
                .GreaterThan(0)
                .WithMessage("Cart has no items.");

            RuleFor(x => x.Total)
                .GreaterThan(0)
                .WithMessage("The total cart value must be greater than 0.");
        }
    }
}