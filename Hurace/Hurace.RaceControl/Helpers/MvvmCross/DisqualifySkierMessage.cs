using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurace.Domain;
using MvvmCross.Plugin.Messenger;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    class DisqualifySkierMessage : MvxMessage
    {
        public DisqualifySkierMessage(object sender, Run run) : base(sender)
        {
            Run = run;
        }

        public Run Run { get; set; }
    }
}
