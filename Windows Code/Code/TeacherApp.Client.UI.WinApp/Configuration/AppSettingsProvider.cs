using TeacherApp.Client.Domain.Configuration;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;

namespace TeacherApp.Client.UI.WinApp.Configuration
{
    internal class AppSettingsProvider : IAppSettingsProvider
    {
        public string this[string settingName]
        {
            get
            {
                ResourceContext ctx = new Windows.ApplicationModel.Resources.Core.ResourceContext();
                ResourceMap rmap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
                string settingValue = rmap.GetValue(settingName, ctx).ValueAsString;
                return settingValue;
            }
        }
    }
}