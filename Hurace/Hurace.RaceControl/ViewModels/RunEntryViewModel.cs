using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurace.Domain;
using Hurace.RaceControl.Helpers.MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugins.Messenger;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class RunEntryViewModel : MvxViewModel
    {
        private Run _run;
        private IMvxMessenger _messenger;

        public RunEntryViewModel(IMvxMessenger messenger, Run run)
        {
            _messenger = messenger;
            Run = run;
            DisqualifySkierCommand = new MvxCommand<Run>(r => _messenger.Publish(new DisqualifySkierMessage(this, Run)));
        }

        public Run Run
        {
            get => _run;
            set => SetProperty(ref _run, value);
        }

        public bool CanDisqualify => Run.Status != RunStatus.Disqualified;

        public MvxCommand<Run> DisqualifySkierCommand { get; set; }
    }
}
