using System;
using MonoTouch.UIKit;

namespace TeacherApp.Client.UI.iOS.Extensions
{
    public static class UITableViewCellExtensions
    {
        public static void ApplyTextLabelStyleForCanExecute(this UITableViewCell tableViewCell, Func<bool> canExecute)
        {
            if(canExecute != null)
            {
                if(canExecute.Invoke())
                {
                    tableViewCell.TextLabel.TextColor = UIColor.White;
                }
                else
                {
                    tableViewCell.TextLabel.TextColor = UIColor.Gray.ColorWithAlpha(0.90f);
                }
            }
        }
    }
}