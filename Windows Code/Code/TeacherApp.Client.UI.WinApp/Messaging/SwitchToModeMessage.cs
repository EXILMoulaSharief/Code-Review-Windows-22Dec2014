using TeacherApp.Client.Shared.UI.iOS.Messenger;
using TeacherApp.Client.UI.iOS.Controllers.GroupBrowser;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class SwitchToModeMessage : Message
    {
        public SwitchToModeMessage(GroupBrowserDisplayMode groupBrowserDisplayMode)
        {
            GroupBrowserDisplayMode = groupBrowserDisplayMode;
        }

        public GroupBrowserDisplayMode GroupBrowserDisplayMode { get; private set; }
    }
}