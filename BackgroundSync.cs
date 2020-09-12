using System;
using System.Diagnostics;
using System.Threading;

namespace SyncMonitorBrightness
{
    public class BackgroundSync 
    {
        public void SyncBrightness()
        {
            var pmbc = new PhysicalMonitorBrightnessController();

            byte prevBuiltInBrightness = 0;

            var refreshDelay = TimeSpan.FromMilliseconds(500);
            var forceResyncTimeSpan = TimeSpan.FromSeconds(30);

            var sw = Stopwatch.StartNew();
            while (true)
            {
                Thread.Sleep(refreshDelay);
                var builtInBrightness = BuiltInMonitorBrightnessController.Get();

                if (prevBuiltInBrightness == builtInBrightness && sw.Elapsed < forceResyncTimeSpan)
                {
                    continue;
                }

                pmbc.Set((uint)(builtInBrightness));
                sw.Restart();
                prevBuiltInBrightness = builtInBrightness;

                var additionalMonitorsBrightness = pmbc.Get();

                BrightnessUpdated?.Invoke(builtInBrightness, additionalMonitorsBrightness);
            }
        }

        public event Action<byte, int> BrightnessUpdated;
    }
}
