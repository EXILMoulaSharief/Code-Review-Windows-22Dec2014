using TeacherApp.Client.Shared.UI.iOS.Messenger;
using TeacherApp.Client.UI.ViewModels.Student;

namespace TeacherApp.Client.UI.iOS.Messaging.Attendance
{
    internal class RemoveExtraNameStudentMessage : Message
    {
        public RemoveExtraNameStudentMessage(GroupBrowserStudentViewModel student)
        {
            Student = student;
        }

        public GroupBrowserStudentViewModel Student { get; private set; }
    }
}