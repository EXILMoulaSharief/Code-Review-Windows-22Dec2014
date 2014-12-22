using System;
using MonoTouch.Foundation;

namespace TeacherApp.Client.UI.iOS.Extensions
{
    public static class DateExtensions
    {
        public static NSDate ToNSDate(this DateTime dateTime)
        {
            DateTime universalTime = dateTime.ToUniversalTime();
            return NSDate.FromTimeIntervalSinceReferenceDate((universalTime - (new DateTime(2001, 1, 1, 0, 0, 0))).TotalSeconds);
        }

        public static DateTime ToDateTime(this NSDate nsDate)
        {
            return (new DateTime(2001, 1, 1, 0, 0, 0)).AddSeconds(nsDate.SecondsSinceReferenceDate);
        }
    }
}