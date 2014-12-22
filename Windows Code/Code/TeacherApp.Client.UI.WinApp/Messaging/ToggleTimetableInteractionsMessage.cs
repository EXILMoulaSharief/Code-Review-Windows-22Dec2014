using TeacherApp.Client.Shared.UI.iOS.Messenger;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class ToggleTimetableInteractionsMessage : Message
    {
        public ToggleTimetableInteractionsMessage(bool userInteractionEnabled)
        {
            UserInteractionEnabled = userInteractionEnabled;
        }

        public bool UserInteractionEnabled { get; private set; }
    }
}