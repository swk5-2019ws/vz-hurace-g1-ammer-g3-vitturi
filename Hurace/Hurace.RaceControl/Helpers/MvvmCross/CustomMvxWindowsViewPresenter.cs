using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using MvvmCross;
using MvvmCross.Platforms.Uap.Presenters;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    public class CustomMvxWindowsViewPresenter : MvxWindowsViewPresenter
    {
        public CustomMvxWindowsViewPresenter(IMvxWindowsFrame rootFrame) : base(rootFrame)
        {
        }

        public override void RegisterAttributeTypes()
        {
            base.RegisterAttributeTypes();
            AttributeTypesToActionsDictionary.Register<MvxWindowPresentationAttribute>(ShowWindow, CloseWindow);
        }

        private Task<bool> CloseWindow(IMvxViewModel viewModel, MvxWindowPresentationAttribute attribute)
        {
            return base.ClosePage(viewModel, attribute);
        }

        private Task<bool> ShowWindow(Type viewType, MvxWindowPresentationAttribute attribute,
            MvxViewModelRequest request)
        {
            try
            {
                return Task.Run(async () =>
                {
                    var requestText = GetRequestText(request);
                    var viewsContainer = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();

                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                        async () =>
                        {
                            var appWindow = await AppWindow.TryCreateAsync();
                            var appWindowContentFrame = new Frame();
                            appWindowContentFrame.Navigate(viewType, requestText);
                            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);
                            await appWindow.TryShowAsync();
                            HandleBackButtonVisibility();
                        });
                    return true;
                });
            }
            catch (Exception exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}