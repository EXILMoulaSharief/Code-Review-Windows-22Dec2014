using MonoTouch.Foundation;
using TeacherApp.Client.Shared.UI.iOS.Messenger;
using TeacherApp.Client.UI.iOS.Controllers.StudentDetail;

namespace TeacherApp.Client.UI.iOS.Messaging
{
    internal class ShowAchievementBehaviourSummaryMessage : Message
    {
        public ShowAchievementBehaviourSummaryMessage(StudentConductCellInfo studentConductCellInfo,
                                                      NSIndexPath indexOfIndividualConductCell,
                                                      float xPositionOfTappedConductCell)
        {
            StudentConductCellInfo = studentConductCellInfo;
            IndexPathOfIndividualConductCell = indexOfIndividualConductCell;
            XPositionOfTappedConductCell = xPositionOfTappedConductCell;
        }

        public StudentConductCellInfo StudentConductCellInfo { get; private set; }
        public NSIndexPath IndexPathOfIndividualConductCell { get; private set; }
        public float XPositionOfTappedConductCell { get; private set; }
    }
}