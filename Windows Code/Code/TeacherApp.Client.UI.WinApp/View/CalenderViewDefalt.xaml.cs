using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using TeacherApp.Client.UI.ViewModels.Main;
using TeacherApp.Client.UI.WinApp.ViewModels;
using TeacherApp.Client.UI.ViewModels.Timetable;
using Windows.UI.Xaml.Navigation;
using TeacherApp.Client.UI.WinApp.Management;
using TeacherApp.Client.UI.WinApp.Common;
using TeacherApp.Client.Shared.UI.VisualSupport;
using TeacherApp.Client.Domain.Configuration.Implementation;
using TeacherApp.Shared.Implementation;
using TeacherApp.Client.Shared.Domain.Services;
using TeacherApp.Client.UI.ViewModels.Student;
using TeacherApp.Client.Marksheet.Domain;
using TeacherApp.Client.Marksheet.UI;
using TeacherApp.Client.Shared.Domain.Factories;
using TeacherApp.Client.Domain;
using System.Collections.Generic;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TeacherApp.Client.UI.WinApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalenderViewDefalt : Page
    {
        private MainViewModel _mainViewModel;
        private TimetableViewModel _timetableViewModel;
        private CalenderViewDefaltViewModel _calenderViewDefaltViewModel;        

        public CalenderViewDefalt()
        {
            this.InitializeComponent();            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //_initCVD = (initComponent)e.Parameter;
            //_mainViewModel = _initCVD.UiFactory.GetMainViewModel();
            //_timetableViewModel = _mainViewModel.TeacherTimetableViewModel;
            //_calenderViewDefaltViewModel = new CalenderViewDefaltViewModel(_timetableViewModel);
            
        }
        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                _calenderViewDefaltViewModel.getWeekData();                
            }
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Home));
            }
        }

       
    }

    ///
    public class TeachersClass
    {
        public string Subject { get; set; }
        public string Class { get; set; }
        public string Location { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string day { get; set; }
    }
     

}
