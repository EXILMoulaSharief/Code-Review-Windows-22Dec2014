using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.Shared.Domain.Managers;
using Windows.Storage;

namespace TeacherApp.Client.UI.WinApp.Common
{
    internal class SecureStorageManager : ISecureStorageManager
    {
        private readonly IDictionary<string, string> _keyValueStore = new Dictionary<string, string>();

        private const string SecurityTokenKey = "SIMSTeacherServiceAccessKey";
        private const string DataServiceEndpointKey = "SIMSDataServiceAccessKey";
        const string secKeyChain = "TeachersApp";

        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.ApplicationDataContainer container;

        public void WriteSecurityToken(string securityToken)
        {
            container = localSettings.CreateContainer(secKeyChain, Windows.Storage.ApplicationDataCreateDisposition.Always);
            if (localSettings.Containers.ContainsKey(SecurityTokenKey))
            {
                localSettings.Containers[secKeyChain].Values[SecurityTokenKey] = null;
            }
            localSettings.Containers[secKeyChain].Values[SecurityTokenKey] = securityToken;
        }
        public string GetSecurityToken()
        {
            if (container.Values.ContainsKey(SecurityTokenKey))
            {
                return container.Values[SecurityTokenKey].ToString();
            }
            return null;
        }
    }
}
