using System;
using System.Collections.Generic;
using System.Linq;
using Hurace.Domain;
using Hurace.RaceControl.Helpers;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class ScreensViewModel : MvxViewModel
    {
        public IEnumerable<ScreenType> ScreenTypes => Enum.GetValues(typeof(ScreenType)).Cast<ScreenType>();

    }
}