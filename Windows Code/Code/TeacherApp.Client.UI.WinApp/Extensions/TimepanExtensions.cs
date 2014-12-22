using System;

namespace TeacherApp.Client.UI.iOS.Extensions
{
    internal static class TimespanExtensions
    {
        private static double _totalTimeLogged;

        public static void GetTime(this TimeSpan timespan, string methodName)
        {
            _totalTimeLogged += timespan.TotalSeconds;
            Console.WriteLine("{0} took \t{1}\t (Seconds)", methodName, timespan.TotalSeconds.ToString("##.################"));
        }

        public static void PrintTotalTimeElapsed()
        {
            Console.WriteLine("Total time logged {0} (Seconds)", _totalTimeLogged);
            _totalTimeLogged = 0;
        }
    }
}