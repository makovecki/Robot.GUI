using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace UICore.Dialogs
{
    public class DialogModel<T> : IDialogModel
    {


        public DialogModel(UserControl view, IDialogViewModel<T> viewModel, string title)
        {
            this.View = view;
            this.ViewModel = viewModel;
            view.DataContext = viewModel;
            Title = title;
        }

        public UserControl View { get; set; }
        public IDialogViewModel ViewModel { get; set; }
        public string Title { get; set; }
    }
}
