using Hurace.Timer;
using System;

namespace Hurace.Simulator
{
    public class SimulatorRaceClock : IRaceClock
    {
        public event TimingTriggeredHandler TimingTriggered;

        public void SendImpulse(int sensorId)
        {
            TimingTriggered?.Invoke(sensorId, DateTime.UtcNow);
        }
    }
}