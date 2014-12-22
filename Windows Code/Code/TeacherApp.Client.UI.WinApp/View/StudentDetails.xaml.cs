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
    public sealed partial class StudentDetails : Page
    {
        public StudentDetails()
        {
            this.InitializeComponent();
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

        private void Image_Tapped_1(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }
        private void HeaderImage_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }
        private void BackImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void OtherInformationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
