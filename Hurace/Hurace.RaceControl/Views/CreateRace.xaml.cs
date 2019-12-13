using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Hurace.Domain;
using Hurace.RaceControl.Helpers;
using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
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

        public CreateRace()
        {
            InitializeComponent();
        }

        private void TextBoxNumber_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void LocationAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput) return;

            var locations = ViewModel.Locations.Where(loc => loc.Name.Contains(sender.Text))
                .Select(loc => loc.Name).ToList();
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
                ViewModel.SelectedSkiers.Add((Skier) args.ChosenSuggestion);
            }
        }
    }
}