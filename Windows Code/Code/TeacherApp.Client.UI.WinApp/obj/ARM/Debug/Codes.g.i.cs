﻿

#pragma checksum "C:\Users\sourabh.b\Documents\Visual Studio 2013\Projects\TeacherApp.Client.UI.WinApp\TeacherApp.Client.UI.WinApp\Codes.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "873CD4369C47AF1EA5D97658DDB7E60D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeacherApp.Client.UI.WinApp
{
    partial class Codes : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Data.CollectionViewSource picturesSource; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button btnSearch; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.StackPanel SP_Search; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.StackPanel AppBarStackPanel; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.GridView gvPictures; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///Codes.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            picturesSource = (global::Windows.UI.Xaml.Data.CollectionViewSource)this.FindName("picturesSource");
            btnSearch = (global::Windows.UI.Xaml.Controls.Button)this.FindName("btnSearch");
            SP_Search = (global::Windows.UI.Xaml.Controls.StackPanel)this.FindName("SP_Search");
            AppBarStackPanel = (global::Windows.UI.Xaml.Controls.StackPanel)this.FindName("AppBarStackPanel");
            gvPictures = (global::Windows.UI.Xaml.Controls.GridView)this.FindName("gvPictures");
        }
    }
}



