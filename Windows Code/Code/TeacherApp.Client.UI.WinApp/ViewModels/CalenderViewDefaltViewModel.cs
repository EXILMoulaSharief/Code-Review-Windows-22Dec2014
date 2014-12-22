using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.UI.ViewModels.Timetable;
using TeacherApp.Client.UI.WinApp.Model;

namespace TeacherApp.Client.UI.WinApp.ViewModels
{
    public class CalenderViewDefaltViewModel
    {

         private readonly TimetableViewModel _timetableViewModel;
        private readonly TimetableCollectionViewController _timetableCollectionViewController;
         public CalenderViewDefaltViewModel(TimetableViewModel timetableViewModel )
         {
             _timetableViewModel = timetableViewModel ;
             _timetableCollectionViewController = new TimetableCollectionViewController(timetableViewModel);
            
         }
         public void getWeekData()
         {
             _timetableViewModel.LoadCurrentWeekDataCommand.Execute(null);            
         }
         public void getNextWeekData()
         {
             _timetableViewModel.LoadNextWeekDataCommand.Execute(null);
         }
         public void getPreviousWeekData()
         {
             _timetableViewModel.LoadPreviousWeekDataCommand.Execute(null);
         }

    }
}
