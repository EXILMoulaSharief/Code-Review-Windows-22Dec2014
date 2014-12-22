using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using TeacherApp.Client.Domain.Configuration;
using TeacherApp.Client.Domain.Configuration.Implementation;
using TeacherApp.Client.Shared.Domain.Managers;
using TeacherApp.Client.Shared.UI.Services;
using TeacherApp.Client.UI.WinApp.Common;
using TeacherApp.Client.UI.WinApp.Configuration;
using TeacherApp.Shared;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TeacherApp.Client.UI.WinApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWebView : Page
    {
        #region Declarations
        /// <summary>
        /// 
        /// </summary>
        private AppSettingsProvider _appSettingsProvider;
        private ISecureStorageManager _secureStorageManager;
        private BrowseMode _browseMode;
        private IConfigurationSettingProvider _configurationSettingProvider;
        private UnityContainer unityContainer; 
        HttpRequestMessage httpRequestMessage;
        Windows.Web.Http.HttpCookie teacherAppCookie;
        WebView mywebview;
        bool showActivity = true;
        #endregion 
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public LoginWebView()
        {
            this.InitializeComponent();   
            this.Unloaded += LoginWebView_Unloaded;
        }
        #endregion
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var bounds = Window.Current.Bounds;
            //ActivityCanvas.Width = bounds.Width;
            //ActivityCanvas.Height = bounds.Height;

            //ActivityCanvas.Children.Add(ActivityIndicator.ProgressActivityIndicator);
            ActivityIndicator.ShowProgress();
            unityContainer = (UnityContainer)e.Parameter;
            var appSettings = unityContainer.Resolve<AppSettingsProvider>();
            var secureStorage = unityContainer.Resolve<SecureStorageManager>();
            var configurationSettingProvider = unityContainer.Resolve<ConfigurationSettingProvider>();
            _appSettingsProvider = appSettings;
            _secureStorageManager = secureStorage;
            _configurationSettingProvider = configurationSettingProvider;
            _browseMode = GlobalFields.BrowseMode;
            Uri path = new Uri(GetBrowseUrlString());
            mywebview = new WebView();           
            mywebview.Height = 900;
            mywebview.Width = 1000;          
            mywebview.NavigationCompleted += mywebview_NavigationCompleted;
            mywebview.NavigationStarting += mywebview_NavigationStarting;
            mygrid.Children.Add(mywebview);
           
            if (_browseMode == BrowseMode.Login || _browseMode == BrowseMode.Reauthenticate)
            {
                string identifierString = CurrentApp.AppId.ToString();
#if (DEBUG)
                {
                    identifierString = _appSettingsProvider["device-name"];                
                }
#endif
                //httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(path.ToString()));
                httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(path.ToString()));
                httpRequestMessage.Headers.Append("Device-Identifier", identifierString);
                mywebview.NavigateWithHttpRequestMessage(httpRequestMessage);
            
            }
            else
            {
                mywebview.Navigate(path);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetBrowseUrlString()
        {
            string browseUrl;
            switch (_browseMode)
            {
                case BrowseMode.Login:
                {
                    string baseUserAccountManagementAddress = _appSettingsProvider["base-user-account-management-address"];
                    var uriBuilder = new UriBuilder(baseUserAccountManagementAddress)
                    {
                        Path = "Security/SignIn"
                    };
                    browseUrl = uriBuilder.ToString();
                    break;
                }
                case BrowseMode.Logout:
                {
                    string baseUserAccountManagementAddress = _appSettingsProvider["base-user-account-management-address"];
                    var uriBuilder = new UriBuilder(baseUserAccountManagementAddress)
                    {
                        Path = "Security/SignOut"
                    };
                    browseUrl = uriBuilder.ToString();
                    break;
                }
                case BrowseMode.Detail:
                {
                    string baseUserAccountManagementAddress = _appSettingsProvider["base-user-account-management-address"];
                    var uriBuilder = new UriBuilder(baseUserAccountManagementAddress)
                    {
                        Path = "Security/Success"
                    };
                    browseUrl = uriBuilder.ToString();
                    break;
                }
                case BrowseMode.Reauthenticate:
                {
                    //need a substutie for windows
                    //_cookieManager.ClearAllCookies();
                    string baseUserAccountManagementAddress = _appSettingsProvider["base-user-account-management-address"];
                    var uriBuilder = new UriBuilder(baseUserAccountManagementAddress)
                    {
                        Path = "Security/SignIn"
                    };
                    browseUrl = uriBuilder.ToString();
                    break;
                }
                default:
                {
                    throw new InvalidOperationException(string.Format("The navigation mode of {0} has not been added to the Navigation Switch.", _browseMode));
                }
            }

            return browseUrl;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mywebview_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs e)
        {
            string url = mywebview.Source.ToString();
            Windows.Web.Http.Filters.HttpBaseProtocolFilter filter = new Windows.Web.Http.Filters.HttpBaseProtocolFilter();
            Windows.Web.Http.HttpCookieCollection cookieStorage = filter.CookieManager.GetCookies(new Uri(url));
            teacherAppCookie = cookieStorage.SingleOrDefault(x => x.Name == "sims_teacher_app");
            if (url.EndsWith("Security/Success"))
            {
                if (teacherAppCookie != null)
                {
                    var value = teacherAppCookie.Value;
                    var fieldValuePairs = value.Split('&');
                    var fieldValueDictionary = new Dictionary<string, string>();
                    foreach (var fieldValuePair in fieldValuePairs)
                    {
                        var fieldValueSet = fieldValuePair.Split('=');
                        if (fieldValueSet.Count() != 2) throw new InvalidOperationException("Query string item split into too many sections");
                        fieldValueDictionary.Add(fieldValueSet[0], fieldValueSet[1]);
                    }
                    string securityToken = fieldValueDictionary["SecurityToken"];
                    _secureStorageManager.WriteSecurityToken(securityToken);
                    string dataServiceBaseAddress = fieldValueDictionary["DataServiceEndpointAddress"];
                    _configurationSettingProvider["dataServiceBaseAddress"] = dataServiceBaseAddress;

                    string schoolIdentifier = fieldValueDictionary["SchoolIdentifier"];
                    _configurationSettingProvider["schoolIdentifier"] = schoolIdentifier;
                   
                   
                    PageManager pageManager = unityContainer.Resolve<PageManager>();
                    pageManager.NavigateToTimeTable();
                }
            }
            else if (url.EndsWith("Security/SignedOut"))
            {
                PageManager pageManager = unityContainer.Resolve<PageManager>();
                GlobalFields.BrowseMode = BrowseMode.Login;
                pageManager.NavigateToLoginPage();
                //teacherAppCookie.Expires = DateTime.Now.AddSeconds(1);
                //_exitSemaphore.Release();
            }            
            //code for hiding progress bar/ring
            //ActivityIndicator.CloseProgress(); //for progress ring
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mywebview_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs e)
        {
            ActivityIndicator.ShowProgress();
        }

        private void LoginWebView_Unloaded(object sender, RoutedEventArgs e)
        {
            ActivityCanvas.Children.Remove(ActivityIndicator.ProgressActivityIndicator);
            ActivityCanvas.Children.Clear();
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

       
        #endregion

       
    }
}
