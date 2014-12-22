using TeacherApp.Client.Shared.UI.iOS.Messenger;
using TeacherApp.Client.UI.ViewModels.Timetable;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class DayEventTappedMessage : Message
    {
        private readonly DayViewModel _dayViewModel;

        public DayEventTappedMessage(DayViewModel dayViewModel)
        {
            _dayViewModel = dayViewModel;
        }

        public DayViewModel DayViewModel
        {
            get { return _dayViewModel; }
        }
    }
}