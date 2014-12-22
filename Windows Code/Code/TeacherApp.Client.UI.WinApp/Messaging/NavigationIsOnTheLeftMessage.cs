using TeacherApp.Client.Shared.UI.iOS.Messenger;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class NavigationIsOnTheLeftMessage : Message
    {
        public NavigationIsOnTheLeftMessage(bool isOnTheLeft)
        {
            IsOnTheLeft = isOnTheLeft;
        }

        public bool IsOnTheLeft { get; private set; }
    }
}