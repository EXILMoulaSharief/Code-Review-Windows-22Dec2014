using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TeacherApp.Client.Shared.UI.VisualSupport;
using TeacherApp.Client.UI.ViewModels.GroupBrowser;
using TeacherApp.Client.UI.WinApp.Common;
using TeacherApp.Client.UI.WinApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AttendanceList : Page
    {
        PageManager pageManager;
        public AttendanceList()
        {
           
            this.InitializeComponent();
            var bounds = Window.Current.Bounds;
            ActivityCanvas.Width = bounds.Width;
            ActivityCanvas.Height = bounds.Height;
            Canvas.SetZIndex(ActivityCanvas, 1);
            ActivityCanvas.Children.Add(ActivityIndicator.ProgressActivityIndicator);
        }

        public XDocument xdoc;
        IEnumerable<flipimage> images;
        private UnityContainer _unityContainer;
        private GroupBrowserViewModel _groupBrowserViewModel;
        private GroupBrowserCollectionViewModel group;
        private List<IDisposable> _propertyBindings = new List<IDisposable>();

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            IEnumerable<flipimage> totalcount;
            List<string> totalvalues = new List<string>();
            Dictionary<string, string> dictpics = new Dictionary<string, string>();
            dictpics.Add("PRESENT", @"ms-appx:///Assets/IconPresent.png");
            dictpics.Add("ABSENT", @"ms-appx:///Assets/IconAbsent.png");
            dictpics.Add("LATE", @"ms-appx:///Assets/IconLate.png");
            dictpics.Add("", @"ms-appx:///Assets/IconUnassigned.png");
            totalvalues.Add("");
            totalvalues.Add("PRESENT");
            totalvalues.Add("ABSENT");
            totalvalues.Add("LATE");
            int count = 0;
            string image = string.Empty;
            ActivityIndicator.ShowProgress();

            //foreach (string str in totalvalues)
            //{
            //    totalcount = searchdetails(str);
            //    ImageSource imgSource = new BitmapImage(new Uri(dictpics[str], UriKind.Absolute));
            //    if (totalcount.ToArray().Length !=0) 
            //    {
            //        if (count == 0)
            //        {
            //            string name = string.Empty;
            //            if (str == "") { name = "CLASS"; }
            //            else name = str;                       
            //            AllClassPanel.Visibility = Visibility.Visible;
            //            AllClassGrid.DataContext = totalcount;
            //            AllClassText.Text = name;
            //            AllClassImage.Source = imgSource;
            //        }
            //        if(count ==1)
            //         {

            //                PresentPanel1.Visibility = Visibility.Visible;
            //                PresentList1.DataContext = totalcount;
            //                PresentPanelText1.Text = str;
            //                PresentImage.Source = imgSource;

            //          }
            //          if ( count == 2)
            //          {

            //                AbsentPanel.Visibility = Visibility.Visible;
            //                AbsentGrid.DataContext = totalcount;
            //                AbsentTexBlock.Text = str;
            //                AbsentImage.Source = imgSource;

            //          }
            //          if (count == 3)
            //          {

            //              LatePanel.Visibility = Visibility.Visible;
            //              LateGrid.DataContext = totalcount;
            //              LateTextBlock.Text = str;
            //              LateImage.Source = imgSource;

            //          }
            //          count = count + 1;
            //    }

            //}
          
            _unityContainer = (UnityContainer)e.Parameter;
            signOut.SetDependencies(_unityContainer);
            pageManager = _unityContainer.Resolve<PageManager>();
            var uiFactory = _unityContainer.Resolve<IUIFactory>();
            _groupBrowserViewModel = uiFactory.GetGroupBrowserViewModel();
            group = new GroupBrowserCollectionViewModel(_groupBrowserViewModel, GlobalFields.selectedModel, _unityContainer);
            _propertyBindings.Add(_groupBrowserViewModel.BindViewModelPropertyChangedTo(ViewModel.IsLoadingPropertyName, GroupBrowserViewIsLoading));
        }

        public void GroupBrowserViewIsLoading()
        {
            if (_groupBrowserViewModel.IsLoading)
            {
                ActivityCanvas.Visibility = Visibility.Visible;
            }
            else
            {
                ActivityCanvas.Visibility = Visibility.Collapsed;
                AllClassPanel.Visibility = Visibility.Visible;
                AllClassGrid.DataContext = _groupBrowserViewModel.StudentsInContext;

            }
        }

        public class StudentsImageDetails
        {
            string Forename { get; set; }
            string Surname { get; set; }
            BitmapImage studentImage { get; set; }

            public StudentsImageDetails(string _forename, string _surname, BitmapImage _image)
            {
                Forename = _forename;
                Surname = _surname;
                studentImage = _image;
            }
        }

        public IEnumerable<flipimage> searchdetails(string functionality)
        {
            xdoc = XDocument.Load("Configuration/XMLFile3.xml");
            images = from img in xdoc.Descendants("Image") where img.Element("Functionality").Value == functionality select new flipimage(img.Element("Name").Value, img.Element("ImageUrl").Value, img.Element("Picture").Value, null, img.Element("Functionality").Value);
            //var finalvalue =  from values in images where images
            return images;

        }
        public IEnumerable<flipimage> searchStudentdetails(string functionality)
        {
            xdoc = XDocument.Load("Configuration/XMLFile3.xml");
            images = from img in xdoc.Descendants("Image") select new flipimage(img.Element("Name").Value, img.Element("ImageUrl").Value, img.Element("Picture").Value, null, img.Element("Functionality").Value);
            //var finalvalue =  from values in images where images
            return images;

        }

        private void Image_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
            pageManager.NavigateToAttendance();
        }
        #region Header Events


        private void AchievementButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
            pageManager.NavigateToAchievement();
        }

        private void BehaviourButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
            pageManager.NavigateToBehaviour();
        }
        private void LateButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
            pageManager.NavigateToLate();
        }

        private void CodesButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
            pageManager.NavigateToCodes();
        }

        private void HeaderImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AttendanceBar1));
            }
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
            pageManager.NavigateToAttendance();
        }

        private void PresentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AbsentButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Attendance));
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
            Search.DataContext = searchStudentdetails(null);
            logincontrol1.IsOpen = true;
        }
        private void BackImage_Tapped(object sender, TappedRoutedEventArgs e)
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
        #endregion

        private void PresentGridSelectionChnaged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GridView lv = sender as GridView;
                TopAppBar.IsSticky = true;
                BottomAppBar.IsSticky = true;
                if (lv != null)
                {
                    //TopAppBar.IsSticky = true;
                    //flipimage obj = (flipimage)lv.SelectedItem;
                    //selected.Add(obj.Name);
                    TopAppBar.IsOpen = true;
                    //BottomAppBar.IsSticky = true;
                    BottomAppBar.IsOpen = true;
                }
                else
                {
                    TopAppBar.IsSticky = false;
                    TopAppBar.IsOpen = false;
                    BottomAppBar.IsSticky = false;
                    BottomAppBar.IsOpen = false;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void closeButtonClick(object sender, TappedRoutedEventArgs e)
        {
            logincontrol1.IsOpen = false;
        }

        private void StudentImage_Tapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(StudentDetails));
            }
        }
        private void SignOut_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            //((Image)sender).Source = new BitmapImage(new Uri("/Assets/Person.png", UriKind.Relative));

        }
    }
}
