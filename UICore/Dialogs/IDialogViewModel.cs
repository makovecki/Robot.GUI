using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UICore.Dialogs
{
    public interface IDialogViewModel<TViewModel> : IDialogViewModel
    {
        Task<TViewModel> WaitForButonTask();
    }

    public interface IDialogViewModel
    {
        Action? MainViewModelCloseAction { get; set; }
        bool AnimateHide { get; }
    }
}
