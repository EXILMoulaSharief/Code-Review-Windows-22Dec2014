using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.BulkAccess;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TeacherApp.Client.UI.WinApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Attendance : Page
    {
        public Attendance()
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
                var pictureQuery = KnownFolders.PicturesLibrary.CreateFileQueryWithOptions(pictureQueryOptions);                //
                var picturesInformation = new FileInformationFactory(pictureQuery, ThumbnailMode.PicturesView);
                picturesSource.Source = picturesInformation.GetVirtualizedFilesVector();
                TopAppBar.IsOpen = true;
                BottomAppBar.IsOpen = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Header Events
        private void AppHeaderImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Search));
            }
        }

        private void HeaderImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(CalenderViewDefalt));
            }
        }
      
        private void AppBarButton_Click(object sender, RoutedEvent e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }
        private void LateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Late));
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

        private void CodesButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Codes));
            }
        }

        private void AcheivementButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Achievement));
            }
        }

        private void BehaviourButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BehaviourIncident));
            }
        }

        private void BackImage_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(CalenderViewDefalt));
            }
        }
      
        private void StudentImage_Tapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(StudentDetails));
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
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
        private void AppSearchButton_Click(object sender, RoutedEventArgs e)
        {
            logincontrol1.IsOpen = true;  
        }
        
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceList));
            }
        }
        #endregion 

        private void AchievementButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Achievement));
        }

        private void SignOut_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void closeButtonClick(object sender, TappedRoutedEventArgs e)
        {

        }

    }
    /// <summary>
    /// The converter class used to display images
    /// </summary>
    class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
            {
                var img = (IRandomAccessStream)value;
                var picture = new BitmapImage();
                picture.SetSource(img);
                return picture;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }

    }

}
