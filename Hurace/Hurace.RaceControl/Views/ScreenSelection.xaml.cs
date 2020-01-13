using System;
using Windows.UI.Notifications;
using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using Microsoft.Toolkit.Uwp.Notifications;
using MvvmCross;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Plugin.Messenger;
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
        private ToastNotification toast;
        private ToastContent toastContent;

        public ScreenSelection()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            var messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
            CreateToast();
            _token = messenger.Subscribe<DialogMessage>(message =>
            {
                toast = new ToastNotification(toastContent.GetXml()) {ExpirationTime = DateTime.Now.AddSeconds(2)};
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            });
        }

        private void CreateToast()
        {
            toastContent = new ToastContent
            {
                Launch = "hurace-no-active-race",
                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text = "Could not open screen",
                                HintMaxLines = 1
                            },

                            new AdaptiveText
                            {
                                Text = "There is no active race at the moment!"
                            }
                        }
                    }
                },
                Audio = new ToastAudio
                {
                    Src = new Uri("ms-winsoundevent:Notification.Reminder")
                }
            };
            toast = new ToastNotification(toastContent.GetXml()) {ExpirationTime = DateTime.Now.AddSeconds(2)};
        }
    }
}