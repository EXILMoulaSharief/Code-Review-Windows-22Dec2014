using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Threading;
using TeacherApp.Client.Domain;
using TeacherApp.Client.Domain.Configuration;
using TeacherApp.Client.Domain.Configuration.Implementation;
using TeacherApp.Client.Marksheet.Domain;
using TeacherApp.Client.Marksheet.UI;
using TeacherApp.Client.Shared.Domain.Factories;
using TeacherApp.Client.Shared.Domain.Managers;
using TeacherApp.Client.Shared.Domain.Services;
using TeacherApp.Client.Shared.UI.Services;
using TeacherApp.Client.Shared.UI.VisualSupport;
using TeacherApp.Client.UI.Cache;
using TeacherApp.Client.UI.Cache.Implementation;
using TeacherApp.Client.UI.Management;
using TeacherApp.Client.UI.ViewModels.Student;
using TeacherApp.Client.UI.WinApp;
using TeacherApp.Client.UI.WinApp.Management;
using TeacherApp.Shared.Implementation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TeacherApp.Client.UI.WinApp.Common
{
    /// <summary>
    /// 
    /// </summary>
    internal class PageManager
    {
        #region Declarations
        private UnityContainer _unityContainer;
        private Frame _rootFrame = (Frame) Window.Current.Content;

        #endregion
        #region Constructor
        public PageManager()
        {
            
        }
        #endregion

        #region Methods
       /// <summary>
       /// 
       /// </summary>
       /// <param name="unityContainer"></param>
        public void Startup(UnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
            _rootFrame.Navigate(typeof(LandingPage), _unityContainer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toPage"></param>
        public void NavigateToLandingPage()
        {
            _rootFrame.Navigate(typeof(LandingPage), _unityContainer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toPage"></param>
        public void NavigateToLoginPage()
        {
            _rootFrame.Navigate(typeof(LoginWebView), _unityContainer);
        }
        /// <summary>
        /// 
        /// </summary>
        public void NavigateToTimeTable()
        {
            _rootFrame.Navigate(typeof(TimeTable), _unityContainer);
        }
        /// <summary>
        /// 
        /// </summary>
        public void NavigateToHome()
        {
            _rootFrame.Navigate(typeof(Home), _unityContainer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toPage"></param>
        public void NavigateToAttendance()
        {
            _rootFrame.Navigate(typeof(AttendanceBar1), _unityContainer);
        }

        /// 
        /// </summary>
        /// <param name="toPage"></param>
        public void NavigateToAchievement()
        {
            _rootFrame.Navigate(typeof(Achievement), _unityContainer);
        }
        /// 
        /// </summary>
        /// <param name="toPage"></param>
        public void NavigateToBehaviour()
        {
            _rootFrame.Navigate(typeof(BehaviourIncident), _unityContainer);
        }
        public void NavigateToLate()
        {
            _rootFrame.Navigate(typeof(Late), _unityContainer);
        }
        public void NavigateToCodes()
        {
            _rootFrame.Navigate(typeof(Codes), _unityContainer);
        }
        public void NavigateToSearch()
        {
            _rootFrame.Navigate(typeof(Search), _unityContainer);
        }

       public void NavigateToAttendanceGrid()
        {
            _rootFrame.Navigate(typeof(AttendanceList), _unityContainer);
        }
       

        #endregion

    }
}