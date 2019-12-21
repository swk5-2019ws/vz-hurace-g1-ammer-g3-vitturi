using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    public interface IDialogService
    {
        void Alert(DialogEvent dialogEvent);
    }
}
