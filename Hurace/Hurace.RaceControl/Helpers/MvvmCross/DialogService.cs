using MvvmCross.Plugin.Messenger;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    internal class DialogService : IDialogService
    {
        private readonly IMvxMessenger _messenger;

        public DialogService(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        public void Alert(DialogEvent dialogEvent)
        {
            _messenger.Publish(new DialogMessage(this, dialogEvent));
        }
    }
}