using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Hurace.RaceControl.Helpers;
using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels.Screens;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views.Screens
{
    public abstract class CurrentResultAbstract : BaseApplicationMvxPage<CurrentResultViewModel>
    {
    }

    [MvxViewFor(typeof(CurrentResultViewModel))]
    [MvxWindowPresentation]
    public sealed partial class CurrentResult : CurrentResultAbstract
    {
        public CurrentResult()
        {
            this.InitializeComponent();
        }
    }
}
