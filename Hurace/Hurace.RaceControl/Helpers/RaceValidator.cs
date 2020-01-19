using System;
using FluentValidation;
using Hurace.Domain;

namespace Hurace.RaceControl.Helpers
{
    public class RaceValidator : AbstractValidator<Race>
    {
        public RaceValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(5, 250);
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.NumberOfSensors).NotEmpty().GreaterThan(1);
            RuleFor(x => x.Website).Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.Website))
                .WithMessage("Please specify a valid URL");
            RuleFor(x => x.PictureUrl).Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.PictureUrl))
                .WithMessage("Please specify a valid URL");
        }

        private static bool BeAValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return true;
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }
    }
}