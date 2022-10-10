using FluentValidation;
using OgarnizerAPI.Entities;

namespace OgarnizerAPI.Models.Validators
{
    public class ClosedServiceQueryValidator : AbstractValidator<ClosedServiceQuery>
    {
        private readonly int[] allowedPageSizes = new[] { 5, 10, 15 };
        private readonly string[] allowedSortByColumnNames = { nameof(ClosedService.Object), nameof(ClosedService.CreatedDate), nameof(ClosedService.UpdateDate)};

        public ClosedServiceQueryValidator()
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
