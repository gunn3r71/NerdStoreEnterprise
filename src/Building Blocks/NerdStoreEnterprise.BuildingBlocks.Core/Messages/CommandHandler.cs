using System.Threading.Tasks;
using FluentValidation.Results;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        public CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message) =>
            ValidationResult.Errors.Add(new ValidationFailure(null, message));

        protected void AddError(string property, string message) =>
            ValidationResult.Errors.Add(new ValidationFailure(property, message));

        protected async Task<ValidationResult> PersistData(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.CommitAsync()) AddError("There was an error persisting the data.");

            return ValidationResult;
        }
    }
}