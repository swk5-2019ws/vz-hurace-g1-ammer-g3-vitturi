using System;

namespace Hurace.Core.Helper
{
    public class SeasonParser
    {
        public static DateTime GetSeasonsStart(uint season)
        {
            return Convert.ToDateTime($"01.10.{season}");
        }
        
        public static DateTime GetSeasonsEnd(uint season)
        {
            return Convert.ToDateTime($"01.04.{season+1}");
        }
    }
}