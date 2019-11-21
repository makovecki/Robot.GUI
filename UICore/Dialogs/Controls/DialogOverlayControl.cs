using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UICore.Dialogs;

namespace UICore.Dialogs.Controls
{
    public class DialogOverlayControl : Control
    {
        public DialogOverlayControl()
        {
            DefaultStyleKey = typeof(DialogOverlayControl);
        }

        public IDialogModel Dialog
        {
            get { return (IDialogModel)GetValue(DialogProperty); }
            set { SetValue(DialogProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dialog.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DialogProperty =
            DependencyProperty.Register("Dialog", typeof(IDialogModel), typeof(DialogOverlayControl), new PropertyMetadata(null, DialogPropertyChanged));

        private static void DialogPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DialogOverlayControl;

            if (control != null)
            control.Visibility = e.NewValue == null ? Visibility.Collapsed : Visibility.Visible;

            //if (control.Visibility == Visibility.Collapsed) control.Opacity = 0.7;
        }

        public bool StartAnimateClose
        {
            get { return (bool)GetValue(StartAnimateCloseProperty); }
            set { SetValue(StartAnimateCloseProperty, value); }
        }

        public static readonly DependencyProperty StartAnimateCloseProperty =
            DependencyProperty.Register("StartAnimateClose", typeof(bool), typeof(DialogOverlayControl), new PropertyMetadata(false, StartAnimateClosePropertyChanged));

        private static void StartAnimateClosePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = d as DialogOverlayControl;
            //if ((bool)e.NewValue)
            //{
            //    Storyboard closingStoryboard = control.TryFindResource("UICore.OverLayClose") as Storyboard;
            //    closingStoryboard = closingStoryboard.Clone();
            //    closingStoryboard.Begin(control);
            //}
           


            //}
        }
    }
}
