using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TeacherApp.Client.Domain;
using TeacherApp.Client.Domain.Configuration;
using TeacherApp.Client.Domain.Configuration.Implementation;
using TeacherApp.Client.Marksheet.Domain;
using TeacherApp.Client.Marksheet.Domain.Services;
using TeacherApp.Client.Marksheet.Domain.Services.Implementation;
using TeacherApp.Client.Marksheet.UI;
using TeacherApp.Client.Shared.Domain.Base;
using TeacherApp.Client.Shared.Domain.Factories;
using TeacherApp.Client.Shared.Domain.Managers;
using TeacherApp.Client.Shared.Domain.Services;
using TeacherApp.Client.Shared.UI.Services;
using TeacherApp.Client.Shared.UI.VisualSupport;
using TeacherApp.Client.UI.Management;
using TeacherApp.Client.UI.ViewModels.Student;
using TeacherApp.Client.UI.WinApp.Common;
using TeacherApp.Client.UI.WinApp.Management;
using TeacherApp.Client.UI.WinApp.Configuration;
using TeacherApp.Shared;
using TeacherApp.Shared.Implementation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TeacherApp.Client.UI.WinApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }
        private UnityContainer infraComponents;

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }
                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (rootFrame.Content == null)
            {

                //new unity contin
                UnityContainer _unityContainer = new UnityContainer();

                //_unityContainer.RegisterType<INetworkConnectivityService, NetworkConnectivityService>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<ISecureStorageManager,SecureStorageManager>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<INavigationManager, NavigationManager>();
                //_unityContainer.RegisterType<INetworkActivityService, NetworkActivityService>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IExecuterFactory,WebRequestFailureRetryExecuterFactory>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IConfigurationSettingProvider, ConfigurationSettingProvider>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<ISerializationManager,ProtobufSerializationManager>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IWebClientService, WebRequestClientService>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IStudentsBadgesSnapshotManager,StudentsBadgesSnapshotManager>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IMarksheetDomainFactory, MarksheetDomainFactory>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IMarksheetViewModelFactory, MarksheetViewModelFactory>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IMarksheetService, MarksheetService>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IQueryStringFactory,QueryStringFactory>();
                //_unityContainer.RegisterType<IServiceClient,ServiceClient>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IDomainFactory,DomainFactory>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IUIFactory, UIFactory>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<IAppSettingsProvider,AppSettingsProvider>(new ContainerControlledLifetimeManager());
                //_unityContainer.RegisterType<PageManager>(new ContainerControlledLifetimeManager());

                //var d = _unityContainer.Resolve<DomainFactory>();
                //var w = _unityContainer.Resolve<UIFactory>();
                NetworkConnectivityService _networkConnectivityService = new NetworkConnectivityService();
                SecureStorageManager _secureStorageManager = new SecureStorageManager();
                NavigationManager _navigationManager = new NavigationManager();
                NetworkActivityService _networkActivityService = new NetworkActivityService();
                DialogService _dialogService = new DialogService();
                WebRequestFailureRetryExecuterFactory _executerFactory = new WebRequestFailureRetryExecuterFactory(_dialogService, _networkConnectivityService, _networkActivityService);
                ConfigurationSettingProvider _configurationSettingProvider = new ConfigurationSettingProvider();
                ProtobufSerializationManager _protobufSerializationManager = new ProtobufSerializationManager();
                WebRequestClientService _webRequestClientService = new WebRequestClientService(_secureStorageManager);
                StudentsBadgesSnapshotManager _studentsBadgesSnapshotManager = new StudentsBadgesSnapshotManager();
                IMarksheetDomainFactory _marksheetDomainFactory = new MarksheetDomainFactory(null);
                IMarksheetViewModelFactory _marksheetViewModelFactory = new MarksheetViewModelFactory(_executerFactory, _marksheetDomainFactory.GetMarksheetService());
                ServiceClient _serviceClient = new ServiceClient(new QueryStringFactory(_configurationSettingProvider, _protobufSerializationManager), _protobufSerializationManager, _webRequestClientService);
                IDomainFactory _domainFactory = new DomainFactory(_secureStorageManager, _executerFactory, _serviceClient, _configurationSettingProvider);
                IUIFactory _uiFactory = new UIFactory(_navigationManager, _domainFactory, _dialogService, _networkConnectivityService, _executerFactory, _studentsBadgesSnapshotManager, _marksheetViewModelFactory);
                AppSettingsProvider _appSettings = new AppSettingsProvider();
               
                _unityContainer.RegisterInstance<NetworkConnectivityService>(_networkConnectivityService, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<SecureStorageManager>(_secureStorageManager, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<NavigationManager>(_navigationManager, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<NetworkActivityService>(_networkActivityService, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<DialogService>(_dialogService, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<WebRequestFailureRetryExecuterFactory>(_executerFactory, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<ConfigurationSettingProvider>(_configurationSettingProvider, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<ProtobufSerializationManager>(_protobufSerializationManager, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<WebRequestClientService>(_webRequestClientService, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<StudentsBadgesSnapshotManager>(_studentsBadgesSnapshotManager, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<IMarksheetDomainFactory>(_marksheetDomainFactory, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<IMarksheetViewModelFactory>(_marksheetViewModelFactory, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<ServiceClient>(_serviceClient, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<IDomainFactory>(_domainFactory, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<IUIFactory>(_uiFactory, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<AppSettingsProvider>(_appSettings, new ContainerControlledLifetimeManager());
                _unityContainer.RegisterInstance<PageManager>(new PageManager(), new ContainerControlledLifetimeManager());               


                PageManager thisPage = _unityContainer.Resolve<PageManager>();
                _dialogService.SetDependencies(thisPage);
                thisPage.Startup(_unityContainer);

                //PageManager thisPage = new PageManager(_unityContainer);
                //thisPage.Startup();
                //PageManager PageManager = new PageManager(rootFrame, _uiFactory, _secureStorageManager, _appSettings, _configurationSettingProvider);
                //_dialogService.SetDependencies(PageManager);
                //PageManager myPage = _unityContainer.Resolve<PageManager>();
                //myPage.Startup();
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }


        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
