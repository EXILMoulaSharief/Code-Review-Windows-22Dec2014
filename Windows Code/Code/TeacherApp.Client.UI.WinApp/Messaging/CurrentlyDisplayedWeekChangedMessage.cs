using TeacherApp.Client.Shared.UI.iOS.Messenger;
using TeacherApp.Client.UI.ViewModels.Timetable;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class CurrentlyDisplayedWeekChangedMessage : Message
    {
        public CurrentlyDisplayedWeekChangedMessage(WeekViewModel weekViewModel)
        {
            CurrentWeek = weekViewModel;
        }

        public WeekViewModel CurrentWeek { get; private set; }
    }
}