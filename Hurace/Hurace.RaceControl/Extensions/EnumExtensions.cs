using System;
using Hurace.Domain;

namespace Hurace.RaceControl.Extensions
{
    internal static class EnumExtensions
    {
        public static string Description(this Enum value)
        {
            if (value.GetType() == typeof(RaceType))
                switch ((RaceType) value)
                {
                    case RaceType.Slalom:
                        return "Slalom";
                    case RaceType.SuperSlalom:
                        return "Super Slalom";
                    default:
                        throw new NotImplementedException();
                }

            if (value.GetType() == typeof(Gender))
                switch ((Gender) value)
                {
                    case Gender.Male:
                        return "Male";
                    case Gender.Female:
                        return "Female";
                    default:
                        throw new NotImplementedException();
                }

            return "Unknown";
        }
    }
}