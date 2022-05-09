using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private List<string> _errors = new();

        protected IReadOnlyList<string> Errors => _errors;

        protected IActionResult CustomResponse(object result = null)
        {
            if (!HasErrors) return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>()
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList();

            AddError(errors);

            return CustomResponse();
        }

        protected void AddError(string error) => _errors.Add(error);

        protected void AddError(IEnumerable<string> errors) => _errors.AddRange(errors);

        protected IActionResult AddError(ValidationResult validationResult)
        {
            AddError(validationResult.Errors.Select(x => x.ErrorMessage));

            return CustomResponse();
        }

        private bool HasErrors => Errors.Any();
    }
}