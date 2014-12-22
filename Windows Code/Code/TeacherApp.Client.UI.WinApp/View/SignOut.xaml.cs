using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TeacherApp.Client.UI.WinApp
{
    public sealed partial class SignOut : UserControl
    {
        #region Declaraions
        private UnityContainer _unityContainer;
        private LogoutViewModel _logoutViewModel;

        #endregion

        public SignOut()
        {
            this.InitializeComponent();
        }


        public void SetDependencies(UnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        private void SignOut_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var uiFactory = _unityContainer.Resolve<IUIFactory>();
            _logoutViewModel = new LogoutViewModel(uiFactory.GetUserViewModel());
            _logoutViewModel.logout();
        }
    }
}
