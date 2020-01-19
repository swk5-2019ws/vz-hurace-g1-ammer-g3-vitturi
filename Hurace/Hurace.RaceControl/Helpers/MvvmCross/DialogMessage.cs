using MvvmCross.Plugin.Messenger;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    internal class DialogMessage : MvxMessage
    {
        public DialogMessage(object sender, DialogEvent dialogEvent) : base(sender)
        {
            DialogEvent = dialogEvent;
        }

        public DialogEvent DialogEvent { get; set; }
    }

    public enum DialogEvent
    {
        RaceEditSuccess,
        NoRaceInProgress
    }
}