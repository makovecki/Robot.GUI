using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UICore.Buttons;
using UICore.Dialogs;

namespace UICore.Dialogs.Controls
{
    public class DialogControl : Control
    {
        public DialogControl()
        {
            DefaultStyleKey = typeof(DialogControl);
        }



        public IDialogModel Dialog
        {
            get { return (IDialogModel)GetValue(DialogProperty); }
            set { SetValue(DialogProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dialog.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DialogProperty =
            DependencyProperty.Register("Dialog", typeof(IDialogModel), typeof(DialogControl), new PropertyMetadata(null, DialogPropertyChanged));

        private static void DialogPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DialogControl;

            if (control != null)
            control.Visibility = e.NewValue == null ? Visibility.Collapsed : Visibility.Visible;
        }



        public ObservableCollection<ButtonViewModel> LeftButtons
        {
            get { return (ObservableCollection<ButtonViewModel>)GetValue(LeftButtonsProperty); }
            set { SetValue(LeftButtonsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftButtons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftButtonsProperty =
            DependencyProperty.Register("LeftButtons", typeof(ObservableCollection<ButtonViewModel>), typeof(DialogControl), new PropertyMetadata(null));



        public ObservableCollection<ButtonViewModel> RightButtons
        {
            get { return (ObservableCollection<ButtonViewModel>)GetValue(RightButtonsProperty); }
            set { SetValue(RightButtonsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightButtons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightButtonsProperty =
            DependencyProperty.Register("RightButtons", typeof(ObservableCollection<ButtonViewModel>), typeof(DialogControl), new PropertyMetadata(null));



        public bool StartAnimateClose
        {
            get { return (bool)GetValue(StartAnimateCloseProperty); }
            set { SetValue(StartAnimateCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartAnimateClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAnimateCloseProperty =
            DependencyProperty.Register("StartAnimateClose", typeof(bool), typeof(DialogControl), new PropertyMetadata(false, StartAnimateClosePropertyChanged));

        private static void StartAnimateClosePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = d as DialogControl;
            //if ((bool)e.NewValue)
            //{
            //    Storyboard closingStoryboard = control.TryFindResource("UICore.OverLayClose") as Storyboard;
            //    closingStoryboard = closingStoryboard.Clone();
            //    closingStoryboard.Begin(control);
            //}
        }
    }
}
