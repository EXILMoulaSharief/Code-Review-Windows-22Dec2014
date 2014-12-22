using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.UI.ViewModels.Timetable;

namespace TeacherApp.Client.UI.WinApp.Model.Timetable
{
    internal class TimetableFullWeekCollectionViewDataSource 
    {
        //private readonly TimetableCollectionView _timetableCollectionView;
        private readonly TimetableViewModel _timetableViewModel;
        private readonly List<TimetableWeekMapping> _timetableWeekMappings;
        //private readonly IMessenger _messenger;
        //private readonly ITimetableConfigurationSettings _timetableTheme;
        //private TimetableGridProperties _gridProperties;
        private WeekViewModel _lastCheckedWeek;
        private bool _gridPropertiesChanged;

        internal TimetableFullWeekCollectionViewDataSource(TimetableViewModel timetableViewModel)
        {
            //_timetableCollectionView = timetableCollectionView;
            _timetableViewModel = timetableViewModel;
            //_messenger = messenger;
            //_timetableTheme = timetableTheme;

            //timetableCollectionView.RegisterClassForCell(typeof(TimetableFullWeekCell), TimetableFullWeekCell.Name);
            //timetableCollectionView.RegisterClassForCell(typeof(TimetableLoadingDataCell), TimetableLoadingDataCell.Name);

            _timetableWeekMappings = new List<TimetableWeekMapping> {new TimetableWeekMapping(0, true, true), new TimetableWeekMapping(1, true), new TimetableWeekMapping(2, true)};
        }

        public int NumberOfSections()
        {
            return _timetableWeekMappings.Count;
        }

        public int GetItemsCount(int section)
        {
            return 1;
        }

        //public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        //{
        //    var currentMapping = _timetableWeekMappings[indexPath.Section];

        //    if(!currentMapping.IsBufferCell)
        //    {
        //        WeekViewModel currentWeek = null;

        //        if(_timetableViewModel.Weeks != null && currentMapping.IndexOfWeek < _timetableViewModel.Weeks.Count)
        //        {
        //            currentWeek = _timetableViewModel.Weeks[currentMapping.IndexOfWeek];
        //        }
        //        _gridPropertiesChanged = false;

        //        var cell = (TimetableFullWeekCell) collectionView.DequeueReusableCell(TimetableFullWeekCell.Name, indexPath);

        //        if(_lastCheckedWeek == null)
        //        {
        //            _lastCheckedWeek = currentWeek;
        //        }

        //        if(_gridProperties == null)
        //        {
        //            _gridProperties = new TimetableGridProperties();
        //            _gridProperties.Setup(currentWeek, cell.Frame.Size);
        //        }
        //        else if(currentWeek != null && IsWeekDifferent(currentWeek))
        //        {
        //            _gridPropertiesChanged = true;
        //            _gridProperties.Setup(currentWeek, cell.Frame.Size);
        //        }

        //        _lastCheckedWeek = currentWeek;

        //        cell.SetupFullWeek(_messenger, _gridProperties, _timetableViewModel, currentWeek, _gridPropertiesChanged, _timetableTheme);

        //        return cell;
        //    }

        //    var loadingDataCell = (TimetableLoadingDataCell) _timetableCollectionView.DequeueReusableCell(TimetableLoadingDataCell.Name, indexPath);
        //    loadingDataCell.SetupLoadingCell(_timetableTheme);
        //    return loadingDataCell;
        //}

        private bool IsWeekDifferent(WeekViewModel currentWeek)
        {
            if(currentWeek.Days.Count() != _lastCheckedWeek.Days.Count()
               || currentWeek.MinDayStartTime.Hour != _lastCheckedWeek.MinDayStartTime.Hour
               || currentWeek.MaxDayEndTime.Hour != _lastCheckedWeek.MaxDayEndTime.Hour)
            {
                return true;
            }

            return false;
        }

        internal void OnWeeksCollectionChanged()
        {
            _timetableWeekMappings.Clear();

            _timetableWeekMappings.Add(new TimetableWeekMapping(0, true, true));

            for(int indexOfWeek = 0; indexOfWeek < _timetableViewModel.Weeks.Count; indexOfWeek++)
            {
                _timetableWeekMappings.Add(new TimetableWeekMapping(indexOfWeek, false));
            }

            _timetableWeekMappings.Add(new TimetableWeekMapping(2, true));

           // _timetableCollectionView.ReloadData();
           // _timetableCollectionView.UpdateSelectedDayAndWeek();
        }

        private class TimetableWeekMapping
        {
            public TimetableWeekMapping(int indexOfWeek, bool isBufferCell)
            {
                IndexOfWeek = indexOfWeek;
                IsBufferCell = isBufferCell;
            }

            public TimetableWeekMapping(int indexOfWeek, bool isBufferCell, bool isPastWeek) : this(indexOfWeek, isBufferCell)
            {
                IsPastWeek = isPastWeek;
            }

            public bool IsBufferCell { get; private set; }
            public int IndexOfWeek { get; private set; }
            public bool IsPastWeek { get; set; }
        }
    }
}

