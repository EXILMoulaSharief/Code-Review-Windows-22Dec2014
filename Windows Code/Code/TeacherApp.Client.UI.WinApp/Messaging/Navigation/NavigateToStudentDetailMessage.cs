using TeacherApp.Client.Shared.UI.iOS.Messenger;
using TeacherApp.Client.UI.ViewModels.Student;

namespace TeacherApp.Client.UI.iOS.Messaging.Navigation
{
    internal class NavigateToStudentDetailMessage : Message
    {
        internal NavigateToStudentDetailMessage(GroupBrowserStudentViewModel studentViewModel)
        {
            Student = studentViewModel;
        }

        public GroupBrowserStudentViewModel Student { get; private set; }
    }
}