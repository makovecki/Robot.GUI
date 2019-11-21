using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UICore.Controls.ProgressBar
{
    public class ProgressBarControl:Control
    {
        public ProgressBarControl()
        {
            DefaultStyleKey = typeof(ProgressBarControl);
        }



        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsIndeterminate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(ProgressBarControl), new PropertyMetadata(false));


    }
}
