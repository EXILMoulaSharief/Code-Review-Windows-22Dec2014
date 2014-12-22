using TeacherApp.Client.Shared.UI.iOS.Messenger;
using TeacherApp.Client.UI.ViewModels.Timetable;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class CurrentyDisplayedStudentTimetableDayChangedMessage : Message
    {
        public CurrentyDisplayedStudentTimetableDayChangedMessage(DayViewModel dayViewModel)
        {
            CurrentDay = dayViewModel;
        }

        public DayViewModel CurrentDay { get; private set; }
    }
}