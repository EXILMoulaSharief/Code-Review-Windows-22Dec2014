using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeacherApp.Client.Shared.UI.Services;
using TeacherApp.Client.UI.WinApp.Common;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TeacherApp.Client.UI.WinApp
{
    class DialogService : IDialogService
    {
        #region Declarations
        string commandSelected = default(string);
        #endregion
        #region Methods
        private PageManager _appPages;

        public void SetDependencies(PageManager appPages)
        {
            _appPages = appPages;
        }

        public void ShowYesNoAlertWithMessage(string message, Action<bool> result)
        {
            UICommand[] myUIcommand = new UICommand[] { new UICommand(), new UICommand() };
            myUIcommand[0] = new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler));
            myUIcommand[1] = new UICommand("No", new UICommandInvokedHandler(this.CommandInvokedHandler));
            MyMessageDialog msg = new MyMessageDialog();
            MessageDialog alertView = msg.Message(myUIcommand, message, string.Empty);
            alertView.DefaultCommandIndex = 0;
            if (commandSelected.Equals("Yes"))
            {
                result.Invoke(true);
            }
            else
            {
                result.Invoke(false);
            }

            //Need to check if its working else need to change implementaion

            alertView.ShowAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<OkCancelDialogResult> ShowOkCancelDialogAsync(string caption, string message)
        {
            OkCancelDialogResult okCancelDialogResult = OkCancelDialogResult.Cancel;
            UICommand[] myUIcommand = new UICommand[] { new UICommand(), new UICommand() };
            myUIcommand[0] = new UICommand("OK", new UICommandInvokedHandler(this.CommandInvokedHandler));
            myUIcommand[1] = new UICommand("Cancil", new UICommandInvokedHandler(this.CommandInvokedHandler));
            MyMessageDialog msg = new MyMessageDialog();
            MessageDialog alertView = msg.Message(myUIcommand, message, caption);
            if (commandSelected.Equals("OK"))
            {
                okCancelDialogResult = OkCancelDialogResult.OK;
            }
            await alertView.ShowAsync();
            return okCancelDialogResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<YesNoDialogResult> ShowYesNoDialogAsync(string caption, string message)
        {
            YesNoDialogResult yesNoDialogResult = YesNoDialogResult.No;
            UICommand[] myUIcommand = new UICommand[] { new UICommand(), new UICommand() };
            myUIcommand[0] = new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler));
            myUIcommand[1] = new UICommand("No", new UICommandInvokedHandler(this.CommandInvokedHandler));
            MyMessageDialog msg = new MyMessageDialog();
            MessageDialog alertView = msg.Message(myUIcommand, message, caption);
            if (commandSelected.Equals("Yes"))
            {
                yesNoDialogResult = YesNoDialogResult.Yes;
            }
            await alertView.ShowAsync();
            return yesNoDialogResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<YesNoDialogResult> ShowYesNoDialogAsync(string message)
        {
            YesNoDialogResult yesNoDialogResult = YesNoDialogResult.No;
            UICommand[] myUIcommand = new UICommand[] { new UICommand(), new UICommand() };
            myUIcommand[0] = new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler));
            myUIcommand[1] = new UICommand("No", new UICommandInvokedHandler(this.CommandInvokedHandler));
            MyMessageDialog msg = new MyMessageDialog();
            MessageDialog alertView = msg.Message(myUIcommand, message, string.Empty);
            if (commandSelected.Equals("Yes"))
            {
                yesNoDialogResult = YesNoDialogResult.Yes;
            }
            await alertView.ShowAsync();
            return yesNoDialogResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        async Task IDialogService.ShowOkDialogAsync(string caption, string message)
        {
            try
            {
                UICommand[] myUIcommand = new UICommand[] { new UICommand() };
                myUIcommand[0] = new UICommand("Ok", new UICommandInvokedHandler(this.CommandInvokedHandler));
                MyMessageDialog msg = new MyMessageDialog();
                MessageDialog alertView = msg.Message(myUIcommand, message, caption);
                await alertView.ShowAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="browseMode"></param>
        /// <returns></returns>
        async Task IDialogService.ShowWebDialogAsync(BrowseMode browseMode)
        {
            using (var semaphoreSlim = new SemaphoreSlim(0))
            {
                GlobalFields.BrowseMode = browseMode;
                _appPages.NavigateToLoginPage();
                await semaphoreSlim.WaitAsync();
            }
        }

        ///// <summary>
        /// Returning command type selcted 
        /// </summary>
        /// <param name="command"></param>
        public void CommandInvokedHandler(IUICommand command)
        {
            if (!command.Equals(null))
            {
                commandSelected = command.Label;
            }
            //return commandSelected;
        }
        #endregion
    }
}
