using System;
using System.Collections.Generic;
using System.Linq;
using Hurace.RaceControl.Extensions;

namespace Hurace.RaceControl.Helpers
{
    internal class EnumHelper
    {
        public static IEnumerable<Tuple<Enum, string>> GetAllValuesAndDescriptions<TEnum>()
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException($"{nameof(TEnum)} must be an enum type");

            return Enum.GetValues(typeof(TEnum)).Cast<Enum>().Select(e => new Tuple<Enum, string>(e, e.Description()))
                .ToList();
        }
    }
}