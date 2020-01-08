using System;
using FluentValidation;
using Hurace.Domain;

namespace Hurace.Api.Helper
{
    public class SkierValidator : AbstractValidator<Skier>
    {
        public SkierValidator()
        {
            RuleSet("UpdateSkierValidation", () =>
            {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Archived).Equals(false);
            });

            RuleSet("CreateSkierValidation", () =>
            {
                RuleFor(x => x.PictureUrl).Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.PictureUrl))
                    .WithMessage("Please specify a valid URL");
                RuleFor(x => x.FirstName).Length(3, 10);
                RuleFor(x => x.LastName).Length(3, 10);
                RuleFor(x => x.Birthdate).Must(BeAValidDate);
            });
        }

        private static bool BeAValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return true;
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }

        private static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default) && date < DateTime.Now;
        }
    }
}