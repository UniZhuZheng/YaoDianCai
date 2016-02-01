using System;
using System.Diagnostics;


namespace Uni.Core.Database.DebugProxy
{
    class ActionTimer : IDisposable
    {
        private readonly Stopwatch stopwatch;
        private readonly Action<TimeSpan> onStop;

        private ActionTimer(Action<TimeSpan> onStop)
        {
            this.onStop = onStop;
            stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            try
            {
                onStop(stopwatch.Elapsed);
            }
            catch {}
        }

        public static IDisposable Begin(Action<TimeSpan> onStop)
        {
            return new ActionTimer(onStop);
        }
    }
}
