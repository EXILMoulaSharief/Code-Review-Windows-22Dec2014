using TeacherApp.Client.Shared.UI.iOS.Messenger;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class TeacherPickerSelectionCancelledMessage : Message
    {
        public TeacherPickerSelectionCancelledMessage(int cancelationTeacherId, string cancelationTeacherName, int restoreTeacherId, string restoreTeacherName)
        {
            CancelationTeacherName = cancelationTeacherName;
            RestoreTeacherId = restoreTeacherId;
            RestoreTeacherName = restoreTeacherName;
            CancelationTeacherId = cancelationTeacherId;
        }

        public int CancelationTeacherId { get; private set; }

        public string CancelationTeacherName { get; private set; }
        public int RestoreTeacherId { get; set; }
        public string RestoreTeacherName { get; set; }
    }
}