using TeacherApp.Client.Shared.UI.iOS.Messenger;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class TeacherPickerSelectionChangedMessage : Message
    {
        public TeacherPickerSelectionChangedMessage(int teacherId, string teacherName)
        {
            TeacherName = teacherName;
            TeacherId = teacherId;
        }

        public int TeacherId { get; private set; }

        public string TeacherName { get; private set; }
    }
}