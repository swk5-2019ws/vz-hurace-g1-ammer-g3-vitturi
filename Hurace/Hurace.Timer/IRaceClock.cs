using System;

namespace Hurace.Timer
{
    public delegate void TimingTriggeredHandler(int sensorId, DateTime time);

    public interface IRaceClock
    {
        event TimingTriggeredHandler TimingTriggered;
    }
}