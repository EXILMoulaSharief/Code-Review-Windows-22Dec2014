using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TeacherApp.Client.UI.WinApp.Common
{
    /// <summary>
    /// Class for Show and Hide Process Ring
    /// </summary>
    public static class ActivityIndicator
    {
        #region Declarations
        /// <summary>
        /// Progress Bar 
        /// </summary>
        public static ProgressRing ProgressActivityIndicator;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializing ProgressRing 
        /// </summary>
        static ActivityIndicator()
        {           
                ProgressActivityIndicator = new ProgressRing();
                ProgressActivityIndicator.Height = 100;
                ProgressActivityIndicator.Width = 80;
                ProgressActivityIndicator.Foreground = new SolidColorBrush(Colors.CornflowerBlue);
                var bounds = Window.Current.Bounds;
                //ProgressActivityIndicator.Margin = new Windows.UI.Xaml.Thickness(bounds.Width % 2, bounds.Height% 2, bounds.Width % 2, bounds.Height % 2);  
                ProgressActivityIndicator.Margin = new Windows.UI.Xaml.Thickness(bounds.Width/2-100, bounds.Height/2-100, 0, 0);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Return ProgressRing
        /// </summary>
        /// <returns></returns>
        public static ProgressRing GetProcess()
        {
            
            return ProgressActivityIndicator;
        }
        /// <summary>
        /// Displaying Activity Ring
        /// </summary>
        public static void ShowProgress()
        {
            ProgressActivityIndicator.IsActive = true;
        }
        /// <summary>
        /// Hiding Activity Ring
        /// </summary>
        public static void CloseProgress()
        {
            ProgressActivityIndicator.IsActive = false;
        }
       #endregion 
    }
}
