using System;
using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using MvvmCross;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Plugins.Messenger;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    public abstract class ScreenSelectionAbstract : BaseApplicationMvxPage<ScreenSelectionViewModel>
    {
    }

    [MvxViewFor(typeof(ScreenSelectionViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class ScreenSelection : ScreenSelectionAbstract
    {
        private MvxSubscriptionToken _token;

        public ScreenSelection()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            var messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
            _token = messenger.Subscribe<DialogMessage>(message =>
            {
                try
                {
                    DialogEventNotification.Show("There is no active race at the moment!", 2000);
                }
                catch (Exception e)
                {
                    // see: https://github.com/windows-toolkit/WindowsCommunityToolkit/issues/2899
                }
            });
        }
    }
}