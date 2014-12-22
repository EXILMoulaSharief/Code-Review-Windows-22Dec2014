using System;
using TeacherApp.Client.Shared.UI.VisualSupport;

namespace TeacherApp.Client.UI.WinApp
{
    public static class ViewModelBinding
    {
        public static IDisposable BindViewModelPropertyChangedTo(this ViewModel viewModel, string propertyName, Action action)
        {
            var binding = new ViewModelPropertyChangedToAction(viewModel, action, propertyName);
            binding.Initialize();
            return binding;
        }
    }
}