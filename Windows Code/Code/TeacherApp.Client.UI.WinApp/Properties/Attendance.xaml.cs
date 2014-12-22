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

        private void Image_Tapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(StudentDetails));
            }
        }
        private void Image_DoubleTapped_1(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Search));
            }
        }

        private void Image_DoubleTapped_2(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(CalenderViewDefalt));
            }
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

    }
    /// <summary>
    /// The converter class used to display images
    /// </summary>
    public class ImageConverter : IValueConverter
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
