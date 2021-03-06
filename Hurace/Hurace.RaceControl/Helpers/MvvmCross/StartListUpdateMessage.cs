﻿using MvvmCross.Plugin.Messenger;

namespace Hurace.RaceControl.Helpers.MvvmCross
{
    internal class StartListUpdateMessage : MvxMessage
    {
        public StartListUpdateMessage(object sender, int startPosition) : base(sender)
        {
            StartPosition = startPosition;
        }

        public int StartPosition { get; set; }
    }
}