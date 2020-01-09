using System;
using FluentValidation;
using FluentValidation.Results;

namespace API.Infrastructure
{
    public static class ValidationExtensions
    {
        public static ValidationResult ValidateIfNullModel<T>(this AbstractValidator<T> validator, T model)
        {
            return model == null
                ? new ValidationResult(new[] {new ValidationFailure(typeof(T).Name, "Model cannot be null.")})
                : validator.Validate(model);
        }

        public static IRuleBuilderOptions<T, TProperty> FieldNotEmpty<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            var error = ErrorCodes.FieldEmpty;

            return ruleBuilder
                .NotEmpty()
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, TProperty> FieldNotNull<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            var error = ErrorCodes.FieldNull;

            return ruleBuilder
                .NotNull()
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, TProperty> FieldMustBeNull<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            var error = ErrorCodes.FieldMustBeNull;

            return ruleBuilder
                .Must(value => value == null)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, string> FieldNotNullOrWhitespace<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            var error = ErrorCodes.FieldNullOrWhitespace;

            return ruleBuilder
                .Must(value => !string.IsNullOrWhiteSpace(value))
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, string> FieldLength<T>(this IRuleBuilder<T, string> ruleBuilder,
            int minValue, int maxValue)
        {
            var error = ErrorCodes.FieldInvalid;

            return ruleBuilder
                .Length(minValue, maxValue)
                .WithErrorCode(error.Code)
                .WithMessage($"Field length has to be between {minValue} and {maxValue}");
        }

        public static IRuleBuilderOptions<T, int> FieldIsBetween<T>(this IRuleBuilder<T, int> ruleBuilder, int minValue,
            int maxValue)
        {
            var error = ErrorCodes.FieldInvalid;

            return ruleBuilder
                .Must(x => x >= minValue && x <= maxValue)
                .WithErrorCode(error.Code)
                .WithMessage($"Field value has to be between {minValue} and {maxValue}");
        }

        public static IRuleBuilderOptions<T, int?> FieldIsBetween<T>(this IRuleBuilder<T, int?> ruleBuilder,
            int minValue, int maxValue)
        {
            var error = ErrorCodes.FieldInvalid;

            return ruleBuilder
                .Must(x => x >= minValue && x <= maxValue)
                .WithErrorCode(error.Code)
                .WithMessage($"Field value has to be between {minValue} and {maxValue}");
        }

        public static IRuleBuilderOptions<T, float?> FieldIsBetween<T>(this IRuleBuilder<T, float?> ruleBuilder,
            float minValue, float maxValue)
        {
            var error = ErrorCodes.FieldInvalid;

            return ruleBuilder
                .Must(x => x >= minValue && x <= maxValue)
                .WithErrorCode(error.Code)
                .WithMessage($"Field value has to be between {minValue} and {maxValue}");
        }

        public static IRuleBuilderOptions<T, TProperty> FieldIsUnique<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, TProperty, bool> predicate)
        {
            var error = ErrorCodes.FieldNotUnique;

            return ruleBuilder
                .Must(predicate)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, decimal> FieldGreaterThan<T>(this IRuleBuilder<T, decimal> ruleBuilder,
            decimal value)
        {
            var error = ErrorCodes.FieldNotGreater(value);

            return ruleBuilder
                .Must(x => x > value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, decimal?> FieldGreaterThan<T>(this IRuleBuilder<T, decimal?> ruleBuilder,
            decimal value)
        {
            var error = ErrorCodes.FieldNotGreater(value);

            return ruleBuilder
                .Must(x => x != null && x > value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, DateTime> FieldGreaterThan<T>(this IRuleBuilder<T, DateTime> ruleBuilder,
            DateTime value)
        {
            var error = ErrorCodes.FieldNotGreater(value);

            return ruleBuilder
                .Must(x => x > value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, int> FieldGreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, int value)
        {
            var error = ErrorCodes.FieldNotGreater(value);

            return ruleBuilder
                .Must(x => x > value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, int?> FieldGreaterThan<T>(this IRuleBuilder<T, int?> ruleBuilder,
            int value)
        {
            var error = ErrorCodes.FieldNotGreater(value);

            return ruleBuilder
                .Must(x => x != null && x > value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, double> FieldGreaterThanOrEqualTo<T>(
            this IRuleBuilder<T, double> ruleBuilder, double value)
        {
            var error = ErrorCodes.FieldNotGreaterOrEqual(value);

            return ruleBuilder
                .Must(x => x >= value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, int> FieldGreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder,
            int value)
        {
            var error = ErrorCodes.FieldNotGreaterOrEqual(value);

            return ruleBuilder
                .Must(x => x >= value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        #region Integer Field Validation Extensions

        public static IRuleBuilderOptions<T, int?> FieldGreaterThanOrEqualTo<T>(this IRuleBuilder<T, int?> ruleBuilder,
            int value)
        {
            var error = ErrorCodes.FieldNotGreaterOrEqual(value);

            return ruleBuilder
                .Must(x => x != null && x >= value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, int> FieldLessThan<T>(this IRuleBuilder<T, int> ruleBuilder, int value)
        {
            var error = ErrorCodes.FieldNotLess(value);

            return ruleBuilder
                .Must(x => x < value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, int?> FieldLessThan<T>(this IRuleBuilder<T, int?> ruleBuilder, int value)
        {
            var error = ErrorCodes.FieldNotLess(value);

            return ruleBuilder
                .Must(x => x != null && x < value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, int> FieldLessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder,
            int value)
        {
            var error = ErrorCodes.FieldNotLessOrEqual(value);

            return ruleBuilder
                .Must(x => x <= value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, int?> FieldLessThanOrEqualTo<T>(this IRuleBuilder<T, int?> ruleBuilder,
            int value)
        {
            var error = ErrorCodes.FieldNotLessOrEqual(value);

            return ruleBuilder
                .Must(x => x != null && x <= value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        public static IRuleBuilderOptions<T, DateTime?> FieldLessThanOrEqualTo<T>(
            this IRuleBuilder<T, DateTime?> ruleBuilder, DateTime value)
        {
            var error = ErrorCodes.FieldNotLessOrEqual(value);

            return ruleBuilder
                .Must(x => x != null && x <= value)
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }

        #endregion
    }
}