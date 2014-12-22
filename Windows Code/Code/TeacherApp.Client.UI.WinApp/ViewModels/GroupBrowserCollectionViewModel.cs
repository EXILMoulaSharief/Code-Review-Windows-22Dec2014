using Microsoft.Practices.Unity;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.Domain;
using TeacherApp.Client.UI.Cache;
using TeacherApp.Client.UI.ViewModels.GroupBrowser;
using TeacherApp.Client.UI.ViewModels.Timetable;
using TeacherApp.Client.UI.WinApp.Common;
using TeacherApp.Client.UI.WinApp.Configuration;
using Windows.UI.Xaml.Controls;
using TeacherApp.Client.UI.WinApp.Common;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading;

namespace TeacherApp.Client.UI.WinApp.ViewModels
{
    public class GroupBrowserCollectionViewModel
    {

        private readonly GroupBrowserViewModel _groupBrowserViewModel;
        private SearchMode _currentSearchModeType;
        private IGroupBrowserMode _currentSearchMode;
        private readonly Dictionary<SearchMode, IGroupBrowserMode> _searchModes;
        private readonly Dictionary<GroupBrowserDisplayMode, IGroupBrowserMode> _coreGroupBrowserModes;
        private IGroupBrowserMode _currentCoreMode;
        private GroupBrowserDisplayMode _currentCoreModeType;
        IImageProvider<BitmapImage> _imageProvider;
        private readonly IDisposable _isChangingStudentsCollectionPropertyChange;
        ImageManager _imageManager;

        public delegate void CallbackEventHandler(ImageDetail<BitmapImage> image);
        public event CallbackEventHandler Callback;

       // public delegate void VerySimpleDelegate();
        public GroupBrowserCollectionViewModel()
        {

        }
        public GroupBrowserCollectionViewModel(GroupBrowserViewModel groupBrowserViewModel, object SelectedEvent,UnityContainer _unityContainer)
        {
            try
            {
                _groupBrowserViewModel = groupBrowserViewModel;
                _groupBrowserViewModel.SelectedEvent = (EventViewModel) SelectedEvent;
                //_groupBrowserViewModel.LoadStudentsCommand.Execute(null);               
                LoadStudentsForGroup();
                _currentCoreModeType = GroupBrowserDisplayMode.None;               
                //StartCoreMode(GroupBrowserDisplayMode.Browse, null);
                _imageManager = new ImageManager(_unityContainer.Resolve<IDomainFactory>(), _unityContainer.Resolve<AppSettingsProvider>());
              
                _imageProvider = _imageManager.GetStudentImages();
                _imageProvider.ImageUpdated += imageProvider_ImageUpdated;
                _isChangingStudentsCollectionPropertyChange = _groupBrowserViewModel.BindViewModelPropertyChangedTo(GroupBrowserViewModel.IsChangingStudentsCollectionPropertyName, IsChangingStudentsPropertyChange);
                
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void imageProvider_ImageUpdated(ImageUpdatedEventHandlerArgs<BitmapImage> args)
        {
            if (Callback != null)  
                Callback(args.ImageDetail);          
        }

        private void StartSearchMode(SwitchToSearchModeMessage message)
        {
            _currentSearchModeType = SearchMode.None;
            IGroupBrowserMode modeToSwitchTo;
            if (_searchModes.TryGetValue(message.SearchMode, out modeToSwitchTo))
            {
                _groupBrowserViewModel.StudentSearchViewModel.IsSearchingWithinMode = true;
                _groupBrowserViewModel.StudentSearchViewModel.IsFilteringWithinMode = true;

                if (_currentSearchMode != null)
                {
                    _currentSearchMode.StopMode(() => modeToSwitchTo.StartMode(() => { _currentSearchMode = modeToSwitchTo; }));
                }
                else
                {
                    modeToSwitchTo.StartMode(() => { _currentSearchMode = modeToSwitchTo; });
                }
            }
            else
            {
                _groupBrowserViewModel.StudentSearchViewModel.IsSearchingWithinMode = false;
                _groupBrowserViewModel.StudentSearchViewModel.IsFilteringWithinMode = false;

                if (_currentSearchMode != null)
                {
                    _currentSearchMode.StopMode(null);
                }
                _currentSearchMode = null;
            }
        }
        private void LoadStudentsForGroup()
        {
            if (_currentSearchMode != null)
            {
                _groupBrowserViewModel.StudentSearchViewModel.IsSearchingWithinMode = false;
                _groupBrowserViewModel.StudentSearchViewModel.IsFilteringWithinMode = false;

                _currentSearchMode.StopMode(() =>
                {
                    _currentSearchMode = null;
                    StartCoreMode(GroupBrowserDisplayMode.Browse, () => _groupBrowserViewModel.LoadStudentsCommand.Execute(null));
                });
            }
            else
            {
                StartCoreMode(GroupBrowserDisplayMode.Browse, () => _groupBrowserViewModel.LoadStudentsCommand.Execute(null));
            }
        }
        private void StartCoreMode(GroupBrowserDisplayMode mode, Action onCompletion)
        {
            if (_currentCoreModeType != mode)
            {
                _currentCoreModeType = mode;
                IGroupBrowserMode modeToSwitchTo;
                if (_coreGroupBrowserModes.TryGetValue(mode, out modeToSwitchTo))
                {
                    if (_currentCoreMode != null)
                    {
                        _currentCoreMode.StopMode(() => modeToSwitchTo.StartMode(() =>
                        {
                            _currentCoreMode = modeToSwitchTo;
                            if (onCompletion != null)
                            {
                                onCompletion.Invoke();
                            }
                        }));
                    }
                    else
                    {
                        modeToSwitchTo.StartMode(() =>
                        {
                            _currentCoreMode = modeToSwitchTo;
                            if (onCompletion != null)
                            {
                                onCompletion.Invoke();
                            }
                        });
                    }
                }
            }
            else
            {
                if (onCompletion != null)
                {
                    onCompletion.Invoke();
                }
            }
        }
        /// <summary>
        /// /
        /// </summary>
        public void  loadCallback( List<ImageDetail<BitmapImage>> list)
        {            
            //bool isCalled = true;
            List<ImageDetail<BitmapImage>> mylist = list;
            //return list;
        }
        private void IsChangingStudentsPropertyChange()
        {
            bool isChangingStudents = _groupBrowserViewModel.IsChangingStudentsCollection;
            if (isChangingStudents)
            {
                //_groupBrowserTransactionManager.ClearTransactions();
            }
            else
            {
                //_groupBrowserTransactionManager.ExecuteAddOrRemovalOfStudents(() =>
                //{

                   
                   var studentIds = _groupBrowserViewModel.StudentsInContext.Select(x => x.Id).ToList();
                   //((AllStudentImageProvider<UIImage>)_imageProvider).Callback += new AllStudentImageProvider<UIImage>.CallbackEventHandler(loadCallback);
                   var studentImageDetails = _imageProvider.GetImages(studentIds);
                   // ((AllStudentImageProvider<UIImage>)_imageProvider).Callback += ((AllStudentImageProvider<UIImage>)_imageProvider).ca (sr_Callback);
                /*
                    while (!((AllStudentImageProvider<UIImage>)_imageProvider).didDownloadFinish()) 
                    {
                        //Thread.Sleep(200);
                        System.Threading.Tasks.Task.Delay(200);
                       
                    }
                    var stdImageDetails = ((AllStudentImageProvider<UIImage>)_imageProvider).getResponseList();

                */
               
                    //foreach (GroupBrowserCell cell in _collectionView.VisibleCells)
                    //{
                    //    var imageDetailWithStudentWhoIsVisible = studentImageDetails.SingleOrDefault(x => x.PersonId == cell.Student.Id);
                    //    if (imageDetailWithStudentWhoIsVisible != null)
                    //    {
                    //        cell.SetImage(imageDetailWithStudentWhoIsVisible.Image, false);
                    //    }
                    //}
              //  });
            }
        }
        void sr_Callback(string something)
        {

        }

    }

internal class SwitchToSearchModeMessage 
{
    public SwitchToSearchModeMessage(SearchMode searchMode)
    {
        SearchMode = searchMode;
    }

    public SearchMode SearchMode { get; private set; }
}
public enum SearchMode
{
    Search = 0,
    Filter = 1,
    AddName = 3,
    None
}
public interface IGroupBrowserMode : IDisposable
{
    /// <param name="onCompletion">Must have onCompletion executed at the end of the method</param>
    void StartMode(Action onCompletion);

    /// <param name="onCompletion">Must have onCompletion executed at the end of the method</param>
    void StopMode(Action onCompletion);
}
internal enum GroupBrowserDisplayMode
    {
        Browse,
        Attendance,
        Achievement,
        Behaviour,
        None,
    }
  
}
