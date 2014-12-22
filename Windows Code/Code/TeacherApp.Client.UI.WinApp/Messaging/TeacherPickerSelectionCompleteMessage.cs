using TeacherApp.Client.Shared.UI.iOS.Messenger;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class TeacherPickerSelectionCompleteMessage : Message
    {
        public TeacherPickerSelectionCompleteMessage(int teacherId)
        {
            TeacherId = teacherId;
        }

        public int TeacherId { get; private set; }
    }
}