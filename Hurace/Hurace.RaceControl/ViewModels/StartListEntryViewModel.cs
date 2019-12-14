using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hurace.Domain;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class StartListEntryViewModel: MvxViewModel
    {
        private int _startPosition;
        private Skier _skier;

        public int StartPosition
        {
            get => _startPosition;
            set => SetProperty(ref _startPosition, value);
        }

        public Skier Skier
        {
            get => _skier;
            set => SetProperty(ref _skier, value);
        }

        public ICommand DeleteCommand { get; set; }
    }
}
