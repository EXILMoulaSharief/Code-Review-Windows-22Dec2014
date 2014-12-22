using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
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
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;
using Microsoft.Practices.Unity;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TeacherApp.Client.UI.WinApp
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeTable : Page
    {
        private MainViewModel _mainViewModel;
        private TimetableViewModel _timetableViewModel;
        private CalenderViewDefaltViewModel _calenderViewDefaltViewModel;
        private List<IDisposable> _propertyBindings = new List<IDisposable>();
        private UnityContainer unityContainer;
        List<DayViewModel> dayList = null;
        const String CurrentDayColor = "#5583A0";
        const String DayColor = "#38779B";
        const String BorderColor = "#1A567A";
        const String StackBackGround = "#768F93";

        private int StackWidth = 0;
        private double DayWidth = 150;
        bool isPresent = false;
        dynamic bounds = 0.0;
        public TimeTable()
        {
            this.InitializeComponent();
            TimeTableGrid.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            // TimeTableGrid.ManipulationStarted += OnManipulationStarted;
            TimeTableGrid.ManipulationCompleted += OnManipulationCompleted;
            bounds = Window.Current.Bounds;
            ActivityCanvas.Width = bounds.Width;
            ActivityCanvas.Height = bounds.Height;
            // DayScroll.Width = bounds.Width;
            ActivityCanvas.Children.Add(ActivityIndicator.ProgressActivityIndicator);
            StackTimeReminder.Visibility = Visibility.Collapsed;
        }

        private void OnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var x = e.Velocities;
            StackTimeReminder.Visibility = Visibility.Collapsed;
            //Canvas can = new Canvas();
            //can.Background = new SolidColorBrush(Colors.Red);
            //can.Width = TimeTableGrid.Width / 2;
            //TimeTableGrid.Children.Add(can);
            if (x.Linear.X > 0)
            {
                ActivityIndicator.ShowProgress();
                _calenderViewDefaltViewModel = new CalenderViewDefaltViewModel(_timetableViewModel);
                _calenderViewDefaltViewModel.getPreviousWeekData();
                dayList = new List<DayViewModel>();
                dayList = (List<DayViewModel>)_timetableViewModel.SelectedWeek.Days;
                RemoveChildren();
                BindDayAndDate(dayList);
                //BlankCanvas.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            }
            // && TimeTableGrid.ActualWidth < bounds.Width
            else if (x.Linear.X < 0)
            {
                ActivityIndicator.ShowProgress();
                _calenderViewDefaltViewModel = new CalenderViewDefaltViewModel(_timetableViewModel);
                _calenderViewDefaltViewModel.getNextWeekData();
                dayList = new List<DayViewModel>();
                dayList = (List<DayViewModel>)_timetableViewModel.SelectedWeek.Days;
                RemoveChildren();
                BindDayAndDate(dayList);
            }

        }

        public void RemoveChildren()
        {
            FirstCanvas.Children.Clear();
            MonFirstCanvas.Children.Clear();
            MonSecondCanvas.Children.Clear();
            MonThirdCanvas.Children.Clear();

            FirstCanvas.Children.Add(MonFirstCanvas);
            FirstCanvas.Children.Add(MonSecondCanvas);
            FirstCanvas.Children.Add(MonThirdCanvas);

            SecondCanvas.Children.Clear();
            TueFirstCanvas.Children.Clear();
            TueSecondCanvas.Children.Clear();
            TueThirdCanvas.Children.Clear();

            SecondCanvas.Children.Add(TueFirstCanvas);
            SecondCanvas.Children.Add(TueSecondCanvas);
            SecondCanvas.Children.Add(TueThirdCanvas);

            ThirdCanvas.Children.Clear();
            WedFirstCanvas.Children.Clear();
            WedSecondCanvas.Children.Clear();
            WedThirdCanvas.Children.Clear();

            ThirdCanvas.Children.Add(WedFirstCanvas);
            ThirdCanvas.Children.Add(WedSecondCanvas);
            ThirdCanvas.Children.Add(WedThirdCanvas);

            FourthCanvas.Children.Clear();
            ThuFirstCanvas.Children.Clear();
            ThuSecondCanvas.Children.Clear();
            ThuThirdCanvas.Children.Clear();

            FourthCanvas.Children.Add(ThuFirstCanvas);
            FourthCanvas.Children.Add(ThuSecondCanvas);
            FourthCanvas.Children.Add(ThuThirdCanvas);

            FifthCanvas.Children.Clear();
            FriFirstCanvas.Children.Clear();
            FriSecondCanvas.Children.Clear();
            FriThirdCanvas.Children.Clear();

            FifthCanvas.Children.Add(FriFirstCanvas);
            FifthCanvas.Children.Add(FriSecondCanvas);
            FifthCanvas.Children.Add(FriThirdCanvas);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //TimeTableGrid.Children.Add(ActivityIndicator.ProgressActivityIndicator);
            ActivityIndicator.ShowProgress();
            unityContainer = (UnityContainer)e.Parameter;
            var uiFactory = unityContainer.Resolve<IUIFactory>();
            _mainViewModel = uiFactory.GetMainViewModel();
            _timetableViewModel = _mainViewModel.TeacherTimetableViewModel;
            _propertyBindings.Add(_timetableViewModel.BindViewModelPropertyChangedTo(ViewModel.IsLoadingPropertyName, TimetableIsLoading));
            _calenderViewDefaltViewModel = new CalenderViewDefaltViewModel(_timetableViewModel);
            _calenderViewDefaltViewModel.getWeekData();
            dayList = new List<DayViewModel>();
            dayList = (List<DayViewModel>)_timetableViewModel.SelectedWeek.Days;
            //List<TeacherApp.Client.UI.ViewModels.Timetable.EventViewModel> EVWM = list
            var EVWM = dayList.SelectMany(h => h.Events).Select(rt => rt.EventId);
            //CvsList.Source =list;
            //listCalendar1.ItemsSource = _timetableViewModel.SelectedWeek.Days.ToList();

            BindDayAndDate(dayList);
        }

        public void BindDayAndDate(List<DayViewModel> dayList)
        {
            MonTextBlock.Text = dayList[0].ToString();
            FirstDate.Text = dayList[0].Date.Day.ToString();
            //if (dayList[0].Date.Day == System.DateTime.Now.Day)
            //{
            //    MonStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);                  
            //}
            //else
            //{
            //    MonStack.Background = GlobalFields.ColorToBrush(DayColor);
            //}
            TueTextBlock.Text = dayList[1].ToString();
            SecondDate.Text = dayList[1].Date.Day.ToString();
            //if (dayList[1].Date.Day == System.DateTime.Now.Day)
            //{
            //    TueStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);
            //}
            //else
            //{
            //    TueStack.Background = GlobalFields.ColorToBrush(DayColor);
            //}
            WedTextBlock.Text = dayList[2].ToString();
            ThirdDate.Text = dayList[2].Date.Day.ToString();
            //if (dayList[2].Date.Day == System.DateTime.Now.Day)
            //{
            //    WedStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);
            //}
            //else
            //{
            //    WedStack.Background = GlobalFields.ColorToBrush(DayColor);
            //}
            ThuTextBlock.Text = dayList[3].ToString();
            FourthDate.Text = dayList[3].Date.Day.ToString();
            //if (dayList[3].Date.Day == System.DateTime.Now.Day)
            //{
            //    ThuStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);
            //}
            //else
            //{
            //    ThuStack.Background = GlobalFields.ColorToBrush(DayColor);
            //}
            FriTextBlock.Text = dayList[4].ToString();
            FifthDate.Text = dayList[4].Date.Day.ToString();
            //if (dayList[4].Date.Day == System.DateTime.Now.Day)
            //{
            //    FriStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);
            //}
            //else
            //{
            //    FriStack.Background = GlobalFields.ColorToBrush(DayColor);
            //}
        }


        public void TimetableIsLoading()
        {
            if (_timetableViewModel.IsLoading)
            {
                ActivityCanvas.Visibility = Visibility.Visible;
                //TimeTableGrid.Visibility = Visibility.Collapsed;
                Canvas.SetZIndex(ActivityCanvas, 1);
                // _messenger.Raise<StartSimsLoadingIndicatorMessage>();

            }
            else
            {
                //_messenger.Raise<StopSimsLoadingIndicatorMessage>();    
                ActivityCanvas.Visibility = Visibility.Collapsed;
                // TimeTableGrid.Visibility = Visibility.Visible;
                ActivityIndicator.CloseProgress();
                OnWeeksCollectionChanged();
            }
        }

        #region Old Code
        //private void OnWeeksCollectionChanged1()
        //{
        //    TimeSpan tsStack1 = new TimeSpan(9);
        //    TimeSpan tsStack2 = new TimeSpan(9);
        //    TimeSpan tsStack3 = new TimeSpan(9);
        //    var weekData = _timetableViewModel.SelectedWeek.Days.SelectMany(evt => evt.Events).AsEnumerable();
        //    //var MonData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Monday" select data;
        //    var MonData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Monday" orderby data.StartTime select data;
        //    var TueData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Tuesday" select data;
        //    var WedData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Wednesday" select data;
        //    var ThuData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Thursday" select data;
        //    var FriData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Friday" select data;


        //    List<TimeSpan> timeTableList = getStartTime();
        //    bool creteBlank = default(bool);
        //    dstTime = new Dictionary<TimeSpan, TimeSpan>();
        //    dstClasses = new Dictionary<string, Double>();
        //    lstClasses = new List<object>();
        //    ///For Monday
        //    //foreach (TimeSpan starttime in timeTableList)
        //    //{

        //        EventViewModel Object = new EventViewModel();
        //        //Border eventBorderMon = new Windows.UI.Xaml.Controls.Border();
        //        //eventBorderMon.BorderThickness = new Thickness(0, 1, 0, 1);
        //        //eventBorderMon.BorderBrush = new SolidColorBrush(Colors.Black);
        //        foreach (EventViewModel obj in MonData)
        //        {
        //            Border eventBorderMon = new Windows.UI.Xaml.Controls.Border();
        //            eventBorderMon.BorderThickness = new Thickness(1, 1, 1, 1);
        //            eventBorderMon.BorderBrush = new SolidColorBrush(Colors.Black);
        //            //eventBorderMon.Width = 500;

        //            //if ((obj.StartTime.Hour).Equals(starttime.Hours))
        //            //{
        //                StackPanel tsk = new StackPanel();

        //                // eventBorderMon.Height = height;
        //                if (tsStack1 < obj.StartTime.TimeOfDay)
        //                {
        //                    tsk = (StackPanel)mypanel(obj, false, OverlappingClass(obj.StartTime.TimeOfDay) ? true : false, obj.EndTime.TimeOfDay, height1, "Stack1");
        //                    tsk.Background = new SolidColorBrush(Colors.Gray);
        //                    eventBorderMon.Child = tsk;
        //                    MonFirstCanvas.Children.Add(eventBorderMon);
        //                    tsStack1 = obj.EndTime.TimeOfDay;
        //                    lstClasses.Add("Stack1" + tsk.ActualHeight);
        //                  //  height1 = height1+tsk.ActualHeight;

        //                }
        //                else if (tsStack2 < obj.StartTime.TimeOfDay)
        //                {
        //                    tsk = (StackPanel)mypanel(obj, false, OverlappingClass(obj.StartTime.TimeOfDay) ? true : false, obj.EndTime.TimeOfDay, height2, "Stack2");
        //                    tsk.Background = new SolidColorBrush(Colors.Gray);
        //                    eventBorderMon.Child = tsk;
        //                    MonSecondCanvas.Children.Add(eventBorderMon);
        //                    tsStack2 = obj.EndTime.TimeOfDay;
        //                    lstClasses.Add("Stack2" + tsk.ActualHeight);
        //                   // height2 = height2 + tsk.ActualHeight;

        //                }
        //                else
        //                {
        //                    //foreach (FrameworkElement st in MonThirdCanvas.Children)
        //                    //{
        //                    //    height = height + st.ActualHeight;
        //                    //}
        //                    tsk = (StackPanel)mypanel(obj, false, OverlappingClass(obj.StartTime.TimeOfDay) ? true : false, obj.EndTime.TimeOfDay, height3, "Stack3");
        //                    tsk.Background = new SolidColorBrush(Colors.Gray);
        //                    eventBorderMon.Child = tsk;
        //                    MonThirdCanvas.Children.Add(eventBorderMon);
        //                    tsStack3 = obj.EndTime.TimeOfDay;
        //                    lstClasses.Add("Stack3" + tsk.ActualHeight);
        //                    //height3 = height3 + tsk.ActualHeight;
        //                }


        //                //StackPanel tsk = new StackPanel();
        //                //tsk = (StackPanel)mypanel(obj, starttime, false, OverlappingClass(obj.StartTime.TimeOfDay) ? true : false);
        //                //dstTime.Add(obj.StartTime.TimeOfDay, obj.EndTime.TimeOfDay);
        //                //// eventBorderMon.Height = height;
        //                //tsk.Background = new SolidColorBrush(Colors.Gray);
        //                //// eventBorderMon.Child = tsk;
        //                //FirstCanvas.Children.Add(tsk);

        //                creteBlank = false;
        //            //}
        //            if (obj.StartTime.Day == System.DateTime.Now.Day)
        //            {
        //                FirstCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
        //            }
        //            else
        //            {
        //                FirstCanvas.Background = GlobalFields.ColorToBrush(DayColor);
        //            }
        //        }
        //        //if (creteBlank.Equals(true))
        //        //{
        //        //    {
        //        //        StackPanel tsk = new StackPanel();
        //        //        tsk = (StackPanel)mypanel(Object, starttime, true, false,new TimeSpan(0));
        //        //        FirstCanvas.Children.Add(tsk);
        //        //        // break;
        //        //    }
        //        //}
        //        creteBlank = true;
        //        dstTime.Clear();
        //        MonStack.Width = 200;

        //    //}
        //    /////
        //    ///For Tuesday
        //    //foreach (TimeSpan starttime in timeTableList)
        //    //{

        //    //    EventViewModel Object = new EventViewModel();
        //    //    //Border eventBorderTue = new Windows.UI.Xaml.Controls.Border();
        //    //    //eventBorderTue.BorderThickness = new Thickness(0, 1, 0, 1);
        //    //    //eventBorderTue.BorderBrush = new SolidColorBrush(Colors.Black);
        //    //    foreach (EventViewModel obj in TueData)
        //    //    {
        //    //        Border eventBorderTue = new Windows.UI.Xaml.Controls.Border();
        //    //        eventBorderTue.BorderThickness = new Thickness(1, 1, 1, 1);
        //    //        eventBorderTue.BorderBrush = new SolidColorBrush(Colors.Black);

        //    //        if ((obj.StartTime.Hour).Equals(starttime.Hours))
        //    //        {
        //    //            StackPanel tsk = new StackPanel();
        //    //            tsk = (StackPanel)mypanel(obj, starttime, false, lstHour.Contains(obj.StartTime.Hour) ? true : false);
        //    //            lstHour.Add(obj.StartTime.Hour);
        //    //            eventBorderTue.Child = tsk;
        //    //            SecondCanvas.Children.Add(eventBorderTue);

        //    //            creteBlank = false;
        //    //        }
        //    //        if (obj.StartTime.Day == System.DateTime.Now.Day)
        //    //        {
        //    //            SecondCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
        //    //        }
        //    //        else
        //    //        {
        //    //            SecondCanvas.Background = GlobalFields.ColorToBrush(DayColor);
        //    //        }
        //    //    }
        //    //    if (creteBlank.Equals(true))
        //    //    {
        //    //        {
        //    //            StackPanel tsk = new StackPanel();
        //    //            tsk = (StackPanel)mypanel(Object, starttime, true, false);
        //    //            SecondCanvas.Children.Add(tsk);
        //    //            // break;
        //    //        }
        //    //    }
        //    //    creteBlank = true;
        //    //    lstHour.Clear();
        //    //    TueStack.Width = 200;
        //    //}
        //    /////

        //    /////For Wednesday
        //    //foreach (TimeSpan starttime in timeTableList)
        //    //{

        //    //    EventViewModel Object = new EventViewModel();
        //    //    //Border eventBorderWed = new Windows.UI.Xaml.Controls.Border();
        //    //    //eventBorderWed.BorderThickness = new Thickness(0, 1, 0, 1);
        //    //    //eventBorderWed.BorderBrush = new SolidColorBrush(Colors.Black);
        //    //    foreach (EventViewModel obj in WedData)
        //    //    {
        //    //        Border eventBorderWed = new Windows.UI.Xaml.Controls.Border();
        //    //        eventBorderWed.BorderThickness = new Thickness(1, 1, 1, 1);
        //    //        eventBorderWed.BorderBrush = new SolidColorBrush(Colors.Black);

        //    //        if ((obj.StartTime.Hour).Equals(starttime.Hours))
        //    //        {
        //    //            StackPanel tsk = new StackPanel();
        //    //            tsk = (StackPanel)mypanel(obj, starttime, false, lstHour.Contains(obj.StartTime.Hour) ? true : false);
        //    //            lstHour.Add(obj.StartTime.Hour);
        //    //            eventBorderWed.Child = tsk;
        //    //            ThirdCanvas.Children.Add(eventBorderWed);
        //    //            creteBlank = false;
        //    //        }
        //    //        if (obj.StartTime.Day == System.DateTime.Now.Day)
        //    //        {
        //    //            ThirdCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
        //    //        }
        //    //        else
        //    //        {
        //    //            ThirdCanvas.Background = GlobalFields.ColorToBrush(DayColor);
        //    //        }
        //    //    }
        //    //    if (creteBlank.Equals(true))
        //    //    {
        //    //        {
        //    //            StackPanel tsk = new StackPanel();
        //    //            tsk = (StackPanel)mypanel(Object, starttime, true, false);
        //    //            ThirdCanvas.Children.Add(tsk);
        //    //            // break;
        //    //        }
        //    //    }
        //    //    creteBlank = true;
        //    //    lstHour.Clear();
        //    //    WedStack.Width = 200;
        //    //}
        //    /////

        //    /////For Thursday
        //    //foreach (TimeSpan starttime in timeTableList)
        //    //{

        //    //    EventViewModel Object = new EventViewModel();
        //    //    //Border eventBorderThu = new Windows.UI.Xaml.Controls.Border();
        //    //    //eventBorderThu.BorderThickness = new Thickness(0, 1, 0, 1);
        //    //    //eventBorderThu.BorderBrush = new SolidColorBrush(Colors.Black);
        //    //    foreach (EventViewModel obj in ThuData)
        //    //    {
        //    //        Border eventBorderThu = new Windows.UI.Xaml.Controls.Border();
        //    //        eventBorderThu.BorderThickness = new Thickness(1, 1, 1, 1);
        //    //        eventBorderThu.BorderBrush = new SolidColorBrush(Colors.Black);

        //    //        if ((obj.StartTime.Hour).Equals(starttime.Hours))
        //    //        {
        //    //            StackPanel tsk = new StackPanel();
        //    //            tsk = (StackPanel)mypanel(obj, starttime, false, lstHour.Contains(obj.StartTime.Hour) ? true : false);
        //    //            lstHour.Add(obj.StartTime.Hour);
        //    //            eventBorderThu.Child = tsk;
        //    //            FourthCanvas.Children.Add(eventBorderThu);
        //    //            creteBlank = false;

        //    //        }
        //    //        if (obj.StartTime.Day == System.DateTime.Now.Day)
        //    //        {
        //    //            FourthCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
        //    //        }
        //    //        else
        //    //        {
        //    //            FourthCanvas.Background = GlobalFields.ColorToBrush(DayColor);
        //    //        }
        //    //    }
        //    //    if (creteBlank.Equals(true))
        //    //    {
        //    //        {
        //    //            StackPanel tsk = new StackPanel();
        //    //            tsk = (StackPanel)mypanel(Object, starttime, true, false);
        //    //            FourthCanvas.Children.Add(tsk);
        //    //            // break;
        //    //        }
        //    //    }
        //    //    creteBlank = true;
        //    //    lstHour.Clear();
        //    //    ThuStack.Width = 200;
        //    //}
        //    /////           
        //    /////For Friday
        //    //foreach (TimeSpan starttime in timeTableList)
        //    //{

        //    //    EventViewModel Object = new EventViewModel();
        //    //    //Border eventBorderFri = new Windows.UI.Xaml.Controls.Border();
        //    //    //eventBorderFri.BorderThickness = new Thickness(0, 1, 0, 1);
        //    //    //eventBorderFri.BorderBrush = new SolidColorBrush(Colors.Black);
        //    //    foreach (EventViewModel obj in FriData)
        //    //    {
        //    //        Border eventBorderFri;


        //    //        if ((obj.StartTime.Hour).Equals(starttime.Hours))
        //    //        {
        //    //            eventBorderFri = new Windows.UI.Xaml.Controls.Border();
        //    //            eventBorderFri.BorderThickness = new Thickness(1, 1, 1, 1);
        //    //            eventBorderFri.BorderBrush = new SolidColorBrush(Colors.Black);
        //    //            StackPanel tsk = new StackPanel();
        //    //            tsk = (StackPanel)mypanel(obj, starttime, false, lstHour.Contains(obj.StartTime.Hour) ? true : false);
        //    //            lstHour.Add(obj.StartTime.Hour);
        //    //            eventBorderFri.Child = tsk;
        //    //            FifthCanvas.Children.Add(eventBorderFri);
        //    //            creteBlank = false;
        //    //        }
        //    //        if (obj.StartTime.Day == System.DateTime.Now.Day)
        //    //        {
        //    //            FifthCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
        //    //        }
        //    //        else
        //    //        {
        //    //            FifthCanvas.Background = GlobalFields.ColorToBrush(DayColor);
        //    //        }
        //    //    }
        //    //    if (creteBlank.Equals(true))
        //    //    {
        //    //        {
        //    //            StackPanel tsk = new StackPanel();
        //    //            tsk = (StackPanel)mypanel(Object, starttime, true, false);
        //    //            FifthCanvas.Children.Add(tsk);
        //    //            // break;
        //    //        }
        //    //    }
        //    //    creteBlank = true;
        //    //    lstHour.Clear();
        //    //    FriStack.Width = 200;
        //    //}
        //    /////


        //}

        #endregion Old Code

        /// <summary>
        /// Method to change width of all the stack based on overlapping of classes
        /// </summary>
        /// <param name="DayFirstCanvas"></param>
        /// <param name="DaySecondCanvas"></param>
        /// <param name="DayThirdCanvas"></param>
        /// <param name="DayStack"></param>
        /// <param name="StackWidth"></param>

        private void DayAlignment(StackPanel DayFirstCanvas, StackPanel DaySecondCanvas, StackPanel DayThirdCanvas, StackPanel DayStack, int StackWidth)
        {
            switch (StackWidth)
            {
                case 3:
                    DayFirstCanvas.Width = DayWidth / 2;
                    DaySecondCanvas.Width = DayWidth / 2;
                    DayThirdCanvas.Width = DayWidth / 2;
                    DayStack.Width = DayFirstCanvas.Width + DaySecondCanvas.Width + DayThirdCanvas.Width;
                    break;
                case 2:
                    DayFirstCanvas.Width = DayWidth / 2;
                    DaySecondCanvas.Width = DayWidth / 2;
                    DayStack.Width = DayFirstCanvas.Width + DaySecondCanvas.Width;
                    break;
                default:
                    DayStack.Width = DayWidth;
                    DayFirstCanvas.Width = DayWidth;
                    break;

            }

        }
        private void OnWeeksCollectionChanged()
        {
            TimeSpan tsStack1 = new TimeSpan(9, 0, 0);
            TimeSpan tsStack2 = new TimeSpan(9, 0, 0);
            TimeSpan tsStack3 = new TimeSpan(9, 0, 0);
            var weekData = _timetableViewModel.SelectedWeek.Days.SelectMany(evt => evt.Events).AsEnumerable();
            var MonData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Monday" orderby data.StartTime select data;
            var TueData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Tuesday" orderby data.StartTime select data;
            var WedData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Wednesday" orderby data.StartTime select data;
            var ThuData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Thursday" orderby data.StartTime select data;
            var FriData = from data in weekData where data.StartTime.DayOfWeek.ToString() == "Friday" orderby data.StartTime select data;
            DayWidth = (bounds.Width - ImageCell.ActualWidth) / 5;
            //For Monday
            foreach (EventViewModel obj in MonData)
            {
                Border eventBorderMon = new Windows.UI.Xaml.Controls.Border();
                eventBorderMon.BorderBrush = GlobalFields.ColorToBrush(BorderColor);
                Border tsk = new Border();

                if (tsStack1 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack1);
                    // tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    //eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0.5, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    //eventBorderMon.Child = tsk;
                    MonFirstCanvas.Children.Add(tsk);
                    tsStack1 = obj.EndTime.TimeOfDay;
                    StackWidth = 1;
                }
                else if (tsStack2 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack2);
                    //  tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0.5, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    MonSecondCanvas.Children.Add(eventBorderMon);
                    tsStack2 = obj.EndTime.TimeOfDay;
                    StackWidth = 2;
                }
                else
                {
                    tsk = (Border)mypanel(obj, tsStack3);
                    // tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    MonThirdCanvas.Children.Add(eventBorderMon);
                    tsStack3 = obj.EndTime.TimeOfDay;
                    StackWidth = 3;
                }
                if (obj.StartTime.Day == System.DateTime.Now.Day)
                {
                    MonStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    MonFirstCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    MonSecondCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    MonThirdCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    TimeReminder(0);
                }
                else
                {
                    MonStack.Background = GlobalFields.ColorToBrush(DayColor);
                    MonFirstCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    MonSecondCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    MonThirdCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                }
            }

            DayAlignment(MonFirstCanvas, MonSecondCanvas, MonThirdCanvas, MonStack, StackWidth);
            StackWidth = 0;
            tsStack1 = new TimeSpan(9, 0, 0);
            tsStack2 = new TimeSpan(9, 0, 0);
            tsStack3 = new TimeSpan(9, 0, 0);


            #region others
            //For Tuesday
            foreach (EventViewModel obj in TueData)
            {
                Border eventBorderMon = new Windows.UI.Xaml.Controls.Border();
                eventBorderMon.BorderBrush = GlobalFields.ColorToBrush(BorderColor);
                Border tsk = new Border();

                if (tsStack1 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack1);
                    // tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0.5, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    TueFirstCanvas.Children.Add(eventBorderMon);
                    tsStack1 = obj.EndTime.TimeOfDay;
                    StackWidth = 1;
                }
                else if (tsStack2 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack2);
                    //  tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0.5, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    TueSecondCanvas.Children.Add(eventBorderMon);
                    tsStack2 = obj.EndTime.TimeOfDay;
                    StackWidth = 2;
                }
                else
                {
                    tsk = (Border)mypanel(obj, tsStack3);
                    // tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    TueThirdCanvas.Children.Add(eventBorderMon);
                    tsStack3 = obj.EndTime.TimeOfDay;
                    StackWidth = 3;
                }
                if (obj.StartTime.Day == System.DateTime.Now.Day)
                {
                    TueStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    TueFirstCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    TueSecondCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    TueThirdCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    TimeReminder(1);
                }
                else
                {
                    TueStack.Background = GlobalFields.ColorToBrush(DayColor);
                    TueFirstCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    TueSecondCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    TueThirdCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                }
            }
            DayAlignment(TueFirstCanvas, TueSecondCanvas, TueThirdCanvas, TueStack, StackWidth);

            StackWidth = 0;
            tsStack1 = new TimeSpan(9, 0, 0);
            tsStack2 = new TimeSpan(9, 0, 0);
            tsStack3 = new TimeSpan(9, 0, 0);

            //For Wednesday
            foreach (EventViewModel obj in WedData)
            {
                Border eventBorderMon = new Windows.UI.Xaml.Controls.Border();
                eventBorderMon.BorderBrush = GlobalFields.ColorToBrush(BorderColor);
                Border tsk = new Border();

                if (tsStack1 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack1);
                    //  tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0.5, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    WedFirstCanvas.Children.Add(eventBorderMon);
                    tsStack1 = obj.EndTime.TimeOfDay;
                    StackWidth = 1;
                }
                else if (tsStack2 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack2);
                    // tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0.5, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    WedSecondCanvas.Children.Add(eventBorderMon);
                    tsStack2 = obj.EndTime.TimeOfDay;
                    StackWidth = 2;
                }
                else
                {
                    tsk = (Border)mypanel(obj, tsStack3);
                    // tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    WedThirdCanvas.Children.Add(eventBorderMon);
                    tsStack3 = obj.EndTime.TimeOfDay;
                    StackWidth = 3;
                }
                if (obj.StartTime.Day == System.DateTime.Now.Day)
                {
                    WedStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    WedFirstCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    WedSecondCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    WedThirdCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    TimeReminder(2);
                }
                else
                {
                    WedStack.Background = GlobalFields.ColorToBrush(DayColor);
                    WedFirstCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    WedSecondCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    WedThirdCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                }
            }
            DayAlignment(WedFirstCanvas, WedSecondCanvas, WedThirdCanvas, WedStack, StackWidth);
            StackWidth = 0;
            tsStack1 = new TimeSpan(9, 0, 0);
            tsStack2 = new TimeSpan(9, 0, 0);
            tsStack3 = new TimeSpan(9, 0, 0);

            //For Thursday
            foreach (EventViewModel obj in ThuData)
            {
                Border eventBorderMon = new Windows.UI.Xaml.Controls.Border();
                eventBorderMon.BorderBrush = GlobalFields.ColorToBrush(BorderColor);
                Border tsk = new Border();

                if (tsStack1 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack1);
                    // tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0.5, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    ThuFirstCanvas.Children.Add(eventBorderMon);
                    tsStack1 = obj.EndTime.TimeOfDay;
                    StackWidth = 1;
                }
                else if (tsStack2 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack2);
                    // tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0.5, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    ThuSecondCanvas.Children.Add(eventBorderMon);
                    tsStack2 = obj.EndTime.TimeOfDay;
                    StackWidth = 2;
                }
                else
                {
                    tsk = (Border)mypanel(obj, tsStack3);
                    //  tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    ThuThirdCanvas.Children.Add(eventBorderMon);
                    tsStack3 = obj.EndTime.TimeOfDay;
                    StackWidth = 3;
                }
                if (obj.StartTime.Day == System.DateTime.Now.Day)
                {
                    ThuStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    ThuFirstCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    ThuSecondCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    ThuThirdCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);

                    TimeReminder(3);
                }
                else
                {
                    ThuStack.Background = GlobalFields.ColorToBrush(DayColor);
                    ThuFirstCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    ThuSecondCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    ThuThirdCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                }
            }
            DayAlignment(ThuFirstCanvas, ThuSecondCanvas, ThuThirdCanvas, ThuStack, StackWidth);
            StackWidth = 0;
            tsStack1 = new TimeSpan(9, 0, 0);
            tsStack2 = new TimeSpan(9, 0, 0);
            tsStack3 = new TimeSpan(9, 0, 0);

            //For Friday
            foreach (EventViewModel obj in FriData)
            {
                Border eventBorderMon = new Windows.UI.Xaml.Controls.Border();
                eventBorderMon.BorderBrush = GlobalFields.ColorToBrush(BorderColor);
                Border tsk = new Border();

                if (tsStack1 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack1);
                    // tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0.5, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    FriFirstCanvas.Children.Add(eventBorderMon);
                    tsStack1 = obj.EndTime.TimeOfDay;
                    StackWidth = 1;
                }
                else if (tsStack2 <= obj.StartTime.TimeOfDay)
                {
                    tsk = (Border)mypanel(obj, tsStack2);
                    //  tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0.5, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    FriSecondCanvas.Children.Add(eventBorderMon);
                    tsStack2 = obj.EndTime.TimeOfDay;
                    StackWidth = 2;
                }
                else
                {
                    tsk = (Border)mypanel(obj, tsStack3);
                    //  tsk.Background = GlobalFields.ColorToBrush(StackBackGround);
                    eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0, 0.5);
                    tsk.Tapped += block1_Tapped;
                    tsk.Name = obj.EventId.ToString();
                    eventBorderMon.Child = tsk;
                    FriThirdCanvas.Children.Add(eventBorderMon);
                    tsStack3 = obj.EndTime.TimeOfDay;
                    StackWidth = 3;
                }
                if (obj.StartTime.Day == System.DateTime.Now.Day)
                {
                    FriStack.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    FriFirstCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    FriSecondCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    FriThirdCanvas.Background = GlobalFields.ColorToBrush(CurrentDayColor);
                    TimeReminder(4);
                }
                else
                {
                    FriStack.Background = GlobalFields.ColorToBrush(DayColor);
                    FriFirstCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    FriSecondCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                    FriThirdCanvas.Background = GlobalFields.ColorToBrush(DayColor);
                }
            }
            DayAlignment(FriFirstCanvas, FriSecondCanvas, FriThirdCanvas, FriStack, StackWidth);
            StackWidth = 0;
            tsStack1 = new TimeSpan(9, 0, 0);
            tsStack2 = new TimeSpan(9, 0, 0);
            tsStack3 = new TimeSpan(9, 0, 0);

            #endregion others
        }

        private void GradientFill()
        {

            //LinearGradientBrush myBrush = new LinearGradientBrush();
            //myBrush.GradientStops.Add(new GradientStop());
            //myBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.5));
            //myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));

            //exampleRectangle.Fill = myBrush;
        }
        private void TimeReminder(int offset)
        {
            #region Code for full background
            //StackTimeReminder.Visibility = Visibility.Visible;
            //StackTimeReminder.Width = DayWidth;
            //StackTimeReminder.Height = (System.DateTime.Now.TimeOfDay.TotalHours - 9) * TimeBlock.ActualHeight;
            //StackTimeReminder.Margin = new Thickness((offset * StackTimeReminder.Width) + TimeBlock.ActualWidth + 5, ImageCell.ActualHeight + 1, 0, 0);
            #endregion

            StackTimeReminder.Visibility = Visibility.Visible;
            StackTimeReminder.Width = DayWidth;
            StackTimeReminder.Height = 3;
            StackTimeReminder.Margin = new Thickness((offset * StackTimeReminder.Width) + TimeBlock.ActualWidth, ((System.DateTime.Now.TimeOfDay.TotalHours - 9) * TimeBlock.ActualHeight) + ImageCell.ActualHeight, 0, 0);
        }

        /// <summary>
        /// For Dynamic creation of Stack Panel
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="TimeTablestartTime"></param>
        /// <returns></returns>
        public Border mypanel(EventViewModel obj, TimeSpan tsStackLastEndTime)
        {
            StackPanel boxpanel = new StackPanel();
            StackPanel stack = new StackPanel();

            double tmargin = ((obj.StartTime - tsStackLastEndTime).TimeOfDay.TotalHours) * TimeBlock.ActualHeight;

            TextBlock block1 = new TextBlock();
            TextBlock block2 = new TextBlock();
            TextBlock block3 = new TextBlock();
            block1.Text = obj.Subject;
            block1.FontSize = 16;
            block2.FontSize = 12;
            block3.FontSize = 12;
            block1.FontFamily = new FontFamily("Segoe UI Emoji");
            block2.FontFamily = new FontFamily("Segoe UI Emoji");
            block3.FontFamily = new FontFamily("Segoe UI Emoji");
            block1.FontWeight = FontWeights.Bold;
            block2.Text = obj.Class;
            block3.Text = obj.Room;
            block1.Margin = new Thickness(10, 0, 0, 0);
            block2.Margin = new Thickness(10, 0, 0, 0);
            block3.Margin = new Thickness(10, 0, 0, 0);
            block1.TextWrapping = TextWrapping.Wrap;
            block2.TextWrapping = TextWrapping.Wrap;
            block3.TextWrapping = TextWrapping.Wrap;
            Canvas.SetZIndex(block1, 3);
            Canvas.SetZIndex(block2, 3);
            Canvas.SetZIndex(block3, 3);
            stack.Children.Add(block1);
            stack.Children.Add(block2);
            stack.Children.Add(block3);
            stack.Width = DayWidth;
            //stack.Height = classHeight(obj.StartTime, obj.EndTime);
            stack.Height = ((obj.EndTime - obj.StartTime).TotalHours) * TimeBlock.ActualHeight + 5;

            Border eventBorderMon = new Windows.UI.Xaml.Controls.Border();
            eventBorderMon.BorderBrush = GlobalFields.ColorToBrush(BorderColor);
            eventBorderMon.BorderThickness = new Thickness(0, 0.5, 0, 0.5);
            eventBorderMon.Child = stack;
            eventBorderMon.Margin = new Thickness(0, tmargin, 0, 0);

            //stack.Margin = new Thickness(tmargin == 0 ? tmargin + 150 : 0, tmargin, 0, 0);
            //stack.VerticalAlignment = VerticalAlignment.Top;
            eventBorderMon.HorizontalAlignment = HorizontalAlignment.Left;
            // stackborder.Child = stack;
            // boxpanel = stack;
            return eventBorderMon;
        }

        void block1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Border selectedStack = (Border)sender;
            var x = _timetableViewModel.SelectedWeek.Days.SelectMany(evt => evt.Events).AsEnumerable();
            EventViewModel viewModelData = (from data in x.AsEnumerable() where data.EventId == Convert.ToInt32(selectedStack.Name) select data).ToList()[0];
            GlobalFields.selectedModel = viewModelData;
            PageManager pageManager = unityContainer.Resolve<PageManager>();
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            pageManager.NavigateToAttendance();
        }
        /// <summary>
        /// Getting Margin from Top
        /// </summary>
        /// <param name="TimeTableStartTime"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public double topMargin(DateTime startTime, double height)
        {
            Double topMargin = default(Double);
            //TimeSpan FistTime = Convert.ToDateTime(startTime).TimeOfDay;
            TimeSpan SecondTime = startTime.TimeOfDay;
            //  TimeSpan avg = SecondTime.Subtract(TimeTableStartTime);
            // if(startTime.TimeOfDay.TotalHours!=0)


            topMargin = ((Convert.ToDouble(startTime.TimeOfDay.Subtract(Convert.ToDateTime("9:00").TimeOfDay).TotalHours)) * TimeBlock.ActualHeight) - height;

            //if (avg.TotalMinutes % 60 == 0) { return 0; }
            //if (avg.TotalMinutes % 30 == 0) { return TimeBlock.ActualHeight/2; }
            //if (avg.TotalMinutes % 15 == 0) { return TimeBlock.ActualHeight / 4; }
            return Math.Abs(topMargin);
        }

        /// <summary>
        /// Getting Margin from Top
        /// </summary>
        /// <param name="TimeTableStartTime"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public double classHeight(DateTime StartTime, DateTime EndTime)
        {
            Double classHeight = default(Double);
            //TimeSpan avg = EndTime.Subtract(StartTime);
            //classHeight = Convert.ToDouble( avg )* (TimeBlock.ActualHeight - 13);
            classHeight = ((Double)(EndTime - StartTime).TotalHours) * (TimeBlock.ActualHeight);

            return classHeight;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TimeSpan> getStartTime()
        {
            List<TimeSpan> TimeTableTime = new List<TimeSpan>();
            TimeTableTime.Add(Convert.ToDateTime("9:00").TimeOfDay);
            TimeTableTime.Add(Convert.ToDateTime("10:00").TimeOfDay);
            TimeTableTime.Add(Convert.ToDateTime("11:00").TimeOfDay);
            TimeTableTime.Add(Convert.ToDateTime("12:00").TimeOfDay);
            TimeTableTime.Add(Convert.ToDateTime("13:00").TimeOfDay);
            TimeTableTime.Add(Convert.ToDateTime("14:00").TimeOfDay);
            TimeTableTime.Add(Convert.ToDateTime("15:00").TimeOfDay);
            TimeTableTime.Add(Convert.ToDateTime("16:00").TimeOfDay);
            TimeTableTime.Add(Convert.ToDateTime("17:00").TimeOfDay);
            return TimeTableTime;
        }
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
        }
    }
}