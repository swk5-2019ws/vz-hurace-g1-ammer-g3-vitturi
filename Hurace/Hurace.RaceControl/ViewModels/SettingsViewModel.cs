﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class SettingsViewModel: MvxViewModel
    {
        public SettingsViewModel()
        {
        }

        public override void Prepare()
        {
            base.Prepare();
            VersionDescription = GetVersionDescription();
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get => _versionDescription;
            set => SetProperty(ref _versionDescription, value);
        }

        private string GetVersionDescription()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{package.DisplayName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
