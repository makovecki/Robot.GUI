using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace UICore.Dialogs
{
    public interface IDialogModel
    {
        UserControl View { get; }
        IDialogViewModel ViewModel { get;  }
        string Title { get;  }
    }
}
