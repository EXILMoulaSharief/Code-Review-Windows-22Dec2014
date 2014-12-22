using Microsoft.Practices.Unity;
using System;
using TeacherApp.Client.UI.WinApp.Common;
using Windows.Storage;
using Windows.Storage.BulkAccess;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TeacherApp.Client.UI.WinApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Codes : Page
    {
        private UnityContainer _unityContainer;
        PageManager pageManager;
        public Codes()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _unityContainer = (UnityContainer)e.Parameter;
            signOut.SetDependencies(_unityContainer);
            pageManager = _unityContainer.Resolve<PageManager>();
        }
        /// <summary>
        /// The below method queries to the 
        /// Picture Library with all its sub folders and
        /// read all pictures
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Define thr query to iterate thriugh all the subfolders 
                var pictureQueryOptions = new QueryOptions();
                //Read through all the subfolders.
                pictureQueryOptions.FolderDepth = FolderDepth.Deep;

                // var folder = StorageFolder.GetFolderFromPathAsync("C:\Users\\sourabh.b\\Downloads\\winrt-known-folders-master\\winrt-known-folders-master\\Store_CS_PictureViewer\Picture");
                //Apply the query on the PicturesLibrary
                var pictureQuery = KnownFolders.PicturesLibrary.CreateFileQueryWithOptions(pictureQueryOptions);

                //
                var picturesInformation = new FileInformationFactory(pictureQuery, ThumbnailMode.PicturesView);
                picturesSource.Source = picturesInformation.GetVirtualizedFilesVector();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
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
        private void AppSearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SP_Search.Visibility.Equals(Visibility.Collapsed))
            {
                SP_Search.Visibility = Visibility.Visible;
                btnSearch.Content = "Add";
            }
            else
            {
                SP_Search.Visibility = Visibility.Collapsed;
                btnSearch.Content = "Add Students";

            }
        }
        private void SaveCode_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SignOut_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        
      
    }
}
