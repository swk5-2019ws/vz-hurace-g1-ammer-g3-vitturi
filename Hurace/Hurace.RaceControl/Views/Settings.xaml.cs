﻿using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    [MvxViewFor(typeof(SettingsViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class Settings : MvxWindowsPage
    {
        public Settings()
        {
            InitializeComponent();
        }
    }
}