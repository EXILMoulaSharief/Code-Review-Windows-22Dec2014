using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.Shared.UI.Services;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TeacherApp.Client.UI.WinApp
{
    internal class NetworkActivityService : INetworkActivityService
    {
        private int _activities;

        public  void ShowIndicator()
        {
          // UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
               // Debug.WriteLine("NetworkActivityService.Show() | Number Of Actions: {0}", _activities);
          //MessageDialog dlg = new MessageDialog("hi");
          //var frame = (Frame)Window.Current.Content;
          //var dialog = frame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => dlg.ShowAsync() );
        }

        public void HideIndicator()
        {
            MessageDialog dlg = new MessageDialog("hi");
            var frame = (Frame)Window.Current.Content;
            //var dialog = frame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => dlg.ShowAsync());
        }
    }
}
