using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using UICore.Buttons;
using UICore.Presentation;

namespace UICore.Dialogs
{
    public class DialogViewModel<TViewModel> : Observable, IDialogViewModel<TViewModel> where TViewModel :DialogViewModel<TViewModel>
    {
        private readonly TaskCompletionSource<TViewModel> tcs = new TaskCompletionSource<TViewModel>();
        public DialogViewModel()
        {
            LeftButtons = new ObservableCollection<ButtonViewModel>();
            RightButtons = new ObservableCollection<ButtonViewModel>();
            AnimateShow = true;
            AnimateHide = true;
        }
        public ObservableCollection<ButtonViewModel> LeftButtons { get; set; }
        public ObservableCollection<ButtonViewModel> RightButtons { get; set; }
        public Action? MainViewModelCloseAction { get; set; }

        public Task<TViewModel> WaitForButonTask()
        {
            return tcs.Task;
        }

        private bool animateShow;

        public bool AnimateShow
        {
            get { return animateShow; }
            set { animateShow = value; OnPropertyChanged(); }
        }

        private bool animateHide;

        public bool AnimateHide
        {
            get { return animateHide; }
            set { animateHide = value; }
        }

        private bool startAnimateClose;

        public bool StartAnimateClose
        {
            get { return startAnimateClose; }
            set { startAnimateClose = value; OnPropertyChanged(); }
        }


        protected virtual void Close()
        {
            StartAnimateClose = true;
            if (MainViewModelCloseAction!= null) MainViewModelCloseAction();
            tcs.SetResult((TViewModel)this);
        }
    }
}
