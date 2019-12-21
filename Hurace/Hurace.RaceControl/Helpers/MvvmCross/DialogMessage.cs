using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;

namespace Hurace.RaceControl.Helpers
{
    class DialogMessage: MvxMessage
    {
        public DialogMessage(object sender, DialogEvent dialogEvent) : base(sender)
        {
            DialogEvent = dialogEvent;
        }

        public DialogEvent DialogEvent { get; set; }
    }

    public enum DialogEvent
    {
        RaceEditSuccess
    }
}
