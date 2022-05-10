using FluentValidation.Results;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages.IntegrationEvents
{
    public class ResponseMessage : Message
    {
        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; set; }
    }
}