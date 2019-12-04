using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();

            RegisterAppStart<ViewModels.NavigationRootViewModel>();
        }
    }
}
