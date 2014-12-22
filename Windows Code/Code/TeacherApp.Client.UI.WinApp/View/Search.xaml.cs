using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
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
    public sealed partial class Search : Page
    {
        private UnityContainer _unityContainer;
        PageManager pageManager;
        public Search()
        {
            this.InitializeComponent();
        }
        public XDocument xdoc;
        IEnumerable<flipimage> images;
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PhotoGrid.DataContext = groupdetails();
            _unityContainer = (UnityContainer)e.Parameter;
            signOut.SetDependencies(_unityContainer);
            pageManager = _unityContainer.Resolve<PageManager>();
        }
        public IEnumerable<flipimage> groupdetails()
        {
            xdoc = XDocument.Load("XMLFile3.xml");
            images = from img in xdoc.Descendants("Image") select new flipimage(img.Element("Name").Value, img.Element("ImageUrl").Value, img.Element("Picture").Value, null, img.Element("Functionality").Value);
            //var finalvalue =  from values in images where images
            return images;
        }
      

        #region Header Events
        private void AchievementButton_Click(object sender, RoutedEventArgs e)
        {
            pageManager.NavigateToAchievement();
        }

        private void BehaviourButton_Click(object sender, RoutedEventArgs e)
        {
            pageManager.NavigateToBehaviour();
        }
        private void LateButton_Click(object sender, RoutedEventArgs e)
        {
            pageManager.NavigateToLate();
        }

        private void CodesButton_Click(object sender, RoutedEventArgs e)
        {
            pageManager.NavigateToCodes();
        }

        private void HeaderImage_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }
       

        private void PresentButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Attendance));
            }
        }

        private void AbsentButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Attendance));
            }
        }
       
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            pageManager.NavigateToSearch();
        }

        private void BackImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }

        #endregion

       
    }
}
