using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Hurace.RaceControl.Views
{
    public abstract class NavigationRootAbstract : BaseApplicationMvxPage<NavigationRootViewModel>
    {
    }

    [MvxViewFor(typeof(NavigationRootViewModel))]
    public sealed partial class NavigationRoot : NavigationRootAbstract
    {
        public NavigationRoot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// NavigationView command binding is not supported at the moment: https://github.com/microsoft/microsoft-ui-xaml/issues/944.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The args</param>
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked) ViewModel.ShowSettings();

            switch (args.InvokedItem as string)
            {
                case "Races":
                    ViewModel.ShowHome();
                    break;
                case "Screens":
                    NavView.Header = "Screens";
                    ViewModel.ShowScreens();
                    break;
                case "Create race":
                    ViewModel.ShowCreateRace();
                    break;
                case "Current race":
                    ViewModel.OpenCurrentRaceCommand.Execute();
                    break;
            }
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            var frame = FindListBoxChildOfType<Frame>(NavView);
            if (frame.CanGoBack) frame.GoBack();
        }

        private static T FindListBoxChildOfType<T>(DependencyObject root) where T : class
        {
            var dependencyObjects = new Queue<DependencyObject>();
            dependencyObjects.Enqueue(root);
            while (dependencyObjects.Count > 0)
            {
                var current = dependencyObjects.Dequeue();
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    if (child is T typedChild) return typedChild;
                    dependencyObjects.Enqueue(child);
                }
            }

            return null;
        }
    }
}