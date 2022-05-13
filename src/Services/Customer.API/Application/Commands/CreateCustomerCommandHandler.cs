using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;
using NerdStoreEnterprise.Services.Customer.API.Application.Events;
using NerdStoreEnterprise.Services.Customer.API.Models;

namespace NerdStoreEnterprise.Services.Customer.API.Application.Commands
{
    public class CreateCustomerCommandHandler : CommandHandler, IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ValidationResult> Handle(CreateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Models.Customer(message.Id, message.Name, message.Email, message.Cpf);

            var existingCustomer = await _customerRepository.GetByCpf(customer.Cpf.Number);

            if (existingCustomer is not null)
            {
                AddError("The CPF entered already exists.");
                return ValidationResult;
            }

            _customerRepository.Add(customer);

            customer.AddEvent(new CreatedCustomerEvent(customer.Id, customer.Name, customer.Email.Address,
                customer.Cpf.Number));

            return await PersistData(_customerRepository.UnitOfWork);
        }
    }
}