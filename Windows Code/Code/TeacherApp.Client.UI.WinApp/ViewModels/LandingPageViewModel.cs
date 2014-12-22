using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.UI.ViewModels.Login;


namespace TeacherApp.Client.UI.WinApp.ViewModels
{
    internal class LandingPageViewModel
    {
        private readonly InitialChoiceViewModel _initialChoiceViewModel;
        public LandingPageViewModel(InitialChoiceViewModel initialChoiceViewModel)
        {
             _initialChoiceViewModel = initialChoiceViewModel;
        }

        public void LoginUser()
        {
            try
            {
                 _initialChoiceViewModel.AssociateWithSchoolCommand.Execute(null);                
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
    }
}
