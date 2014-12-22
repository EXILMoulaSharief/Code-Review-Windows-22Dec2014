using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.Shared.UI.Services;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace TeacherApp.Client.UI.WinApp.Common
{
    public static class GlobalFields
    {
        //Properties
        public static BrowseMode BrowseMode { get; set; }

        public static object selectedModel { get; set; }

        //Methods
        public static Brush ColorToBrush(string color) // color = "#E7E44D"
        {
            color = color.Replace("#", "");
            if (color.Length == 6)
            {
                return new SolidColorBrush(ColorHelper.FromArgb(255,
                    byte.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber)));
            }
            else
            {
                return null;
            }
        }

    }
}
