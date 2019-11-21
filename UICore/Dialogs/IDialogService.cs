using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UICore.Dialogs
{
    public interface IDialogService
    {
        Task<TViewModel> ShowDialogAsync<TView, TViewModel>(string title, object? parameter = null) where TView : UserControl
                                                         where TViewModel : class,IDialogViewModel<TViewModel>;
    }
}
