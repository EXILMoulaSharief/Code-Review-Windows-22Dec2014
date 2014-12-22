using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.Shared.UI.Services;
using Windows.Networking.Connectivity;

namespace TeacherApp.Client.UI.WinApp
{
    internal class NetworkConnectivityService : INetworkConnectivityService
    {
        public enum NetworkStatus
        {
            NotReachable,
            ReachableViaCarrierDataNetwork,
            ReachableViaWiFiNetwork
        }

        public bool DeviceIsConnectedToANetwork()
        {
            return Reachability.InternetConnectionStatus() != NetworkStatus.NotReachable;
        }

        private static class Reachability
        {
            private static string GetConnectionProfile(ConnectionProfile connectionProfile)
            {
                string connectionProfileInfo = string.Empty;
                if (connectionProfile != null)
                {
                    switch (connectionProfile.GetNetworkConnectivityLevel())
                    {
                        case NetworkConnectivityLevel.None:
                            connectionProfileInfo = "None";
                            break;
                        case NetworkConnectivityLevel.LocalAccess:
                            connectionProfileInfo = "LocalAccess";
                            break;
                        case NetworkConnectivityLevel.ConstrainedInternetAccess:
                            connectionProfileInfo = "ConstrainedInternetAccess";
                            break;
                        case NetworkConnectivityLevel.InternetAccess:
                            connectionProfileInfo = "InternetAccess";
                            break;
                        default:
                            connectionProfileInfo = "None";
                            break;
                            

                    }                   
                }
                return connectionProfileInfo;
            }


            // 
            // Raised every time there is an interesting reachable event, 
            // we do not even pass the info as to what changed, and 
            // we lump all three status we probe into one
            //
            public static event EventHandler ReachabilityChanged;

            private static void OnChange(int flags)
            {
                var h = ReachabilityChanged;
                if (h != null)
                {
                    h(null, EventArgs.Empty);
                }
            }


            public static NetworkStatus InternetConnectionStatus()
            {
                string status = string.Empty; 
                ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();
                status = GetConnectionProfile(profile);
                if(status.Equals("None"))
                { return NetworkStatus.NotReachable; }
                if (status.Equals("LocalAccess"))
                { return NetworkStatus.ReachableViaCarrierDataNetwork; }
                if (status.Equals("ConstrainedInternetAccess"))
                { return NetworkStatus.NotReachable; }
                if (status.Equals("InternetAccess"))
                { return NetworkStatus.ReachableViaWiFiNetwork; }
                return NetworkStatus.NotReachable;
               
            }

        }
    }
}
