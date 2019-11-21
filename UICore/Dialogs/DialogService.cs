using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UICore.IoC;
using UICore.MainWindow;

namespace UICore.Dialogs
{
    public class DialogService : IDialogService
    {
        private readonly IIoCService iocService;
        private readonly IWindowService windowService;

        public DialogService(IIoCService iocService, IWindowService windowService)
        {
            this.iocService = iocService;
            this.windowService = windowService;
        }
        public async Task<TViewModel> ShowDialogAsync<TView, TViewModel>(string title, object? parameter =null) where TView : UserControl
                                                                where TViewModel : class,IDialogViewModel<TViewModel>
        {

            var window = await windowService.GetCurrentWindowWithViewModelAsync();
            IMainWindowViewModel? vm = window.DataContext as IMainWindowViewModel;

            if (vm != null)
            {
                vm.Dialog = new DialogModel<TViewModel>(iocService.ResolveTemporaryType<TView>(), iocService.ResolveTemporaryType<TViewModel>(parameter), title);

                vm.Dialog.ViewModel.MainViewModelCloseAction = () =>
               {
                   vm.Dialog = null;
               };
                return await ((IDialogViewModel<TViewModel>)((DialogModel<TViewModel>)vm.Dialog).ViewModel).WaitForButonTask();

            }

            return iocService.ResolveTemporaryType<TViewModel>(parameter);
        }
    }
}
