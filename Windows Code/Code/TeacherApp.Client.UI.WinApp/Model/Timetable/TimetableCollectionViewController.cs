using System;
using System.Collections.Generic;
using System.Linq;
//using TeacherApp.Client.UI.iOS.Visuals.Timetable.Views;
using TeacherApp.Client.UI.ViewModels.Timetable;
using TeacherApp.Client.UI.WinApp.Model.Timetable;

namespace TeacherApp.Client.UI.WinApp.Model
{
    internal class TimetableCollectionViewController 
    {
        //private readonly IMessenger _messenger;
        //private readonly ITimetableConfigurationSettings _timetableConfigurationSettings;
        private readonly TimetableViewModel _timetableViewModel;
        private bool _timetableHasChanged;
        private DayViewModel _currentDayViewModel;
        private WeekViewModel _currentWeekViewModel;
       // private NSIndexPath _indexForToday;
        private bool _scrolledToFuture;
       // private NSIndexPath _indexOfWeekCurrentlyOnScreen;

        private TimetableDayCollectionViewDataSource _timetableDayDayCollectionViewDataSource;
        //private TimetableDayCollectionViewDelegate _timetableDayDayCollectionViewDelegate;

        private TimetableFullWeekCollectionViewDataSource _timetableFullWeekCollectionViewDataSource;
       // private TimetableFullWeekCollectionViewDelegate _timetableFullWeekCollectionViewDelegate;

        private bool _timetableIsFullWeekMode;
        private bool _timetableWeeksPopulated;
        private readonly List<IDisposable> _propertyBindings;

        internal TimetableCollectionViewController(TimetableViewModel timetableViewModel)
        {
            _timetableViewModel = timetableViewModel;
            //_messenger = messenger;
            //_timetableConfigurationSettings = timetableConfigurationSettings;

            _propertyBindings = new List<IDisposable>();

            Initialize(timetableViewModel);
        }

        private void Initialize(TimetableViewModel timetableViewModel)
        {
            _timetableHasChanged = false;
            _timetableWeeksPopulated = false;
            _timetableIsFullWeekMode = false;
            //TimetableProperties.TimetableSwitchToFullMode = false;

            //DayCollectionView = new TimetableCollectionView(new RectangleF(0, -(TimetableProperties.TimetableDayViewOffset), TimetableProperties.TimetableWidth, TimetableProperties.TimetableFullDayHeight), this);
            _timetableDayDayCollectionViewDataSource = new TimetableDayCollectionViewDataSource(timetableViewModel);
            //_timetableDayDayCollectionViewDelegate = new TimetableDayCollectionViewDelegate(_messenger, _timetableConfigurationSettings, DayCollectionView);

            //FullWeekCollectionView = new TimetableCollectionView(new RectangleF(0, 0, TimetableProperties.TimetableWidth, TimetableProperties.TimetableFullWeekHeight), this);
            _timetableFullWeekCollectionViewDataSource = new TimetableFullWeekCollectionViewDataSource(timetableViewModel);
            //_timetableFullWeekCollectionViewDelegate = new TimetableFullWeekCollectionViewDelegate(FullWeekCollectionView);

            //DayCollectionView.DataSource = _timetableDayDayCollectionViewDataSource;
            //DayCollectionView.Delegate = _timetableDayDayCollectionViewDelegate;

            //FullWeekCollectionView.DataSource = _timetableFullWeekCollectionViewDataSource;
            //FullWeekCollectionView.Delegate = _timetableFullWeekCollectionViewDelegate;

            //_indexOfWeekCurrentlyOnScreen = NSIndexPath.FromItemSection(0, 1);
            ScrollToSelectedDayIndex(false);

            if (_timetableViewModel is TeacherTimetableViewModel)
            {
               // _propertyBindings.Add(_timetableViewModel.BindViewModelPropertyChangedTo(TeacherTimetableViewModel.SelectedEventPropertyName, OnSelectedEventChanged));
            }

            //_messenger.Subscribe<DayChangedAtMidnightMessage>(OnDayChanged);
            //_messenger.Subscribe<EventSelectedMessage>(EventSelected);
        }

        //public TimetableCollectionView FullWeekCollectionView { get; private set; }
        //public TimetableCollectionView DayCollectionView { get; private set; }

        /*
        private void EventSelected(EventSelectedMessage message)
        {
            var teacherTimetableViewModel = _timetableViewModel as TeacherTimetableViewModel;
            if (teacherTimetableViewModel != null)
            {
                teacherTimetableViewModel.SelectedEvent = message.SelectedEvent;
            }
        }*/

        public void ViewDidLoad()
        {
            //base.ViewDidLoad();
            //View.Frame = FullWeekCollectionView.Frame;
            //View.AutoresizingMask = UIViewAutoresizing.All;
            //View.AddSubview(FullWeekCollectionView);
            //View.AddSubview(DayCollectionView);
        }

        private void OnSelectedEventChanged()
        {
            if (_timetableViewModel is TeacherTimetableViewModel)
            {
                var teacherTimetableViewModel = _timetableViewModel as TeacherTimetableViewModel;
                if (teacherTimetableViewModel.SelectedEvent != null)
                {
                    //_messenger.Raise(new ChangeGroupBrowserToSelectedEventMessage(teacherTimetableViewModel.SelectedEvent));

                    //DayCollectionView.ReloadData();
                    //FullWeekCollectionView.ReloadData();
                }
            }
        }

        internal void OnWeeksCollectionChanged()
        {
            if (!_timetableViewModel.TimetableReset)
            {
                _timetableDayDayCollectionViewDataSource.OnWeeksCollectionChanged();
                _timetableFullWeekCollectionViewDataSource.OnWeeksCollectionChanged();

                _timetableWeeksPopulated = true;
            }
            if (_timetableHasChanged || _timetableViewModel.TimetableReset)
            {
                //_indexOfWeekCurrentlyOnScreen = DayCollectionView.IndexPathForItemAtPoint(new PointF(DayCollectionView.ContentOffset.X, 50));
                if (_scrolledToFuture || _timetableViewModel.TimetableReset)
                {
                    //_indexOfWeekCurrentlyOnScreen = NSIndexPath.FromItemSection(0, _timetableViewModel.Weeks.Count);
                }
                else
                {
                    //_indexOfWeekCurrentlyOnScreen = NSIndexPath.FromRowSection(4, 1);
                }

                if (_timetableViewModel.TimetableReset)
                {
                    //Need to reload
                    _timetableDayDayCollectionViewDataSource.OnWeeksCollectionChanged();
                    _timetableFullWeekCollectionViewDataSource.OnWeeksCollectionChanged();

                    _timetableWeeksPopulated = true;
                }

                ScrollToSelectedDayIndex(false);
                _timetableHasChanged = false;
                _timetableViewModel.TimetableReset = false;

                UpdateSelectedDayAndWeek();
            }
        }

        internal void ScrollToToday(bool animated)
        {
            DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;

            int currentDayOfWeek;
            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Monday)
            {
                currentDayOfWeek = 0;
            }
            else
            {
                currentDayOfWeek = (int)dayOfWeek - 1;
            }

            //_indexForToday = NSIndexPath.FromRowSection(currentDayOfWeek, 1);

            //_indexOfWeekCurrentlyOnScreen = _indexForToday;

            //DayCollectionView.ScrollToItem(_indexForToday, UICollectionViewScrollPosition.CenteredHorizontally, animated);
            //FullWeekCollectionView.ScrollToItem(NSIndexPath.FromItemSection(0, _indexForToday.Section), UICollectionViewScrollPosition.CenteredHorizontally, animated);

            UpdateSelectedDayAndWeek();
        }

        private void RaiseTimetableDisplayModeMessage()
        {
            if (_timetableIsFullWeekMode)
            {
                //if (_timetableConfigurationSettings.IsTeacherTimetable())
                //{
                //    _messenger.Raise(new CurrentlyDisplayedWeekChangedMessage(_currentWeekViewModel));
                //}
            }
            else
            {
                //if (_timetableConfigurationSettings.IsTeacherTimetable())
                //{
                //    _messenger.Raise(new CurrentyDisplayedDayChangedMessage(_currentDayViewModel));
                //}
                //else
                //{
                //    _messenger.Raise(new CurrentyDisplayedStudentTimetableDayChangedMessage(_currentDayViewModel));
                //}
            }
        }

        internal void UpdateSelectedDayAndWeek()
        {
            //var todaysCell = _timetableDayDayCollectionViewDataSource.GetCell(DayCollectionView, _indexOfWeekCurrentlyOnScreen);
            //if (todaysCell.GetType() == typeof(TimetableSingleDayCell))
            //{
                //_currentWeekViewModel = ((TimetableSingleDayCell)todaysCell).Week;
                //_currentDayViewModel = ((TimetableSingleDayCell)todaysCell).DayToShow;
                //_timetableViewModel.SelectedWeek = _currentWeekViewModel;
                //RaiseTimetableDisplayModeMessage();
            //}
        }

        internal void SetFullTimetable()
        {
            //_timetableIsFullWeekMode = true;
            //ScrollToSelectedDayIndex(false);
            //FullWeekCollectionView.Alpha = 1;
            //DayCollectionView.Hidden = true;
            //View.BringSubviewToFront(FullWeekCollectionView);
            //UpdateSelectedDayAndWeek();

            //if (_timetableWeeksPopulated)
            //{
            //    var visibleWeekCell = FullWeekCollectionView.VisibleCells.FirstOrDefault() as TimetableFullWeekCell;
            //    if (visibleWeekCell != null)
            //    {
            //        visibleWeekCell.AnimateToFullMode();
            //    }
            //}
        }

        //internal void SetDayTimetable(NSAction onComplete)
        //{
        //    _timetableIsFullWeekMode = false;
        //    ScrollToSelectedDayIndex(false);
        //    DayCollectionView.Hidden = false;

        //    UIView.Animate(.2f, () => { FullWeekCollectionView.Alpha = 0; }, () =>
        //    {
        //        if (_timetableWeeksPopulated)
        //        {
        //            var visibleWeekCell = FullWeekCollectionView.VisibleCells.FirstOrDefault() as TimetableFullWeekCell;
        //            if (visibleWeekCell != null)
        //            {
        //                visibleWeekCell.ResetCellForFullModeTransition();
        //            }
        //        }

        //        View.BringSubviewToFront(DayCollectionView);
        //        if (onComplete != null)
        //        {
        //            onComplete.Invoke();
        //        }
        //    });

        //    UpdateSelectedDayAndWeek();
        //}

        private void ScrollToSelectedDayIndex(bool animated)
        {
            //var weekItem = NSIndexPath.FromItemSection(0, _indexOfWeekCurrentlyOnScreen.Section);
            //FullWeekCollectionView.ScrollToItem(weekItem, UICollectionViewScrollPosition.CenteredHorizontally, animated);
            //if (_indexOfWeekCurrentlyOnScreen != null)
            //{
            //    DayCollectionView.ScrollToItem(_indexOfWeekCurrentlyOnScreen, UICollectionViewScrollPosition.CenteredHorizontally, animated);
            //}
        }

        internal void TimetableUpdated()
        {
            if (_timetableWeeksPopulated)
            {
                _timetableHasChanged = true;

                //if (_timetableIsFullWeekMode)
                //{
                //    _indexOfWeekCurrentlyOnScreen = FullWeekCollectionView.IndexPathForItemAtPoint(new PointF(FullWeekCollectionView.ContentOffset.X, 50));
                //}
                //else
                //{
                //    _indexOfWeekCurrentlyOnScreen = DayCollectionView.IndexPathForItemAtPoint(new PointF(DayCollectionView.ContentOffset.X, 50));
                //}

                //if (DayCollectionView.ContentOffset.X == 0 || FullWeekCollectionView.ContentOffset.X == 0)
                //{
                //    _scrolledToFuture = false;
                //    _indexOfWeekCurrentlyOnScreen = NSIndexPath.FromItemSection(0, 1);
                //    UpdateSelectedDayAndWeek();

                //    _timetableViewModel.LoadPreviousWeekDataCommand.Execute(null);
                //}

                //if (DayCollectionView.ContentOffset.X >= (DayCollectionView.ContentSize.Width - TimetableProperties.TimetableWidth) || FullWeekCollectionView.ContentOffset.X >= (FullWeekCollectionView.ContentSize.Width - TimetableProperties.TimetableWidth))
                //{
                //    _scrolledToFuture = true;
                //    _indexOfWeekCurrentlyOnScreen = NSIndexPath.FromItemSection(4, _timetableViewModel.Weeks.Count);
                    UpdateSelectedDayAndWeek();

                    _timetableViewModel.LoadNextWeekDataCommand.Execute(null);
                //}

                UpdateSelectedDayAndWeek();
            }
            //else
            //{
            //    _indexOfWeekCurrentlyOnScreen = null;
            //}
        }

        internal void SetSelectedDay(DayViewModel day)
        {
            DayOfWeek dayOfWeek = day.Date.DayOfWeek;

            int currentDayOfWeek;
            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Monday)
            {
                currentDayOfWeek = 0;
            }
            else
            {
                currentDayOfWeek = (int)dayOfWeek - 1;
            }

            //NSIndexPath newSelectedWeekIndex = NSIndexPath.FromItemSection(currentDayOfWeek, _indexOfWeekCurrentlyOnScreen.Section);

            //_indexOfWeekCurrentlyOnScreen = newSelectedWeekIndex;
        }

        internal float GetYOffsetForTimetable()
        {
            float offset = 0.0f;//TimetableProperties.TimetableFullDayHeight * _indexOfWeekCurrentlyOnScreen.Row;

            return offset;//TimetableProperties.TimetableDayViewOffset + offset;
        }

        private void OnDayChanged()
        {
            _timetableViewModel.UpdateCurrentDayAndWeekCommand.Execute(null);

            if (_timetableViewModel.CurrentWeek == null)
            {
                _timetableViewModel.LoadNextWeekDataCommand.Execute(null);
            }
            else
            {
                if (_timetableViewModel.CurrentWeek.Equals(_timetableViewModel.SelectedWeek))
                {
                    _currentDayViewModel = _timetableViewModel.CurrentWeek.Days.FirstOrDefault(d => d.IsCurrentDay);

                    //DayCollectionView.ReloadData();
                    //FullWeekCollectionView.ReloadData();

                    //if (!TimetableProperties.TimetableSwitchToFullMode)
                    //{
                    //    _messenger.Raise(new CurrentyDisplayedDayChangedMessage(_currentDayViewModel));
                    //    ScrollToToday(true);
                    //}
                }
            }
        }

        protected void Dispose(bool disposing)
        {
            //DayCollectionView.Dispose();
            //FullWeekCollectionView.Dispose();

            //_timetableDayDayCollectionViewDataSource.Dispose();
            //_timetableDayDayCollectionViewDelegate.Dispose();

            //_timetableFullWeekCollectionViewDataSource.Dispose();
            //_timetableFullWeekCollectionViewDelegate.Dispose();

            //_messenger.Unsubscribe<DayChangedAtMidnightMessage>(OnDayChanged);
            //_messenger.Unsubscribe<EventSelectedMessage>(EventSelected);

            //foreach (var propertyBinding in _propertyBindings)
            //{
            //    propertyBinding.Dispose();
            //}
            //base.Dispose(disposing);
        }
    }
}