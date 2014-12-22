using TeacherApp.Client.Shared.UI.iOS.Messenger;
using TeacherApp.Client.UI.iOS.Visuals.GroupBrowser.GroupBrowserSearch;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class SwitchToSearchModeMessage : Message
    {
        public SwitchToSearchModeMessage(SearchMode searchMode)
        {
            SearchMode = searchMode;
        }

        public SearchMode SearchMode { get; private set; }
    }
}