using System;
using System.ComponentModel;
using TeacherApp.Client.Shared.UI.VisualSupport;

namespace TeacherApp.Client.UI.WinApp
{
    public class ViewModelPropertyChangedToAction : IDisposable
    {
        private readonly Action _action;
        private readonly ViewModel _viewModel;
        private readonly string _propertyName;

        public ViewModelPropertyChangedToAction(ViewModel viewModel, Action action, string propertyName)
        {
            _viewModel = viewModel;
            _action = action;
            _propertyName = propertyName;
        }

        public void Initialize()
        {
            _viewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public void Dispose()
        {
            _viewModel.PropertyChanged -= ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == _propertyName)
            {
                _action.Invoke();
            }
        }
    }
}