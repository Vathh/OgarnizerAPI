using FluentValidation;
using OgarnizerAPI.Entities;

namespace OgarnizerAPI.Models.Validators
{
    public class OrderQueryValidator : AbstractValidator<OrderQuery>
    {
        private readonly int[] allowedPageSizes = new[] { 5, 10, 15 };
        private readonly string[] allowedSortByColumnNames = { nameof(Order.Client), nameof(Order.Object), nameof(Order.CreatedDate), nameof(Order.UpdateDate) };

        public OrderQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
