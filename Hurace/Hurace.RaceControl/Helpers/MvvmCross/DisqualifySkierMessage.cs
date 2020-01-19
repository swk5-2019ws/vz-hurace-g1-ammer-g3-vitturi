using Hurace.Domain;
using MvvmCross.Plugin.Messenger;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    internal class DisqualifySkierMessage : MvxMessage
    {
        public DisqualifySkierMessage(object sender, Run run) : base(sender)
        {
            Run = run;
        }

        public Run Run { get; set; }
    }
}