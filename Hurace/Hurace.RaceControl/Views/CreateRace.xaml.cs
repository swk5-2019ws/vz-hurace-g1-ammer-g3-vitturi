using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Input;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Hurace.Domain;
using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using Microsoft.Toolkit.Uwp.Notifications;
using MvvmCross;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    public abstract class CreateRaceAbstract : BaseApplicationMvxPage<CreateRaceViewModel>
    {
    }

    [MvxViewFor(typeof(CreateRaceViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class CreateRace : CreateRaceAbstract
    {
        private const string EmptyLocationMessage = "No locations found!";
        private MvxSubscriptionToken _token;
        private ToastNotification toast;
        private ToastContent toastContent;

        public CreateRace()
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
                if (message.DialogEvent == DialogEvent.RaceEditSuccess)
                {
                    toast = new ToastNotification(toastContent.GetXml()) { ExpirationTime = DateTime.Now.AddSeconds(4) };
                    ToastNotificationManager.CreateToastNotifier().Show(toast);
                }
            });

            if (ViewModel.SelectedLocation != null) LocationSuggestBox.Text = ViewModel.SelectedLocation.Name;
        }

        private void CreateToast()
        {
            toastContent = new ToastContent
            {
                Launch = "hurace-race-updated",
                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text = "Race update",
                                HintMaxLines = 1
                            },

                            new AdaptiveText
                            {
                                Text = "The race was updated successfully!"
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

        private void TextBoxNumber_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void LocationAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput) return;

            var locations = ViewModel.Locations.Where(loc => loc.Name.Contains(sender.Text)).Select(loc => loc.Name)
                .ToList();
            sender.ItemsSource = locations.Count == 0 ? new List<string> {EmptyLocationMessage} : locations;
        }

        private void LocationAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender,
            AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            sender.Text = args.SelectedItem.ToString();
        }

        private void LocationAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender,
            AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null && sender.Text != EmptyLocationMessage)
            {
                sender.Text = args.ChosenSuggestion.ToString();
                ViewModel.SelectedLocation = ViewModel.Locations.First(loc => loc.Name == sender.Text);
            }
        }

        private void SkierAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender,
            AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            sender.Text = string.Empty;
        }

        private void SkierAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender,
            AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                sender.Text = string.Empty;
                var messenger = ViewModel.Messenger;
                ViewModel.StartListEntries.Add(new StartListEntryViewModel(messenger)
                {
                    Skier = (Skier) args.ChosenSuggestion,
                    StartPosition = ViewModel.StartListEntries.Count + 1,
                    RaceStatus = ViewModel.Status
                });
            }
        }

        private void StartListEntry_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse ||
                e.Pointer.PointerDeviceType == PointerDeviceType.Pen)
                VisualStateManager.GoToState(sender as Control, "HoverButtonsShown", true);
        }

        private void StartListEntry_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as Control, "HoverButtonsHidden", true);
        }
    }
}