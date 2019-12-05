using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Helpers
{
    public class BaseApplicationMvxPage<TViewModel>: MvxWindowsPage<TViewModel> where TViewModel: MvxViewModel
    {
    }
}
