using FluentValidation;
using OgarnizerAPI.Entities;

namespace OgarnizerAPI.Models.Validators
{
    public class ClosedOrderQueryValidator : AbstractValidator<ClosedOrderQuery>
    {
        private readonly int[] allowedPageSizes = new[] { 5, 10, 15 };
        private readonly string[] allowedSortByColumnNames = { nameof(ClosedOrder.Client), nameof(ClosedOrder.Object), nameof(ClosedOrder.CreatedDate), nameof(ClosedOrder.UpdateDate) };

        public ClosedOrderQueryValidator()
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
