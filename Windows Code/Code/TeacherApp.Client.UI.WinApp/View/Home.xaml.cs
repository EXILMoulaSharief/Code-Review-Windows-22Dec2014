using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TeacherApp.Client.UI.WinApp.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TeacherApp.Client.UI.WinApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {

        private UnityContainer _unityContainer;
        private PageManager pageManager;
        public Home()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //_initHome = (initComponent)e.Parameter;
            _unityContainer = (UnityContainer)e.Parameter;
            signOut.SetDependencies(_unityContainer);
            pageManager = _unityContainer.Resolve<PageManager>();
            this.Frame.Navigating += Frame_Navigating;
        }

        private void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StackPanel_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(CalenderViewDefalt));
            }
        }

        private void StackPanel_Tapped_3(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Login));
                //this.Frame.Navigate(typeof(CalenderViewDefalt), _initHome);
            }
        }

        private void GroupStackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            pageManager.NavigateToSearch();
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                //this.Frame.Navigate(typeof(Login), _initHome);
                PageManager pageManager = _unityContainer.Resolve<PageManager>();
                pageManager.NavigateToAttendance();

            }
        }

        private void BackImage_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void HeaderImage_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void SignOut_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
}
}
