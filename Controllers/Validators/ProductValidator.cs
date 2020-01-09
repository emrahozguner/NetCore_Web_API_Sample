using API.Resources;
using FluentValidation;

namespace API.Controllers.Validators
{
    public class ProductValidator : AbstractValidator<SaveProductResource>
    {
        public ProductValidator()
        {
            RuleFor(a => a.CategoryId).GreaterThan(0)
                .WithMessage($"Please specify {nameof(SaveProductResource.CategoryId)}");
        }
    }
}