using System;
using TeacherApp.Client.UI.Management;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TeacherApp.Client.UI.WinApp.Common;

namespace TeacherApp.Client.UI.WinApp.Management
{
    public class NavigationManager : INavigationManager
    {
        private event EventHandler<NavigationHelper> _navigate;

        public event EventHandler<NavigationHelper> Navigate
        {
            add { _navigate += value; }
            remove { _navigate -= value; }
        }
        //Navigation for iOS commented
        
        public void NavigateTo(NavigationDestination navigationDestination)
        {
            var navigate = _navigate;
            //Home mypage = new Home();
            if(navigate != null)
            {
            //initComponent _initCVD = new initComponent();
            //if (navigationDestination.ToString() == "Main") { }
               // navigate.Invoke(this, new NavigateEventArgs(navigationDestination));
              //navigate.Invoke(this, new NavigationHelper(mypage ));
               
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePageType"></param>
        public void NavigatePage(Type sourcePageType)
        {
            ((Frame)Window.Current.Content).Navigate(sourcePageType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePageType"></param>
        /// <param name="parameter"></param>
        public void NavigatePage(Type sourcePageType, object parameter)
        {
            ((Frame)Window.Current.Content).Navigate(sourcePageType, parameter);
        }
        /// <summary>
        /// 
        /// </summary>
        public void GoBack()
        {
            ((Frame)Window.Current.Content).GoBack();
        }

        event EventHandler<NavigateEventArgs> INavigationManager.Navigate
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
}