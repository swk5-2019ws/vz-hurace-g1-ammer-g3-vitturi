using MvvmCross.Plugins.Messenger;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    class DialogService : IDialogService
    {
        private IMvxMessenger _messenger;

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
