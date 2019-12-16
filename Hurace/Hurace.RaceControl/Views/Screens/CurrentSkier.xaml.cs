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
using Hurace.RaceControl.ViewModels.Screens;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;


namespace Hurace.RaceControl.Views.Screens
{
    [MvxViewFor(typeof(CurrentSkierViewModel))]
    public sealed partial class CurrentSkier : MvxWindowsPage
    {
        public CurrentSkier()
        {
            this.InitializeComponent();
        }
    }
}
