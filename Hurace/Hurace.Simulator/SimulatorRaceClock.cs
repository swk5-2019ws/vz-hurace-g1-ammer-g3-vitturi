using System;
using Hurace.Timer;

namespace Hurace.Simulator
{
    public class SimulatorRaceClock : IRaceClock
    {
        public event TimingTriggeredHandler TimingTriggered;

        public void SendImpulse(int sensorId)
        {
            TimingTriggered?.Invoke(sensorId, DateTime.Now);
        }
    }
}