using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherApp.Client.Shared.UI.VisualSupport;
using Windows.UI.Popups;


namespace TeacherApp.Client.UI.WinApp
{
    public class MyMessageDialog 
    {
        public MessageDialog Message(UICommand [] commands, string Message, string caption)
        {
            var messageDialog = new MessageDialog(Message,caption);

            foreach (UICommand cmd in commands)
            {
                messageDialog.Commands.Add(cmd);
            }
            return messageDialog;
        }
     
       
    }
    /// <summary>
    /// 
    /// </summary>
    public static class ViewModelBinding
    {
        public static IDisposable BindViewModelPropertyChangedTo(this ViewModel viewModel, string propertyName, Action action)
        {
            var binding = new ViewModelPropertyChangedToAction(viewModel, action, propertyName);
            binding.Initialize();
            return binding;
        }
    }
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
            if (e.PropertyName == _propertyName)
            {
                _action.Invoke();
            }
        }
    }
}
