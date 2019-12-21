using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugins.Messenger;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    class DialogService: IDialogService
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
