using System;
using System.Collections.Generic;
using System.Text;
using UICore.Dialogs;

namespace UICore.MainWindow
{
    public interface IMainWindowViewModel
    {
        IDialogModel? Dialog { get; set; }
    }
}
