using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace API.Infrastructure
{
    public class ValidationErrorResult : ErrorResult
    {
        private ValidationErrorResult(ErrorModel resultModel, int statusCode) : base(resultModel, statusCode)
        {
        }

        public static ValidationErrorResult Create(ValidationResult validationResult, string target)
        {
            return new ValidationErrorResult(new ValidationErrorModel(validationResult, target),
                StatusCodes.Status422UnprocessableEntity);
        }
    }

    public class ValidationErrorModel : ErrorModel
    {
        public ValidationErrorModel(ValidationResult validationResult, string target)
        {
            Target = target;
            Code = ErrorCodes.ValidationErrors.Code;
            Message = ErrorCodes.ValidationErrors.Message;

            Details = validationResult.Errors.Select(
                e => new ValidationError(e.PropertyName, e.ErrorMessage, e.ErrorCode)).ToList();
        }

        public List<ValidationError> Details { get; }
    }

    public class ValidationError
    {
        public ValidationError(string field, string message, string code)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
            Code = code;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public string Code { get; }
    }
}