using TeacherApp.Client.Shared.UI.iOS.Messenger;
using TeacherApp.Client.UI.ViewModels.Timetable;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class ChangeGroupBrowserToSelectedEventMessage : Message
    {
        private readonly EventViewModel _eventViewModel;

        public ChangeGroupBrowserToSelectedEventMessage(EventViewModel eventViewModel)
        {
            _eventViewModel = eventViewModel;
        }

        public EventViewModel SelectedEvent
        {
            get { return _eventViewModel; }
        }
    }
}