using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using TeacherApp.Client.Domain;
using TeacherApp.Client.Shared.UI.VisualSupport;
using TeacherApp.Client.UI.Cache;
using TeacherApp.Client.UI.ViewModels.GroupBrowser;
using TeacherApp.Client.UI.WinApp.Common;
using TeacherApp.Client.UI.WinApp.Configuration;
using TeacherApp.Client.UI.WinApp.Management;
using TeacherApp.Client.UI.WinApp.ViewModels;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TeacherApp.Client.UI.WinApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AttendanceBar1 : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private List<IDisposable> _propertyBindings = new List<IDisposable>();       
        List<string> selected = new List<string>();      
        private UnityContainer _unityContainer;
        PageManager _pageManager;
        private GroupBrowserViewModel _groupBrowserViewModel;
        private GroupBrowserCollectionViewModel group;
        private List<ImageDetail<BitmapImage>> imageList;
        private List<StudentsImageDetails> finalList;
        int count = 15;
        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
        public ObservableCollection<StudentsImageDetails> students = new ObservableCollection<StudentsImageDetails>();
        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public AttendanceBar1()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            Window.Current.SizeChanged += Current_SizeChanged;
            var bounds = Window.Current.Bounds;
            ActivityCanvas.Width = bounds.Width;
            ActivityCanvas.Height = bounds.Height;          
            ActivityCanvas.Children.Add(ActivityIndicator.ProgressActivityIndicator);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ActivityIndicator.ShowProgress();
            _unityContainer = (UnityContainer)e.Parameter;
            _pageManager = _unityContainer.Resolve<PageManager>();
            signOut.SetDependencies(_unityContainer);            
            var uiFactory = _unityContainer.Resolve<IUIFactory>();
            _groupBrowserViewModel = uiFactory.GetGroupBrowserViewModel();
             group = new GroupBrowserCollectionViewModel(_groupBrowserViewModel, GlobalFields.selectedModel,_unityContainer);   
            _propertyBindings.Add(_groupBrowserViewModel.BindViewModelPropertyChangedTo(ViewModel.IsLoadingPropertyName, GroupBrowserViewIsLoading));
            imageList = new List<ImageDetail<BitmapImage>>();
            group.Callback += new TeacherApp.Client.UI.WinApp.ViewModels.GroupBrowserCollectionViewModel.CallbackEventHandler(callback);
            finalList = new List<StudentsImageDetails>();
            //LogoutViewModel logout = new LogoutViewModel(UserModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagedeatails"></param>
        public void callback(ImageDetail<BitmapImage> imagedeatails)
        {
            imageList.Add(imagedeatails);
            // removed = since the index starts from zero by prabakaran.N
            for(int i=0; i< _groupBrowserViewModel.StudentsInContext.Count ; i++)
            {
                if( _groupBrowserViewModel.StudentsInContext[i].Id.Equals(imagedeatails.PersonId))
                {
                    finalList.Add(new StudentsImageDetails(_groupBrowserViewModel.StudentsInContext[i].Forename, _groupBrowserViewModel.StudentsInContext[i].Surname, imagedeatails.Image));
                    students.Add(new StudentsImageDetails(_groupBrowserViewModel.StudentsInContext[i].Forename, _groupBrowserViewModel.StudentsInContext[i].Surname));
                    createGrid(i, _groupBrowserViewModel.StudentsInContext[i].Forename, _groupBrowserViewModel.StudentsInContext[i].Surname, imagedeatails.Image);          
                    
                }

            }
         
            if(imageList.Count.Equals(_groupBrowserViewModel.StudentsInContext.Count))
            {
                //ActivityCanvas.Visibility = Visibility.Collapsed;
                //AllClassPanel.Visibility = Visibility.Visible;
                //AllClassGrid.ItemsSource = students; 
                ActivityCanvas.Visibility = Visibility.Collapsed;
                
            }
      
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="surname"></param>
        /// <param name="forename"></param>
        /// <param name="studentImage"></param>
        /// <param name="type"></param>
        public void createGrid(int i, string surname, string forename, BitmapImage studentImage)
        {
            StackPanel outerstack = new StackPanel();
            outerstack.Width = 160;
            StackPanel stack = new StackPanel();
            stack.Height = 210;
            stack.Background = GlobalFields.ColorToBrush("#ffffff"); ;           
                Image image = new Image();
                image.Width = 150;
                image.Height = 160;
                image.Stretch = Stretch.Fill;
                if (image != null)
                {
                    image.Source = (BitmapImage)studentImage;

                }      
                TextBlock txtblock1 = new TextBlock();
                TextBlock txtblock2 = new TextBlock();
                txtblock1.Foreground = GlobalFields.ColorToBrush("#117BBD");
                txtblock2.Foreground = GlobalFields.ColorToBrush("#117BBD");
                txtblock1.FontSize = 20;
                txtblock2.FontSize = 18;
                txtblock1.Text = surname;
                txtblock2.Text = forename;
                txtblock1.Margin = new Thickness(10, 1, 0, 0);
                txtblock2.Margin = new Thickness(10, 1, 0, 0);
                stack.Children.Add(image);
                stack.Children.Add(txtblock1);
                stack.Children.Add(txtblock2);               
            if (i < 15)
            {
                Grid.SetRow(outerstack, 0);
                Grid.SetColumn(outerstack, i);
                outerstack.Margin = new Thickness(0, 0, 1, 0);
            }
            else
            {
                Grid.SetRow(outerstack, 1);
                Grid.SetColumn(outerstack, i - count);
                outerstack.Margin = new Thickness(0, 2, 1, 0);

            }
           
            outerstack.Children.Add(stack);
            AllClassGrid.Children.Add(outerstack);
        }
        

        
        /// <summary>
        /// 
        /// </summary>
        public void GroupBrowserViewIsLoading()
        {
            if (_groupBrowserViewModel.IsLoading)
            {
                ActivityCanvas.Visibility = Visibility.Visible;                
            }
            else
            {
                //ActivityCanvas.Visibility = Visibility.Collapsed;
                //AllClassPanel.Visibility = Visibility.Visible;
                //AllClassGrid.DataContext = _groupBrowserViewModel.StudentsInContext;
                
               
            }
        }
        public void group_Callback()
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

            //TopAppBar.IsOpen = true;
            //BottomAppBar.IsOpen = true;
            
        }       
        #region Header Events

       
        private void PresentButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void AbsentButton_Click(object sender, RoutedEventArgs e)
        {
          
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Search));
            }
        }
        private void HeaderImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
            _pageManager.NavigateToTimeTable();
        }
        private void AppSearchButton_Click(object sender, RoutedEventArgs e)
        {
            logincontrol1.IsOpen = true;  
        }
        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            //foreach (UIElement item in AppBarStackPanel.Children)
            //{
            //    var barElement = item as ICommandBarElement;
            //    if (barElement != null)
            //    {
            //        barElement.IsCompact = e.Size.Width < 700;
            //    }
            //}
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

      
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //navigationHelper.OnNavigatedFrom(e);
        }

       

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
            _pageManager.NavigateToAttendanceGrid();
        }

        #endregion

        private void PresentGridSelectionChnaged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GridView lv = sender as GridView;
                TopAppBar.IsSticky = true;
                BottomAppBar.IsSticky = true;
                if (lv.SelectedItem != null)
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
        #endregion

        private void closeButtonClick(object sender, TappedRoutedEventArgs e)
        {
            logincontrol1.IsOpen = false;
        }

        private void PresentGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //TopAppBar.IsOpen = true;
            //BottomAppBar.IsOpen = true;
        }

        private void BackImage_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void AchievementButton_Click(object sender, RoutedEventArgs e)
        {            
            _pageManager.NavigateToAchievement();
        }

        private void BehaviourButton_Click(object sender, RoutedEventArgs e)
        {
            _pageManager.NavigateToBehaviour();
        }
        private void LateButton_Click(object sender, RoutedEventArgs e)
        {
            _pageManager.NavigateToLate();
        }

        private void CodesButton_Click(object sender, RoutedEventArgs e)
        {
            _pageManager.NavigateToCodes();
        }

        private void SignOut_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
        }
    }
    public class StudentsImageDetails
    {
        string Forename { get; set; }
        string Surname { get; set; }
        BitmapImage studentImage { get; set; }

        public StudentsImageDetails(string _forename,string _surname,BitmapImage _image)
        {
            Forename = _forename;
            Surname = _surname;
            studentImage = _image;
        }
        public StudentsImageDetails(string _forename, string _surname)
        {
            Forename = _forename;
            Surname = _surname;
            //studentImage = _image;
        }
    }
}
