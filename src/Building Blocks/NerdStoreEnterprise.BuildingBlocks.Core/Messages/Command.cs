using System;
using FluentValidation.Results;
using MediatR;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid() => throw new NotImplementedException();
    }
}