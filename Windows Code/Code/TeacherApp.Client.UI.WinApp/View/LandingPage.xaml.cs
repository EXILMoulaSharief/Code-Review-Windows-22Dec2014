using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TeacherApp.Client.Shared.UI.VisualSupport;
using TeacherApp.Client.UI.WinApp.Common;
using TeacherApp.Client.UI.WinApp.Management;
using TeacherApp.Client.Shared.Domain.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TeacherApp.Client.Domain.Configuration.Implementation;
using TeacherApp.Client.Shared.Domain.Factories;
using TeacherApp.Shared.Implementation;
using TeacherApp.Client.Domain;
using TeacherApp.Client.UI.ViewModels.Student;
using TeacherApp.Client.UI.WinApp.Configuration;
using TeacherApp.Client.UI.ViewModels.Login;
using TeacherApp.Client.UI.WinApp.ViewModels;
using TeacherApp.Client.UI.Management;
using Microsoft.Practices.Unity;

namespace TeacherApp.Client.UI.WinApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LandingPage : Page
    {
        #region Declaraions
        /// <summary>
        /// 
        /// </summary>
        private UnityContainer unityContainer;
        /// <summary>
        /// 
        /// </summary>
        private LandingPageViewModel _landingPageViewModel;
        #endregion
        #region constructor
        /// <summary>
        /// 
        /// </summary>
        public LandingPage()
        {
            this.InitializeComponent();            
        }       
       
        #endregion
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            unityContainer = (UnityContainer)e.Parameter;
            var uiFactory = unityContainer.Resolve<IUIFactory>();
            _landingPageViewModel = new LandingPageViewModel(uiFactory.GetInitialChoiceViewModel());           
            SignInStoryboard.Begin();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Signin_Tapped(object sender, TappedRoutedEventArgs e)
        {           
            _landingPageViewModel.LoginUser();           
        }
        #endregion

    }
}
