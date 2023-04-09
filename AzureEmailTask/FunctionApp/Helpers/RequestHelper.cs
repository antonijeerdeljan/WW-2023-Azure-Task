using AzureEmailTask.Domain.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AzureEmailTask.FunctionApp.Helpers
{
    public static class RequestHelper
    {
        public static IActionResult IsValidRequest<TValidator, TModel>(TValidator validator, TModel modelToValidate) where TValidator : AbstractValidator<TModel>
        {

            var validationResult = validator.Validate(modelToValidate);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new
                {
                    Error = "Validation error",
                    Details = e.ErrorMessage,
                    Status = e.ErrorCode
                }).ToArray();

                return new BadRequestObjectResult(errors);
            }

            return new OkObjectResult(modelToValidate);
        }
    }
}
