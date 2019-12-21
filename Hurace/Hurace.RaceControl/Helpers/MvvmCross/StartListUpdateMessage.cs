using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    class StartListUpdateMessage: MvxMessage
    {
        public StartListUpdateMessage(object sender, int startPosition) : base(sender)
        {
            StartPosition = startPosition;
        }

        public int StartPosition { get; set; }
    }
}
