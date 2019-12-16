using System;
using Hurace.Timer;

namespace Hurace.Simulator
{
    public class SimulatorRaceClock : IRaceClock
    {
        public event TimingTriggeredHandler TimingTriggered;
    }
}