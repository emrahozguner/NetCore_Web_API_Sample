using FluentValidation;

namespace API.Infrastructure
{
    public static class ErrorCodes
    {
        public static readonly ErrorCode ValidationErrors = new ErrorCode("0", "Validation errors occurred.");

        public static readonly ErrorCode FieldInvalid = new ErrorCode("InvalidValue", "Invalid field value.");
        public static readonly ErrorCode FieldEmpty = new ErrorCode("1", "Field can't be empty.");
        public static readonly ErrorCode FieldNull = new ErrorCode("2", "Field can't be null.");

        public static readonly ErrorCode FieldNullOrWhitespace =
            new ErrorCode("3", "Field can't be null or whitespace.");

        public static readonly ErrorCode FieldNotUnique = new ErrorCode("4", "Field has to be unique.");

        public static readonly ErrorCode
            FieldDoesntExist = new ErrorCode("5", "Field with specified ID doesn't exist.");

        public static readonly ErrorCode FieldMustBeNull = new ErrorCode("10", "Field must be null.");

        public static readonly ErrorCode FieldHaveDuplicateValues =
            new ErrorCode("11", "Field must not have duplicate values.");

        public static ErrorCode FieldNotGreater<T>(T value)
        {
            return new ErrorCode("6", $"Field value has to be greater than {value}.");
        }

        public static ErrorCode FieldNotGreaterOrEqual<T>(T value)
        {
            return new ErrorCode("7", $"Field value has to be greater than or equal to {value}.");
        }

        public static ErrorCode FieldNotLess<T>(T value)
        {
            return new ErrorCode("8", $"Field value has to be less than {value}.");
        }

        public static ErrorCode FieldNotLessOrEqual<T>(T value)
        {
            return new ErrorCode("9", $"Field value has to be less than or equal to {value}.");
        }

        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> ruleBuilder, ErrorCode error)
        {
            return ruleBuilder
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }
    }

    public class ErrorCode
    {
        public ErrorCode(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }
    }
}