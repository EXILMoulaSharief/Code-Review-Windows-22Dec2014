﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace TeacherApp.Client.UI.WinApp
{
    public partial class App : global::Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
        private global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlTypeInfoProvider _provider;

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            if(_provider == null)
            {
                _provider = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            if(_provider == null)
            {
                _provider = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(fullName);
        }

        public global::Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Windows.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }
}

namespace TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo
{
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByType(type);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByName(typeName);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;

        private void InitTypeTables()
        {
            _typeNameTable = new string[25];
            _typeNameTable[0] = "TeacherApp.Client.UI.WinApp.ImageConverter";
            _typeNameTable[1] = "Object";
            _typeNameTable[2] = "TeacherApp.Client.UI.WinApp.SignOut";
            _typeNameTable[3] = "Windows.UI.Xaml.Controls.UserControl";
            _typeNameTable[4] = "TeacherApp.Client.UI.WinApp.Achievement";
            _typeNameTable[5] = "Windows.UI.Xaml.Controls.Page";
            _typeNameTable[6] = "TeacherApp.Client.UI.WinApp.Attendance";
            _typeNameTable[7] = "TeacherApp.Client.UI.WinApp.AttendanceBar1";
            _typeNameTable[8] = "TeacherApp.Client.UI.WinApp.ObservableDictionary";
            _typeNameTable[9] = "String";
            _typeNameTable[10] = "TeacherApp.Client.UI.WinApp.NavigationHelper";
            _typeNameTable[11] = "Windows.UI.Xaml.DependencyObject";
            _typeNameTable[12] = "TeacherApp.Client.UI.WinApp.AttendanceList";
            _typeNameTable[13] = "TeacherApp.Client.UI.WinApp.BehaviourIncident";
            _typeNameTable[14] = "TeacherApp.Client.UI.WinApp.TimeTable";
            _typeNameTable[15] = "TeacherApp.Client.UI.WinApp.LoginWebView";
            _typeNameTable[16] = "TeacherApp.Client.UI.WinApp.CalenderViewDefalt";
            _typeNameTable[17] = "TeacherApp.Client.UI.WinApp.Codes";
            _typeNameTable[18] = "TeacherApp.Client.UI.WinApp.Home";
            _typeNameTable[19] = "TeacherApp.Client.UI.WinApp.LandingPage";
            _typeNameTable[20] = "TeacherApp.Client.UI.WinApp.Late";
            _typeNameTable[21] = "TeacherApp.Client.UI.WinApp.Login";
            _typeNameTable[22] = "TeacherApp.Client.UI.WinApp.MainPage";
            _typeNameTable[23] = "TeacherApp.Client.UI.WinApp.Search";
            _typeNameTable[24] = "TeacherApp.Client.UI.WinApp.StudentDetails";

            _typeTable = new global::System.Type[25];
            _typeTable[0] = typeof(global::TeacherApp.Client.UI.WinApp.ImageConverter);
            _typeTable[1] = typeof(global::System.Object);
            _typeTable[2] = typeof(global::TeacherApp.Client.UI.WinApp.SignOut);
            _typeTable[3] = typeof(global::Windows.UI.Xaml.Controls.UserControl);
            _typeTable[4] = typeof(global::TeacherApp.Client.UI.WinApp.Achievement);
            _typeTable[5] = typeof(global::Windows.UI.Xaml.Controls.Page);
            _typeTable[6] = typeof(global::TeacherApp.Client.UI.WinApp.Attendance);
            _typeTable[7] = typeof(global::TeacherApp.Client.UI.WinApp.AttendanceBar1);
            _typeTable[8] = typeof(global::TeacherApp.Client.UI.WinApp.ObservableDictionary);
            _typeTable[9] = typeof(global::System.String);
            _typeTable[10] = typeof(global::TeacherApp.Client.UI.WinApp.NavigationHelper);
            _typeTable[11] = typeof(global::Windows.UI.Xaml.DependencyObject);
            _typeTable[12] = typeof(global::TeacherApp.Client.UI.WinApp.AttendanceList);
            _typeTable[13] = typeof(global::TeacherApp.Client.UI.WinApp.BehaviourIncident);
            _typeTable[14] = typeof(global::TeacherApp.Client.UI.WinApp.TimeTable);
            _typeTable[15] = typeof(global::TeacherApp.Client.UI.WinApp.LoginWebView);
            _typeTable[16] = typeof(global::TeacherApp.Client.UI.WinApp.CalenderViewDefalt);
            _typeTable[17] = typeof(global::TeacherApp.Client.UI.WinApp.Codes);
            _typeTable[18] = typeof(global::TeacherApp.Client.UI.WinApp.Home);
            _typeTable[19] = typeof(global::TeacherApp.Client.UI.WinApp.LandingPage);
            _typeTable[20] = typeof(global::TeacherApp.Client.UI.WinApp.Late);
            _typeTable[21] = typeof(global::TeacherApp.Client.UI.WinApp.Login);
            _typeTable[22] = typeof(global::TeacherApp.Client.UI.WinApp.MainPage);
            _typeTable[23] = typeof(global::TeacherApp.Client.UI.WinApp.Search);
            _typeTable[24] = typeof(global::TeacherApp.Client.UI.WinApp.StudentDetails);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_ImageConverter() { return new global::TeacherApp.Client.UI.WinApp.ImageConverter(); }
        private object Activate_2_SignOut() { return new global::TeacherApp.Client.UI.WinApp.SignOut(); }
        private object Activate_4_Achievement() { return new global::TeacherApp.Client.UI.WinApp.Achievement(); }
        private object Activate_6_Attendance() { return new global::TeacherApp.Client.UI.WinApp.Attendance(); }
        private object Activate_7_AttendanceBar1() { return new global::TeacherApp.Client.UI.WinApp.AttendanceBar1(); }
        private object Activate_8_ObservableDictionary() { return new global::TeacherApp.Client.UI.WinApp.ObservableDictionary(); }
        private object Activate_12_AttendanceList() { return new global::TeacherApp.Client.UI.WinApp.AttendanceList(); }
        private object Activate_13_BehaviourIncident() { return new global::TeacherApp.Client.UI.WinApp.BehaviourIncident(); }
        private object Activate_14_TimeTable() { return new global::TeacherApp.Client.UI.WinApp.TimeTable(); }
        private object Activate_15_LoginWebView() { return new global::TeacherApp.Client.UI.WinApp.LoginWebView(); }
        private object Activate_16_CalenderViewDefalt() { return new global::TeacherApp.Client.UI.WinApp.CalenderViewDefalt(); }
        private object Activate_17_Codes() { return new global::TeacherApp.Client.UI.WinApp.Codes(); }
        private object Activate_18_Home() { return new global::TeacherApp.Client.UI.WinApp.Home(); }
        private object Activate_19_LandingPage() { return new global::TeacherApp.Client.UI.WinApp.LandingPage(); }
        private object Activate_20_Late() { return new global::TeacherApp.Client.UI.WinApp.Late(); }
        private object Activate_21_Login() { return new global::TeacherApp.Client.UI.WinApp.Login(); }
        private object Activate_22_MainPage() { return new global::TeacherApp.Client.UI.WinApp.MainPage(); }
        private object Activate_23_Search() { return new global::TeacherApp.Client.UI.WinApp.Search(); }
        private object Activate_24_StudentDetails() { return new global::TeacherApp.Client.UI.WinApp.StudentDetails(); }
        private void MapAdd_8_ObservableDictionary(object instance, object key, object item)
        {
            var collection = (global::System.Collections.Generic.IDictionary<global::System.String, global::System.Object>)instance;
            var newKey = (global::System.String)key;
            var newItem = (global::System.Object)item;
            collection.Add(newKey, newItem);
        }

        private global::Windows.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  TeacherApp.Client.UI.WinApp.ImageConverter
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.Activator = Activate_0_ImageConverter;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 1:   //  Object
                xamlType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  TeacherApp.Client.UI.WinApp.SignOut
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
                userType.Activator = Activate_2_SignOut;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 3:   //  Windows.UI.Xaml.Controls.UserControl
                xamlType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 4:   //  TeacherApp.Client.UI.WinApp.Achievement
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_4_Achievement;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 5:   //  Windows.UI.Xaml.Controls.Page
                xamlType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 6:   //  TeacherApp.Client.UI.WinApp.Attendance
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_6_Attendance;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 7:   //  TeacherApp.Client.UI.WinApp.AttendanceBar1
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_7_AttendanceBar1;
                userType.AddMemberName("DefaultViewModel");
                userType.AddMemberName("NavigationHelper");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 8:   //  TeacherApp.Client.UI.WinApp.ObservableDictionary
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.DictionaryAdd = MapAdd_8_ObservableDictionary;
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 9:   //  String
                xamlType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 10:   //  TeacherApp.Client.UI.WinApp.NavigationHelper
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.DependencyObject"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 11:   //  Windows.UI.Xaml.DependencyObject
                xamlType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 12:   //  TeacherApp.Client.UI.WinApp.AttendanceList
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_12_AttendanceList;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 13:   //  TeacherApp.Client.UI.WinApp.BehaviourIncident
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_13_BehaviourIncident;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 14:   //  TeacherApp.Client.UI.WinApp.TimeTable
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_14_TimeTable;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 15:   //  TeacherApp.Client.UI.WinApp.LoginWebView
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_15_LoginWebView;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 16:   //  TeacherApp.Client.UI.WinApp.CalenderViewDefalt
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_16_CalenderViewDefalt;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 17:   //  TeacherApp.Client.UI.WinApp.Codes
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_17_Codes;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 18:   //  TeacherApp.Client.UI.WinApp.Home
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_18_Home;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 19:   //  TeacherApp.Client.UI.WinApp.LandingPage
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_19_LandingPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 20:   //  TeacherApp.Client.UI.WinApp.Late
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_20_Late;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 21:   //  TeacherApp.Client.UI.WinApp.Login
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_21_Login;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 22:   //  TeacherApp.Client.UI.WinApp.MainPage
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_22_MainPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 23:   //  TeacherApp.Client.UI.WinApp.Search
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_23_Search;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 24:   //  TeacherApp.Client.UI.WinApp.StudentDetails
                userType = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_24_StudentDetails;
                userType.SetIsLocalType();
                xamlType = userType;
                break;
            }
            return xamlType;
        }


        private object get_0_AttendanceBar1_DefaultViewModel(object instance)
        {
            var that = (global::TeacherApp.Client.UI.WinApp.AttendanceBar1)instance;
            return that.DefaultViewModel;
        }
        private object get_1_AttendanceBar1_NavigationHelper(object instance)
        {
            var that = (global::TeacherApp.Client.UI.WinApp.AttendanceBar1)instance;
            return that.NavigationHelper;
        }

        private global::Windows.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlMember xamlMember = null;
            global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "TeacherApp.Client.UI.WinApp.AttendanceBar1.DefaultViewModel":
                userType = (global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType)GetXamlTypeByName("TeacherApp.Client.UI.WinApp.AttendanceBar1");
                xamlMember = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlMember(this, "DefaultViewModel", "TeacherApp.Client.UI.WinApp.ObservableDictionary");
                xamlMember.Getter = get_0_AttendanceBar1_DefaultViewModel;
                xamlMember.SetIsReadOnly();
                break;
            case "TeacherApp.Client.UI.WinApp.AttendanceBar1.NavigationHelper":
                userType = (global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlUserType)GetXamlTypeByName("TeacherApp.Client.UI.WinApp.AttendanceBar1");
                xamlMember = new global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlMember(this, "NavigationHelper", "TeacherApp.Client.UI.WinApp.NavigationHelper");
                xamlMember.Getter = get_1_AttendanceBar1_NavigationHelper;
                xamlMember.SetIsReadOnly();
                break;
            }
            return xamlMember;
        }
    }

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlSystemBaseType : global::Windows.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlUserType : global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlSystemBaseType
    {
        global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Windows.UI.Xaml.Markup.IXamlType _baseType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, global::System.Type fullType, global::Windows.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }

        override public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        override public object CreateFromString(string input)
        {
            if (_enumValues != null)
            {
                int value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    int enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt32(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return value; 
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlMember : global::Windows.UI.Xaml.Markup.IXamlMember
    {
        global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::TeacherApp.Client.UI.WinApp.TeacherApp_Client_UI_WinApp_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Windows.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Windows.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}



