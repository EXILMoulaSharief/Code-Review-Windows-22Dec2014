using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.UI.ViewModels.User;

namespace TeacherApp.Client.UI.WinApp.ViewModels
{
    public class LogoutViewModel
    {
        private readonly UserViewModel _userViewModel;

        public LogoutViewModel(UserViewModel userViewModel)
        {
            _userViewModel = userViewModel;
        }

        public void logout()
        {
            _userViewModel.LogoutUserCommand.Execute(null);

        }
    }
}
